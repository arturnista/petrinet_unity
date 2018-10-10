using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_01 : RoomDoor {

	void OnTriggerEnter2D(Collider2D coll) {
		if(!this.CheckEnd(coll)) return;

		PetriNetPlace axePlace = GameController.main.petriNet.GetPlace ("axe_picked");
		bool hasWeapon = axePlace.Markers > 0;
		if(hasWeapon) {
        	SceneManager.LoadScene("Room_02", LoadSceneMode.Single);
		} else {
        	SceneManager.LoadScene("Room_03", LoadSceneMode.Single);
		}
	}

}
