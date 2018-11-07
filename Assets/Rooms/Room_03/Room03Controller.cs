using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room03Controller : RoomController {

	public bool isOffensive;

	public override void MonsterDead() {
		roomDoor.Activate(true);
        isOffensive = true;
	}

}
