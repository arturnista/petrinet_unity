using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

	public string petriName;
	
	void OnTriggerEnter2D(Collider2D coll) {
		Player player = coll.GetComponent<Player>();
		if(player) {
			GameController.main.petriNet.AddMarkers(petriName, 1);
			Destroy(this.gameObject);
		}
	}

}
