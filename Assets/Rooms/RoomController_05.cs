using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController_05 : RoomController {

	public int monsterRequired;
	private int monsterKilled;

	protected override void Start() {
		base.Start();

		PlayerStatus.main.HasWeapon = true;
		PlayerStatus.main.HasHammer = true;
	}

	public override void MonsterDead() {
		monsterKilled += 1;
		if(monsterKilled == monsterRequired) {
			door.Activate();
		}
	}

}
