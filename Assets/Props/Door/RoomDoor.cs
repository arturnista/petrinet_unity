using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour {

	public string petriName;
	
	void Start () {	
		GameController.main.petriNet.AddListener(petriName, () => {
			Destroy(this.gameObject);
		});
	}
	
}
