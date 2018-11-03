using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour {

	private Rigidbody2D mRigidbody;

	[SerializeField]
	private float moveSpeed = 20f;
	[SerializeField]
	private float damage = 35f;

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D>();
		mRigidbody.velocity = transform.up * moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Monster monster = coll.GetComponent<Monster>();
		if(monster != null) {
			monster.TakeDamage(damage, transform);
		}

		if(coll.gameObject.layer == LayerMask.NameToLayer("Map") || coll.gameObject.layer == LayerMask.NameToLayer("Props")) {
			Destroy(this.gameObject);
		}
	}
}
