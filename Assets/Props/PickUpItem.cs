using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

    public enum PickUpType {
        Weapon, Orb
    }

    public PickUpType type;
	public GameObject activate;
	public Sprite emptySprite;
	
	void OnTriggerEnter2D(Collider2D coll) {
		Player player = coll.GetComponent<Player>();
		if(player) {
			//Destroy(this.gameObject);
			GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<Animator>().enabled = false;
			GetComponentInChildren<SpriteRenderer>().sprite = emptySprite;

            GameObject.FindObjectOfType<RoomController>().OpenDoor();

			if(type == PickUpType.Weapon) {
				PlayerStatus.main.HasWeapon = true;
				PlayerStatus.main.HasHammer = false;
				PlayerStatus.main.HasCharger = false;
			} else if(type == PickUpType.Orb) {
				PlayerStatus.main.HasOrb = true;
			}
		}
	}

}
