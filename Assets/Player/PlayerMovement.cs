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

	public bool isActive;

	private Rigidbody2D mRigidbody;
	private SpriteRenderer spriteRenderer;

	private float angle = 0;
	public float Angle {
		get {
			return angle;
		}
	}

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		moveSpeedMultiplier = 1f;
		isActive = true;
	}

	void Update () {
		if(isActive) {
			desideredVelocity = GetMoveVelocity();
		} else {
			desideredVelocity = Vector2.zero;
		}

		currentVelocity = Vector2.MoveTowards (currentVelocity, desideredVelocity, acceleration * Time.deltaTime);
		extraVelocity = Vector2.MoveTowards(extraVelocity, Vector2.zero, acceleration * Time.deltaTime);
	}

	void FixedUpdate() {
        mRigidbody.velocity = currentVelocity + extraVelocity;
	}

	public Vector2 GetMoveVelocity() {
		float hor = Input.GetAxisRaw ("Horizontal");
		float ver = Input.GetAxisRaw ("Vertical");

		float mult = 1f;
		if(hor != 0 && ver != 0) mult = .7f;

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

		if(hor < 0f) spriteRenderer.flipX = true;
		else if(hor > 0f) spriteRenderer.flipX = false;
		
		Debug.Log(angle * -1);
		transform.eulerAngles = new Vector3 (0f, 0f, angle);
		spriteRenderer.transform.eulerAngles = new Vector3 (0f, 0f, 0f);

		return new Vector2(hor, ver) * mult * moveSpeed * moveSpeedMultiplier;
	}

	public Vector2 GetMoveVelocityRotating() {
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

		return transform.up * mult * moveSpeed * moveSpeedMultiplier;
	}

	public void AddExtraVelocity(Vector2 eVelocity) {
		extraVelocity += eVelocity;
	}
	
}