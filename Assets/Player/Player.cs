using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private float moveSpeed = 2f;
	[SerializeField]
	private float acceleration = 10f;

	private Vector2 desideredVelocity;

	private Rigidbody2D mRigidbody;
	private PetriNetPlace levelPlace;
	private PetriNetPlace experiencePlace;

	private float angle = 0;

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D> ();
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
		mRigidbody.velocity = Vector2.MoveTowards (mRigidbody.velocity, desideredVelocity, acceleration * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.P)) {
			GameController.main.petriNet.AddMarkers ("experience", 1);
			Debug.Log ("" + levelPlace.Markers + " EXP: " + experiencePlace.Markers);
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Attack", 0f, .1f);
		} else if(Input.GetKeyDown(KeyCode.Space)) {
			CancelInvoke();
		}
	}

	void Attack() {

	}
	
}