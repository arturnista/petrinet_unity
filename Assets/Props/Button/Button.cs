using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public string pressPetriName;
	public string upPetriName;

	public Sprite pressSprite;
	private Sprite normalSprite;

	private SpriteRenderer spriteRenderer;
	private bool isPressed;

	private int amount;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		normalSprite = spriteRenderer.sprite;
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Rigidbody2D isRigid = coll.GetComponent<Rigidbody2D>();
		if(isRigid == null) return;
		
		if(amount == 0) {
			spriteRenderer.sprite = pressSprite;
			GameController.main.petriNet.AddMarkers(pressPetriName, 1);
		}
		amount++;
	}

	void OnTriggerExit2D(Collider2D coll) {
		Rigidbody2D isRigid = coll.GetComponent<Rigidbody2D>();
		if(isRigid == null) return;
		
		amount--;
		if(amount <= 0) {
			amount = 0;
			spriteRenderer.sprite = normalSprite;
			GameController.main.petriNet.AddMarkers(upPetriName, 1);
		}
	}


}
