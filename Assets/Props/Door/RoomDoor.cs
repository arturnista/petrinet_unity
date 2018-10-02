using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour {

	public string petriName;
	public Sprite openSprite;

	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	
	void Start () {	
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		GameController.main.petriNet.AddListener(petriName, () => {
			spriteRenderer.sprite = openSprite;
			boxCollider.isTrigger = true;
		});
	}
	
}
