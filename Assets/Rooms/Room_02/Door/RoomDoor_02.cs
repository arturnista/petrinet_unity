using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_02 : RoomDoor {

    public string petriNameOpenButton;
    public string petriNameCloseButton;

    private Sprite closeSprite;

    protected override void Start() {
        base.Start();

        closeSprite = spriteRenderer.sprite;

		PetriNetPlace roomFinal = GameController.main.petriNet.GetPlace("room_02_final");
        
		GameController.main.petriNet.AddListener(petriNameOpenButton, () => {
			spriteRenderer.sprite = openSprite;
			boxCollider.isTrigger = true;
		});
        
		GameController.main.petriNet.AddListener(petriNameCloseButton, () => {
			if(roomFinal.Markers == 0) {
				spriteRenderer.sprite = closeSprite;
				boxCollider.isTrigger = false;
			}
		});
    }

	void OnTriggerEnter2D(Collider2D coll) {
		if(!this.CheckEnd(coll)) return;
        SceneManager.LoadScene("Final", LoadSceneMode.Single);
	}

}
