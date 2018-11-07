using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor : MonoBehaviour {

	public string roomName;
	public Sprite openSprite;
	public bool startOpen;

    protected Sprite closeSprite;

	protected SpriteRenderer spriteRenderer;
	protected BoxCollider2D boxCollider;

    protected bool isLocked;
	
	protected virtual void Start () {	
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();

        closeSprite = spriteRenderer.sprite;
        isLocked = false;

		if(startOpen) Activate();
	}

	public void Activate(bool lockRoom = false) {
        if (isLocked) return;

        spriteRenderer.sprite = openSprite;
        boxCollider.isTrigger = true;

        if(lockRoom) isLocked = lockRoom;
	}

	public void Deactivate(bool lockRoom = false) {
        if (isLocked) return;

        spriteRenderer.sprite = closeSprite;
        boxCollider.isTrigger = false;

        if (lockRoom) isLocked = lockRoom;
    }

	protected bool CheckEnd(Collider2D coll) {		
		Player player = coll.GetComponent<Player>();
		return player != null;
	}

	protected virtual void OnTriggerEnter2D(Collider2D coll) {
		if(!this.CheckEnd(coll)) return;
        SceneManager.LoadScene(roomName, LoadSceneMode.Single);
	}
	
}
