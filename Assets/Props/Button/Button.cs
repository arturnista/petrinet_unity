using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject activate;
	public Sprite pressSprite;
	private Sprite normalSprite;

    private SpriteRenderer spriteRenderer;
	private RoomDoor door;
	private bool isPressed;

	private int amount;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
        door = activate.GetComponent<RoomDoor>();
		normalSprite = spriteRenderer.sprite;
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Rigidbody2D isRigid = coll.GetComponent<Rigidbody2D>();
		if(isRigid == null) return;
		
		if(amount == 0) {
			spriteRenderer.sprite = pressSprite;
            door.Activate();
		}
		amount++;
	}

	void OnTriggerExit2D(Collider2D coll) {
		Rigidbody2D isRigid = coll.GetComponent<Rigidbody2D>();
		if(isRigid == null) return;
		
		amount--;
		if(amount <= 0) {
            door.Deactivate();
            amount = 0;
			spriteRenderer.sprite = normalSprite;
		}
	}


}
