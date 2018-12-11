using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class PatrolPosition {
	public Vector3 position;
	public float wait;
}

public class Monster : MonoBehaviour {

	public float health;
	public bool startPatroling;
	public bool runFromTarget;

    private Animator animator;
	private Rigidbody2D mRigidbody;
	private RoomController roomController;

	[SerializeField]
	public float patrolMoveSpeed;
	[SerializeField]
	public float moveSpeed;
	[SerializeField]
	private List<PatrolPosition> patrolList;
	private int patrolIndex;
	
	private float stunTime = 0;

	private bool isDead;
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
		isPatroling = startPatroling;
	}

	void Start() {
        roomController = GameObject.FindObjectOfType<RoomController>();
	}
	
	void Update () {
		if(isDead) return;

        if(stunTime > 0) {
            stunTime -= Time.deltaTime;
            moveVelocity = Vector2.zero;
            return;
		}

		if(isPatroling) {

            moveVelocity = Vector2.zero;

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


		} else if(target != null) {

			Vector3 direction = target.transform.position - transform.position;
			if(runFromTarget) {
                direction.x = -direction.x;
                direction.y = -direction.y;
			}

			if(direction.sqrMagnitude > 36) {

                target = null;
                isPatroling = startPatroling;

			} else {

                moveVelocity = direction.normalized * moveSpeed;

            }
			//transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);			

		} else {

            moveVelocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);

		}

        if (extraVelocity.sqrMagnitude != 0) extraVelocity = Vector3.MoveTowards(extraVelocity, Vector3.zero, moveSpeed * Time.deltaTime);

    }

	void FixedUpdate() {
		if (isDead) return;
        if (stunTime > 0) return;

        mRigidbody.velocity = moveVelocity + extraVelocity;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (isDead) return;
		if (runFromTarget) return;
		if (this.stunTime > 0) return;
		
		PlayerHealth player = coll.collider.GetComponent<PlayerHealth>();
		if(player) {
			player.TakeDamage(10f, transform.position);
		}
	}

	public void FollowTarget(GameObject target) {
		isPatroling = false;
		this.target = target;
	}

	public void Stun(float stunTime) {
		this.stunTime = stunTime;
        moveVelocity = Vector2.zero;
        extraVelocity = Vector2.zero;
    }

	public void TakeDamage(float dmg, Transform attacker) {
        if (animator) animator.SetTrigger("take_damage");

		extraVelocity = Vector3.Normalize(transform.position - attacker.position) * 6f;

		health -= dmg;
		if(health <= 0) {
        	animator.SetTrigger("die");
			isDead = true;
            roomController.MonsterDead();
		}
	}
}
