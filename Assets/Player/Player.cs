using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private float moveSpeed = 2f;
	[SerializeField]
	private float acceleration = 10f;

	private Vector2 desideredVelocity;

	private Rigidbody2D mRigidbody;
	private PetriNetPlace levelPlace;
	private PetriNetPlace experiencePlace;

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		levelPlace = GameController.main.petriNet.GetPlace ("level");
		experiencePlace = GameController.main.petriNet.GetPlace ("experience");
		GameController.main.petriNet.AddListener ("level_up", () => {
			Debug.Log ("Levelup! " + levelPlace.Markers);
		});
	}

	void Update () {
		float hor = Input.GetAxisRaw ("Horizontal");
		float ver = Input.GetAxisRaw ("Vertical");

		Vector2 moveVelocity = transform.up * ver + transform.right * hor;
		desideredVelocity = moveVelocity * moveSpeed;

		mRigidbody.velocity = Vector2.MoveTowards (mRigidbody.velocity, desideredVelocity, acceleration * Time.deltaTime);

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 direction = Vector3.Normalize (transform.position - mousePos);
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;

		transform.eulerAngles = new Vector3 (0f, 0f, angle + 90f);

		if (Input.GetKeyDown (KeyCode.Space)) {
			GameController.main.petriNet.AddMarkers ("experience", 1);
			Debug.Log ("" + levelPlace.Markers + " EXP: " + experiencePlace.Markers);
		}

		if(Input.GetMouseButtonDown(0)) {
			InvokeRepeating("Fire", 0f, .1f);
		} else if(Input.GetMouseButtonUp(0)) {
			CancelInvoke();
		}
	}

	void Fire() {
		Instantiate(bulletPrefab, transform.position, transform.rotation);
	}
}