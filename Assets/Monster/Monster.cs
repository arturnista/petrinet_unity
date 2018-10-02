using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public string petriName;
	public float health;

	private Rigidbody2D mRigidbody;

	void Start () {
		mRigidbody = GetComponent<Rigidbody2D>();
		RandomVelocity();
	}
	
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Player player = coll.collider.GetComponent<Player>();
		if(player) {
			Debug.Log("NAo?");
			player.TakeDamage(10f, transform.position);
		}
	}

	void RandomVelocity() {
		float x = Random.Range(-3f, 3f);
		float y = Random.Range(-3f, 3f);
		mRigidbody.velocity = new Vector3(x, y);
	}

	public void TakeDamage(float dmg) {
		health -= dmg;
		if(health <= 0) {
			GameController.main.petriNet.AddMarkers(petriName, 1);
			Destroy(this.gameObject);
		}
	}
}
