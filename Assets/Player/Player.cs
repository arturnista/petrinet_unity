using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	void Awake() {
		if(PlayerStatus.main == null) {
			GameObject go = new GameObject("PlayerStatus");
			go.AddComponent<PlayerStatus>();
		}

		if(GameController.main == null) {
			GameObject go = new GameObject("GameController");
			go.AddComponent<GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
