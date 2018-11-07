using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject activate;
	public Sprite pressSprite;
	private Sprite normalSprite;

	protected bool openDoor;

    private SpriteRenderer spriteRenderer;
	private RoomDoor door;
	private bool isPressed;

	private int amount;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
        if(activate) door = activate.GetComponent<RoomDoor>();
		normalSprite = spriteRenderer.sprite;
        openDoor = true;
	}
	
	void Update () {
		
	}

	protected virtual void OnTriggerEnter2D(Collider2D coll) {
		Rigidbody2D isRigid = coll.GetComponent<Rigidbody2D>();
		if(isRigid == null) return;
		
		if(amount == 0) {
			spriteRenderer.sprite = pressSprite;
            if(openDoor) door.Activate();
		}
		amount++;
	}

    protected virtual void OnTriggerExit2D(Collider2D coll) {
		Rigidbody2D isRigid = coll.GetComponent<Rigidbody2D>();
		if(isRigid == null) return;
		
		amount--;
		if(amount <= 0) {
            if(openDoor) door.Deactivate();
            amount = 0;
			spriteRenderer.sprite = normalSprite;
		}
	}


}
