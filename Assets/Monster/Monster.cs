using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class PatrolPosition {
	public Vector3 position;
	public float wait;
}

public class Monster : MonoBehaviour {

	public string petriName;
	public float health;

	private Rigidbody2D mRigidbody;

	[SerializeField]
	public float patrolMoveSpeed;
	[SerializeField]
	public float moveSpeed;
	[SerializeField]
	private List<PatrolPosition> patrolList;
	private int patrolIndex;

	private bool isPatroling;
	private bool isWaitingPatrol;
	private float waitingPatrolTime;

	private GameObject target;

	private float maxHealth;

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D>();

		maxHealth = health;
		patrolIndex = 0;
		isPatroling = true;
	}
	
	void Update () {

		if(isPatroling) {

			if(isWaitingPatrol) {

				waitingPatrolTime += Time.deltaTime;
				if(waitingPatrolTime > patrolList[patrolIndex].wait) {
					isWaitingPatrol = false;
					patrolIndex = (patrolIndex + 1) % patrolList.Count;
				}

			} else {

				Vector3 nPos = patrolList[patrolIndex].position;
				if(Vector3.Distance(transform.position, nPos) < .1f) {
					isWaitingPatrol = true;
					waitingPatrolTime = 0;
				} else {
					transform.position = Vector3.MoveTowards(transform.position, nPos, patrolMoveSpeed * Time.deltaTime);
				}

			}


		} else {

			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);			

		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		PlayerHealth player = coll.collider.GetComponent<PlayerHealth>();
		if(player) {
			player.TakeDamage(10f, transform.position);
		}
	}

	public void FollowTarget(GameObject target) {
		isPatroling = false;
		this.target = target;
	}

	public void TakeDamage(float dmg) {
		health -= dmg;
		if(health <= 0) {
			GameController.main.petriNet.AddMarkers(petriName, 1);
			Destroy(this.gameObject);
		}
	}
}
