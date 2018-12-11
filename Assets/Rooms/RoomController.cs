using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	public int monsterRequired;

	private RoomDoor door;
	private int monsterKilled;

	protected virtual void Start () {
		door = GameObject.FindObjectOfType<RoomDoor>();
		monsterKilled = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenDoor() {
		door.Activate();
	}

	public void MonsterDead() {
		monsterKilled += 1;
		if(monsterKilled >= monsterRequired) door.Activate();
	}

}
