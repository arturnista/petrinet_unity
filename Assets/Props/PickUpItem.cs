using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

	public string petriName;
	public Sprite emptySprite;
	
	void OnTriggerEnter2D(Collider2D coll) {
		Player player = coll.GetComponent<Player>();
		if(player) {
			GameController.main.petriNet.AddMarkers(petriName, 1);
			//Destroy(this.gameObject);
			GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<Animator>().enabled = false;
			GetComponentInChildren<SpriteRenderer>().sprite = emptySprite;
		}
	}

}
