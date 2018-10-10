using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour {

	private PetriNetPlace magicOrbPlace;

	private LineRenderer levitateLine;
	private GameObject levitateObject;
	private Vector3 levitateOffset;
	[SerializeField]
	private LayerMask levitateLayer;

	private bool isActive {
		get {
			return magicOrbPlace.Markers > 0;
		}
	}

	void Awake() {
		levitateLine = GetComponent<LineRenderer>();		
	}

	void Start () {
		magicOrbPlace = GameController.main.petriNet.GetPlace ("magic_orb_picked");		
	}
	
	void Update () {
		
		if(isActive) {
			if(Input.GetKeyDown(KeyCode.E)) {
				StartLevitate();
			} else if(Input.GetKeyUp(KeyCode.E)) {
				StopLevitate();
			}
		}

		if(levitateObject) {
			levitateObject.transform.position = transform.position - levitateOffset;

			levitateLine.SetPosition(0, transform.position);
			levitateLine.SetPosition(1, levitateObject.transform.position);
		}
	}

	void StartLevitate() {
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
