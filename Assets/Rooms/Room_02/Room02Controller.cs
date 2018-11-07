using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room02Controller : RoomController {

    ButtonRoom02[] buttons;

	void Start () {
        buttons = GameObject.FindObjectsOfType<ButtonRoom02>();
	}

    public void ActivateButton() {
		foreach(ButtonRoom02 btn in buttons) {
			if(!btn.isRightPressed) return;
		}

		roomDoor.Activate();
	}

	public void DeactivateButton() {
		roomDoor.Deactivate();
	}
}
