using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	protected RoomDoor door;

	protected virtual void Awake () {
		if(PlayerStatus.main == null) {
			GameObject ps = new GameObject("PlayerStatus");
			ps.AddComponent<PlayerStatus>();
		}
	}

	protected virtual void Start () {
		door = GameObject.FindObjectOfType<RoomDoor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenDoor() {
		door.Activate();
	}

	public virtual void MonsterDead() {

	}

}
