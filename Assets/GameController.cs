using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private PetriNet mPetriNet;
	public PetriNet petriNet {
		get {
			return mPetriNet;
		}
	}

	public static GameController main;

	void Awake () {
		main = this;

		mPetriNet = new PetriNet ();

		petriNet.CreatePlace ("experience");
		petriNet.CreatePlace ("level");

		petriNet.CreateTransition ("level_up");

		bool arcCreated = petriNet.CreateArc ("experience", "level_up", 10);
		arcCreated = petriNet.CreateArc ("level_up", "level");

		petriNet.CreatePlace ("room_01_key_01");
		petriNet.CreatePlace ("room_01_key_02");
		petriNet.CreatePlace ("room_01_door");

		petriNet.CreateTransition ("room_01_open_door");

		petriNet.CreateArc ("room_01_key_01", "room_01_open_door");
		petriNet.CreateArc ("room_01_key_02", "room_01_open_door");
		petriNet.CreateArc ("room_01_open_door", "room_01_door");
	}

}