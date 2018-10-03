using System.Collections;
using System.Collections.Generic;
using System.IO;
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
		if(main != null) {
			Destroy(this.gameObject);
			return;
		}
		
		main = this;

		DontDestroyOnLoad(this);
		CreateNet();
	}

	public void Restart() {
		CreateNet();
	}

	void CreateNet() {
		
		mPetriNet = new PetriNet ();

		petriNet.CreatePlace ("magic_orb");
		petriNet.CreatePlace ("magic_orb_picked");
		petriNet.CreateTransition ("magic_orb_pickup");
		petriNet.CreateArc ("magic_orb", "magic_orb_pickup");
		petriNet.CreateArc ("magic_orb_pickup", "magic_orb_picked");

		petriNet.CreatePlace ("axe");
		petriNet.CreatePlace ("axe_picked");
		petriNet.CreateTransition ("axe_pickup");
		petriNet.CreateArc ("axe", "axe_pickup");
		petriNet.CreateArc ("axe_pickup", "axe_picked");

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
		});

		petriNet.AddListener("magic_orb_pickup", () => {
			PickUpItem[] items = GameObject.FindObjectsOfType<PickUpItem>();
			foreach(PickUpItem it in items) Destroy(it.gameObject);
		});

		petriNet.AddListener("room_01_open_door", () => {

		});

		petriNet.CreatePlace ("room_02_enemies");
		petriNet.CreateTransition ("room_02_open_door");
		petriNet.CreatePlace ("room_02_door");
		
		petriNet.CreateArc ("room_02_enemies", "room_02_open_door", 5);
		petriNet.CreateArc ("room_02_open_door", "room_02_door");

		petriNet.CreatePlace ("room_03_button_press");
		petriNet.CreatePlace ("room_03_button_up");
		petriNet.CreatePlace ("room_03_door_is_open");
		petriNet.CreateTransition ("room_03_open_door");
		petriNet.CreateTransition ("room_03_close_door");

		petriNet.CreateArc ("room_03_button_press", "room_03_open_door");
		petriNet.CreateArc ("room_03_open_door", "room_03_door_is_open");
		
		petriNet.CreateArc ("room_03_button_up", "room_03_close_door");
		petriNet.CreateArc ("room_03_door_is_open", "room_03_close_door");
	}
	
	void SaveFile() {
        string path = "./petri_net.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(petriNet);
        writer.Close();
	}

	void LoadFile() {
        string path = "./petri_net.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
		mPetriNet.Update(reader.ReadToEnd());
        reader.Close();
	}

	void Update() {
		
		if(Input.GetKeyDown(KeyCode.I)) SaveFile();
		else if(Input.GetKeyDown(KeyCode.O)) LoadFile();
		
	}

}