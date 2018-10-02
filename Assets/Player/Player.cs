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
	private PetriNetPlace levelPlace;
	private PetriNetPlace experiencePlace;

	private LineRenderer lineAttack;
	[SerializeField]
	private LayerMask attackLayer;

	private Text healthText;

	private float angle = 0;

	private bool hasWeapon = false;
	private bool hasMagicOrb = false;

	public bool HasWeapon {
		get {
			return hasWeapon;
		}
		set {
			hasWeapon = value;
		}
	}

	public bool HasMagicOrb {
		get {
			return hasMagicOrb;
		}
		set {
			hasMagicOrb = value;
		}
	}

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D> ();
		lineAttack = GetComponent<LineRenderer>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
	}

	void Start () {
		levelPlace = GameController.main.petriNet.GetPlace ("level");
		experiencePlace = GameController.main.petriNet.GetPlace ("experience");
		GameController.main.petriNet.AddListener ("level_up", () => {
			Debug.Log ("Levelup! " + levelPlace.Markers);
		});

		GameController.main.petriNet.AddListener("room_01_open_door", () => {
			Debug.Log("The door is now open!");
		});
	}

	void Update () {
		healthText.text = "Health: " + health;
		
		float hor = Input.GetAxisRaw ("Horizontal");
		float ver = Input.GetAxisRaw ("Vertical");

		if(ver > 0) angle = 0f;
		else if(ver < 0) angle = 180f;
		if(hor > 0) angle = -90f;
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
		

		if (Input.GetKeyDown (KeyCode.P)) {
			GameController.main.petriNet.AddMarkers ("experience", 1);
			Debug.Log ("" + levelPlace.Markers + " EXP: " + experiencePlace.Markers);
		}

		lineAttack.SetPosition(0, transform.position);
		lineAttack.SetPosition(1, transform.position);
		if(Input.GetKeyDown(KeyCode.Space)) {
			Attack();
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
		lineAttack.SetPosition(0, transform.position);

		float atkDistance = 2f;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, atkDistance, attackLayer);
		if(hit) {

			Monster monster = hit.transform.GetComponent<Monster>();
			if(monster) {
				lineAttack.SetPosition(1, monster.transform.position);
				monster.TakeDamage(30f);
			} else {
				lineAttack.SetPosition(1, transform.position + transform.up * atkDistance);
			}
		} else {
			lineAttack.SetPosition(1, transform.position + transform.up * atkDistance);
		}
	}
	
}