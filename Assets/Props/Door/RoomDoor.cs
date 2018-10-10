using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour {

	public string petriName;
	public Sprite openSprite;

	protected SpriteRenderer spriteRenderer;
	protected BoxCollider2D boxCollider;
	
	protected virtual void Start () {	
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		
		GameController.main.petriNet.AddListener(petriName, () => {
			spriteRenderer.sprite = openSprite;
			boxCollider.isTrigger = true;
		});
	}

	protected bool CheckEnd(Collider2D coll) {		
		Player player = coll.GetComponent<Player>();
		return player != null;

	}
	
}
