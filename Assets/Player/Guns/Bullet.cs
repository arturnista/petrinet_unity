using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Rigidbody2D mRigidbody;

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D>();
		mRigidbody.velocity = transform.up * 30f;

		Invoke("DestroyBullet", 5f);
	}

	void DestroyBullet() {
		Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update () {

	}
}