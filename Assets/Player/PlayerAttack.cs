using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private PetriNetPlace weaponPlace;
	private PlayerMovement movement;
	private SpriteRenderer attackIndicatorSprite;

	[SerializeField]
	private float damage;
	[SerializeField]
	private LayerMask attackLayer;
	[SerializeField]
	private GameObject attackEffectPrefab;
	[SerializeField]
	private GameObject attackProjectilePrefab;
	[SerializeField]
	private float spinAttackTime = 1f;
	private float attackStartTime;
	private bool isChargingAttack;
	private float chargeTime;

	private bool isActive {
		get {
			return weaponPlace.Markers > 0;
		}
	}

	void Awake() {
		movement = GetComponent<PlayerMovement>();
		attackIndicatorSprite = transform.Find("SpriteAttackIndicator").GetComponent<SpriteRenderer>();
		attackIndicatorSprite.enabled = false;
	}

	void Start () {
		weaponPlace = GameController.main.petriNet.GetPlace ("weapon_picked");		
	}
	
	void Update () {
		
		if(isActive) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				StartAttack();
			} else if(Input.GetKeyUp(KeyCode.Space)) {
				StopAttack();
			}

		}

		if(isChargingAttack) {
			chargeTime += Time.deltaTime;
			
			if(chargeTime < spinAttackTime) attackIndicatorSprite.color = Color.white;
			else attackIndicatorSprite.color = Color.red;
		}
	}

	void StartAttack() {
		attackStartTime = Time.time;
		isChargingAttack = true;
		
		movement.MoveSpeedMultiplier = .3f;
		chargeTime = 0f;
		attackIndicatorSprite.enabled = true;
	}

	void StopAttack() {
		float attackHoldTime = Time.time - attackStartTime;		
		isChargingAttack = false;
		attackIndicatorSprite.enabled = false;

		if(attackHoldTime < spinAttackTime) {
			BasicAttack();
		} else {
			SpinAttack();
		}

		movement.MoveSpeedMultiplier = 1f;
	}

	void BasicAttack() {
		Instantiate(attackEffectPrefab, transform.position + transform.up, transform.rotation);
		
		Vector2 size = Vector2.one * 1.5f;

		float playerHalfSize = 1.5f;
		Vector2 origin = transform.position + transform.up * playerHalfSize;

		DebugPhysics.Box box = new DebugPhysics.Box(origin, size / 2f, Quaternion.AngleAxis(movement.Angle, Vector3.forward));
		DebugPhysics.DrawBox(box, Color.red, 2f);

		Collider2D[] colliders = Physics2D.OverlapBoxAll(origin, size, movement.Angle, attackLayer);
		DamageMonsters(colliders, 1f);
	}

	void SpinAttack() {
		float range = 4f;

		DebugExtension.DebugCircle(transform.position, transform.forward, Color.red, range, 2f);

		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, attackLayer);
		DamageMonsters(colliders, 1f);	
	}

	void DamageMonsters(Collider2D[] colliders, float dmgMultiplier) {
		foreach (Collider2D coll in colliders) {
			Monster monster = coll.transform.GetComponent<Monster>();
			if(monster) {
				monster.TakeDamage(damage * dmgMultiplier, this.transform);
			}
		}
	}
}
