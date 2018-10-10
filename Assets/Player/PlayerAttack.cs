using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private PetriNetPlace axePlace;
	private PlayerMovement movement;

	[SerializeField]
	private float damage;
	[SerializeField]
	private LayerMask attackLayer;
	[SerializeField]
	private GameObject attackEffectPrefab;
	[SerializeField]
	private GameObject attackProjectilePrefab;
	private float attackStartTime;
	private bool isChargingAttack;

	private bool isActive {
		get {
			return axePlace.Markers > 0;
		}
	}

	void Awake() {
		movement = GetComponent<PlayerMovement>();
	}

	void Start () {
		axePlace = GameController.main.petriNet.GetPlace ("axe_picked");		
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

		}
	}

	void StartAttack() {
		attackStartTime = Time.time;
		isChargingAttack = true;
		
		movement.MoveSpeedMultiplier = .3f;
	}

	void StopAttack() {
		float attackHoldTime = Time.time - attackStartTime;		
		isChargingAttack = false;

		if(attackHoldTime < .5f) {
			BasicAttack();
		} else if(attackHoldTime < 1f) {
			FrontAttack();
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

		float angle = transform.eulerAngles.z;
		
		DebugPhysics.Box box = new DebugPhysics.Box(origin, size / 2f, Quaternion.AngleAxis(angle, Vector3.forward));
		DebugPhysics.DrawBox(box, Color.red, 2f);

		Collider2D[] colliders = Physics2D.OverlapBoxAll(origin, size, angle, attackLayer);
		DamageMonsters(colliders, 1f);
	}

	void FrontAttack() {
		Instantiate(attackProjectilePrefab, transform.position, transform.rotation);
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
				monster.TakeDamage(damage * dmgMultiplier);
			}
		}
	}
}
