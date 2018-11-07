using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_01 : RoomDoor {

	public string offensiveRoomName;

	protected virtual void OnTriggerEnter2D(Collider2D coll) {
		if(!this.CheckEnd(coll)) return;
		Room01Controller rc = GameObject.FindObjectOfType<Room01Controller>();
		if(rc.monsterAmount < 0) SceneManager.LoadScene(offensiveRoomName, LoadSceneMode.Single);
		else SceneManager.LoadScene(roomName, LoadSceneMode.Single);
	}
}
