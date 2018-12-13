using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController_04 : RoomController {

	public int monsterRequired;
	private int monsterKilled;

	protected override void Start() {
		base.Start();

		monsterKilled = 0;
		PlayerStatus.main.HasWeapon = true;
		PlayerStatus.main.HasCharger = true;
	}

	public override void MonsterDead() {
		monsterKilled += 1;
		if(monsterKilled >= monsterRequired) {
			door.Activate();
		}
	}

}
