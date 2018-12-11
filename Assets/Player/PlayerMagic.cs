using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour {

	private PlayerMovement movement;
    private PlayerStatus status;

	private LineRenderer telekineseLine;
    private Transform telekineseTransform;
	private Rigidbody2D telekineseRigidbody;
	private Vector3 telekineseOffset;
	[SerializeField]
	private float telekineseRadius = 2f;
	[SerializeField]
	private LayerMask telekineseLayer;

	void Awake() {
		movement = GetComponent<PlayerMovement>();
    	status = GetComponent<PlayerStatus>();
        telekineseLine = GetComponent<LineRenderer>();		
		telekineseRigidbody = null;
        telekineseTransform = null;
	}

	void Start() {
        status = PlayerStatus.main;
    }

	void Update () {
		
		if(status.HasOrb) {
			if(Input.GetKeyDown(KeyCode.E)) {
				Starttelekinese();
			} else if(Input.GetKeyUp(KeyCode.E)) {
				Stoptelekinese();
			}
		}

		if(telekineseTransform) {
			if(telekineseRigidbody) telekineseRigidbody.velocity = movement.GetMoveVelocity();

			telekineseLine.SetPosition(0, transform.position);
			telekineseLine.SetPosition(1, telekineseTransform.position);
		}
	}

	void Starttelekinese() {
		Collider2D coll = Physics2D.OverlapCircle(transform.position, telekineseRadius, telekineseLayer);
		if(coll) {
			movement.isActive = false;			
			telekineseLine.enabled = true;
            telekineseTransform = coll.transform;
			telekineseRigidbody = coll.transform.GetComponent<Rigidbody2D>();
			telekineseOffset = transform.position - telekineseTransform.position;
			if(telekineseRigidbody) telekineseRigidbody.velocity = Vector2.zero;

            RoomDoor door = coll.transform.GetComponent<RoomDoor>();
			if(door) door.Activate();
		}
	}

	void Stoptelekinese() {
		movement.isActive = true;

		telekineseLine.enabled = false;
		telekineseRigidbody = null;
        telekineseTransform = null;	
	}
}
