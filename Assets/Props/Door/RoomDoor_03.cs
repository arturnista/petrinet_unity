using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_03 : RoomDoor {

    public string petriNameClose;

    private Sprite closeSprite;

    protected override void Start() {
        base.Start();

        closeSprite = spriteRenderer.sprite;
        
		GameController.main.petriNet.AddListener(petriNameClose, () => {
			spriteRenderer.sprite = closeSprite;
			boxCollider.isTrigger = false;
		});
    }

    void OnTriggerEnter2D() {
        SceneManager.LoadScene("Final", LoadSceneMode.Single);
	}

}
