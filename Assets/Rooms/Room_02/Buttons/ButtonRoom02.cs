using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRoom02 : Button {

    public PropsRoom03.PropForm form;
    private Room02Controller controller;
	public bool isRightPressed;

	void Start () {

        controller = GameObject.FindObjectOfType<Room02Controller>();

        isRightPressed = false;
        openDoor = false;
		
	}
	
	protected virtual void OnTriggerEnter2D(Collider2D coll) {
		base.OnTriggerEnter2D(coll);

        PropsRoom03 prop = coll.GetComponent<PropsRoom03>();
		if(prop && prop.form == form) {
            isRightPressed = true;
            controller.ActivateButton();
		}
	}

    protected virtual void OnTriggerExit2D(Collider2D coll) {
        base.OnTriggerExit2D(coll);

        PropsRoom03 prop = coll.GetComponent<PropsRoom03>();
		if(prop && prop.form == form) {
            isRightPressed = false;
            controller.DeactivateButton();
        }

    }
}
