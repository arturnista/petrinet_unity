using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class SpawnObject {
	public GameObject prefab;
	public Vector3 position;
}


public class SpawnTrigger : MonoBehaviour {

	[SerializeField]
	private List<SpawnObject> objectsList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Player player = coll.transform.GetComponent<Player>();
		if(player) {
			foreach(SpawnObject obj in objectsList) {
				Instantiate(obj.prefab, obj.position, Quaternion.identity);
			}
			
			Destroy(this.gameObject);
		}
	}
}
