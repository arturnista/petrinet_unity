using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour {

	private PetriNetPlace magicOrbPlace;
	private PlayerMovement movement;
    private PlayerStatus status;

	private LineRenderer telekineseLine;
	private Rigidbody2D telekineseRigidbody;
	private Vector3 telekineseOffset;
	[SerializeField]
	private float telekineseRadius = 2f;
	[SerializeField]
	private LayerMask telekineseLayer;

	private bool isActive {
		get {
			return magicOrbPlace.Markers > 0;
		}
	}

	void Awake() {
		movement = GetComponent<PlayerMovement>();
    	status = GetComponent<PlayerStatus>();
        telekineseLine = GetComponent<LineRenderer>();		
		telekineseRigidbody = null;
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

		if(telekineseRigidbody) {
			telekineseRigidbody.velocity = movement.GetMoveVelocity();

			telekineseLine.SetPosition(0, transform.position);
			telekineseLine.SetPosition(1, telekineseRigidbody.transform.position);
		}
	}

	void Starttelekinese() {
		Collider2D coll = Physics2D.OverlapCircle(transform.position, telekineseRadius, telekineseLayer);
		if(coll) {
			movement.isActive = false;			
			telekineseLine.enabled = true;
			telekineseRigidbody = coll.transform.GetComponent<Rigidbody2D>();
			telekineseOffset = transform.position - telekineseRigidbody.transform.position;
			telekineseRigidbody.velocity = Vector2.zero;
		}
	}

	void Stoptelekinese() {
		movement.isActive = true;

		telekineseLine.enabled = false;
		telekineseRigidbody = null;		
	}
}
