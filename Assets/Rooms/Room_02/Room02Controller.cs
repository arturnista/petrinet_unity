using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room02Controller : RoomController {

	public override void MonsterDead() {
		roomDoor.Activate(true);
	}

}
