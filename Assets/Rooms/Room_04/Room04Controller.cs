using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room04Controller : RoomController {

	int monsterAmount;

	void Start () {
		Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
        monsterAmount = 0;
        foreach(Monster mon in monsters) {
			if(!mon.runFromTarget) monsterAmount += 1;
		}
	}

	public override void MonsterDead() {
        monsterAmount--;
		if(monsterAmount <= 0) roomDoor.Activate();
	}
}
