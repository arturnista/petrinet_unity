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

    private Animator animator;
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

	private Vector3 moveVelocity;
	private Vector3 extraVelocity;

	private GameObject target;

	private float maxHealth;

	void Awake () {
		mRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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

			Vector3 direction = target.transform.position - transform.position;
            moveVelocity = direction.normalized * moveSpeed;
			if(extraVelocity.sqrMagnitude != 0) extraVelocity = Vector3.MoveTowards(extraVelocity, Vector3.zero, moveSpeed * Time.deltaTime);

			//transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);			

		}

	}

	void FixedUpdate() {
        mRigidbody.velocity = moveVelocity + extraVelocity;
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

	public void TakeDamage(float dmg, Transform attacker) {
        animator.SetTrigger("take_damage");

		extraVelocity = Vector3.Normalize(transform.position - attacker.position) * 6f;

		health -= dmg;
		if(health <= 0) {
			GameController.main.petriNet.AddMarkers(petriName, 1);
			Destroy(this.gameObject);
		}
	}
}
