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
	}

}