using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_00 : RoomDoor {

	public string orbRoomName;
	public string weaponRoomName;

	protected override void OnTriggerEnter2D(Collider2D coll) {
		if(!this.CheckEnd(coll)) return;
		if(!PlayerStatus.main.HasOrb) {
        	SceneManager.LoadScene(weaponRoomName, LoadSceneMode.Single);
		} else if(!PlayerStatus.main.HasWeapon) {
            SceneManager.LoadScene(orbRoomName, LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene(roomName, LoadSceneMode.Single);
        }
	}
}
