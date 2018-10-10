using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetection : MonoBehaviour {

	private Monster monster;

	void Awake () {
		monster = GetComponentInParent<Monster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Player player = coll.GetComponent<Player>();
		if(player) {
			monster.FollowTarget(player.gameObject);
		}
	}
}
