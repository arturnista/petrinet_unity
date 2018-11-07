using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_03 : RoomDoor {

	public string offensiveRoom;

    protected virtual void OnTriggerEnter2D(Collider2D coll) {
        if (!this.CheckEnd(coll)) return;
        Room03Controller rc = GameObject.FindObjectOfType<Room03Controller>();
		
        if(rc.isOffensive) {
        	SceneManager.LoadScene(offensiveRoom, LoadSceneMode.Single);
		} else {
            SceneManager.LoadScene(roomName, LoadSceneMode.Single);
        }
    }
}
