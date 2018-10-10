using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_01 : RoomDoor {

	void OnTriggerEnter2D(Collider2D coll) {
		if(!this.CheckEnd(coll)) return;

        SceneManager.LoadScene("Room_02", LoadSceneMode.Single);
	}

}
