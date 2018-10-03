using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[SerializeField]
	private float moveSpeed = 2f;
	[SerializeField]
	private float acceleration = 10f;
	[SerializeField]
	private float health = 100f;

	private Vector2 desideredVelocity;
	private Vector2 currentVelocity;
	private Vector2 extraVelocity;

	private Rigidbody2D mRigidbody;

	[SerializeField]
	private LayerMask attackLayer;
	[SerializeField]
	private GameObject attackEffectPrefab;

	private LineRenderer levitateLine;
	private GameObject levitateObject;
	private Vector3 levitateOffset;
	[SerializeField]
	private LayerMask levitateLayer;

	private PetriNetPlace magicOrbPlace;
	private PetriNetPlace axePlace;

	private Text healthText;

	private float angle = 0;

	private bool hasWeapon {
		get {
			return axePlace.Markers > 0;
		}
	}
	private bool hasMagicOrb {
		get {
			return magicOrbPlace.Markers > 0;
		}
	}

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D> ();
		levitateLine = GetComponent<LineRenderer>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
	}

	void Start () {
		magicOrbPlace = GameController.main.petriNet.GetPlace ("magic_orb_picked");
		axePlace = GameController.main.petriNet.GetPlace ("axe_picked");
		Show();
	}

	void Show() {
		Debug.Log("W: " + hasWeapon + "  ::  M: " + hasMagicOrb);
	}

	void Update () {
		healthText.text = "Health: " + health;
		
		float hor = Input.GetAxisRaw ("Horizontal");
		float ver = Input.GetAxisRaw ("Vertical");

		if(ver > 0) {
			angle = 0f;

			if(hor > 0) angle -= 45f;
			else if(hor < 0) angle += 45f;
		} else if(ver < 0) {
			angle = 180f;		

			if(hor > 0) angle += 45f;
			else if(hor < 0) angle -= 45f;
		} else if(hor > 0) angle = 270f;
		else if(hor < 0) angle = 90f;

		transform.eulerAngles = new Vector3 (0f, 0f, angle);

		float mult = 0;
		if(ver != 0) mult = Mathf.Abs(ver);
		else if(hor != 0) mult = Mathf.Abs(hor);

		Vector2 moveVelocity = transform.up * mult;
		desideredVelocity = moveVelocity * moveSpeed;
		currentVelocity = Vector2.MoveTowards (currentVelocity, desideredVelocity, acceleration * Time.deltaTime);
		mRigidbody.velocity = currentVelocity + extraVelocity;

		extraVelocity = Vector2.MoveTowards(extraVelocity, Vector2.zero, 10f * Time.deltaTime);
		if(Input.GetKeyDown(KeyCode.Space)) {
			if(hasWeapon) {
				Attack();
			} else if(hasMagicOrb) {
				Levitate();
			}
		} else if(Input.GetKeyUp(KeyCode.Space)) {
			if(hasMagicOrb) {
				StopLevitate();
			}
		}

		if(levitateObject) {
			levitateObject.transform.position = transform.position - levitateOffset;

			levitateLine.SetPosition(0, transform.position);
			levitateLine.SetPosition(1, levitateObject.transform.position);
		}
	}

	public void TakeDamage(float dmg, Vector3 enemyPosition) {
		extraVelocity = Vector3.Normalize(transform.position - enemyPosition) * 10f;
		health -= dmg;
		if(health <= 0) {
			Destroy(this.gameObject);
		}
	}

	void Attack() {
		Instantiate(attackEffectPrefab, transform.position + transform.up, transform.rotation);
		RaycastHit2D hit = Physics2D.BoxCast(transform.position + transform.up * 1.5f, Vector2.one * 1.5f, 0f, Vector2.zero, 1f, attackLayer);
		if(hit) {

			Monster monster = hit.transform.GetComponent<Monster>();
			if(monster) {
				monster.TakeDamage(30f);
			}
		}
	}

	void Levitate() {
		RaycastHit2D hit = Physics2D.BoxCast(transform.position + transform.up * 1.5f, Vector2.one * 1.5f, 0f, Vector2.zero, 1f, levitateLayer);
		if(hit) {
			levitateLine.enabled = true;
			levitateObject = hit.transform.gameObject;
			levitateOffset = transform.position - levitateObject.transform.position;
			Rigidbody2D levitateRigid = levitateObject.GetComponent<Rigidbody2D>();
			if(levitateRigid) levitateRigid.velocity = Vector2.zero;
		}
	}

	void StopLevitate() {
		levitateLine.enabled = false;
		levitateObject = null;		
	}
	
}