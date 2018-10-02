using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_01 : RoomDoor {

	void OnTriggerEnter2D() {
		if(GameObject.FindObjectOfType<Player>().HasWeapon) {
        	SceneManager.LoadScene("Room_02", LoadSceneMode.Single);
		} else {
        	SceneManager.LoadScene("Room_03", LoadSceneMode.Single);
		}
	}

}
