using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController_01 : RoomController {

	public int monsterRequired;
	private int monsterKilled;

	protected override void Start() {
		base.Start();
		monsterKilled = 0;
	}

	public override void MonsterDead() {
		monsterKilled += 1;
		if(monsterKilled == monsterRequired) {
			door.roomName = "_Scenes/Room_04";
			door.Activate();
		} else if(monsterKilled > monsterRequired) {
			door.roomName = "_Scenes/Room_05";
		}
	}

}
