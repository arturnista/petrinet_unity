using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	Player player;
	
	void Awake () {
		player = GameObject.FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 nPos = player.transform.position;
        nPos.z = -10f;

		transform.position = nPos;
	}
}
