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

		petriNet.CreatePlace ("magic_orb");
		petriNet.CreateTransition ("magic_orb_pickup");
		petriNet.CreateArc ("magic_orb", "magic_orb_pickup");

		petriNet.CreatePlace ("axe");
		petriNet.CreateTransition ("axe_pickup");
		petriNet.CreateArc ("axe", "axe_pickup");

		petriNet.CreatePlace ("room_01_requirement");
		petriNet.CreateArc ("axe_pickup", "room_01_requirement");
		petriNet.CreateArc ("magic_orb_pickup", "room_01_requirement");

		petriNet.CreateTransition ("room_01_open_door");
		petriNet.CreatePlace ("room_01_door");
		petriNet.CreateArc ("room_01_requirement", "room_01_open_door");
		petriNet.CreateArc ("room_01_open_door", "room_01_door");

		petriNet.AddListener("axe_pickup", () => {
			PickUpItem[] items = GameObject.FindObjectsOfType<PickUpItem>();
			foreach(PickUpItem it in items) Destroy(it.gameObject);

			GameObject.FindObjectOfType<Player>().HasWeapon = true;
		});

		petriNet.AddListener("magic_orb_pickup", () => {
			PickUpItem[] items = GameObject.FindObjectsOfType<PickUpItem>();
			foreach(PickUpItem it in items) Destroy(it.gameObject);

			GameObject.FindObjectOfType<Player>().HasMagicOrb = true;
		});

		petriNet.AddListener("room_01_open_door", () => {

		});

		petriNet.CreatePlace ("room_02_enemies");
		petriNet.CreateTransition ("room_02_open_door");
		petriNet.CreatePlace ("room_02_door");
		
		petriNet.CreateArc ("room_02_enemies", "room_02_open_door", 5);
		petriNet.CreateArc ("room_02_open_door", "room_02_door");
	}

}