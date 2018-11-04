using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	public GameObject roomDoorObject;

	protected RoomDoor roomDoor;

	protected virtual void Awake() {
        roomDoor = roomDoorObject.GetComponent<RoomDoor>();
	}
	
	public virtual void MonsterDead () {
		
	}
	
}
