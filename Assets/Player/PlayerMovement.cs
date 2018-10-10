using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float moveSpeed = 2f;
	[SerializeField]
	private float acceleration = 10f;

	private Vector2 desideredVelocity;
	private Vector2 currentVelocity;
	private Vector2 extraVelocity;

	private float moveSpeedMultiplier;
	public float MoveSpeedMultiplier {
		get {
			return moveSpeedMultiplier;
		}

		set {
			moveSpeedMultiplier = value;
		}
	}

	private Rigidbody2D mRigidbody;

	private float angle = 0;

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D> ();
		moveSpeedMultiplier = 1f;
	}

	void Update () {
		
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
		desideredVelocity = moveVelocity * moveSpeed * moveSpeedMultiplier;
		currentVelocity = Vector2.MoveTowards (currentVelocity, desideredVelocity, acceleration * Time.deltaTime);
		mRigidbody.velocity = currentVelocity + extraVelocity;

		extraVelocity = Vector2.MoveTowards(extraVelocity, Vector2.zero, 10f * Time.deltaTime);
	}

	public void AddExtraVelocity(Vector2 eVelocity) {
		extraVelocity += eVelocity;
	}
	
}