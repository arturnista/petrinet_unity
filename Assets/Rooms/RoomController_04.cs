using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController_04 : RoomController {

	protected override void Start() {
		base.Start();

		PlayerStatus.main.HasWeapon = true;
	}

}
