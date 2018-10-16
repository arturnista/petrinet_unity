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
		mPetriNet.Clear();

		petriNet.CreatePlace ("magic_orb");
		petriNet.CreatePlace ("magic_orb_picked");
		petriNet.CreateTransition ("magic_orb_pickup");
		petriNet.CreateArc ("magic_orb", "magic_orb_pickup");
		petriNet.CreateArc ("magic_orb_pickup", "magic_orb_picked");

		petriNet.CreatePlace ("weapon");
		petriNet.CreatePlace ("weapon_picked");
		petriNet.CreateTransition ("weapon_pickup");
		petriNet.CreateArc ("weapon", "weapon_pickup");
		petriNet.CreateArc ("weapon_pickup", "weapon_picked");

		petriNet.CreatePlace ("room_01_requirement");
		petriNet.CreateArc ("weapon_pickup", "room_01_requirement");
		petriNet.CreateArc ("magic_orb_pickup", "room_01_requirement");

		petriNet.CreateTransition ("room_01_open_door");
		petriNet.CreatePlace ("room_02_final");
		petriNet.CreateArc ("room_01_requirement", "room_01_open_door");
		petriNet.CreateArc ("room_01_open_door", "room_02_final");

		petriNet.AddListener("room_01_open_door", () => {

		});

		petriNet.CreatePlace ("room_02_enemies");
		petriNet.CreateTransition ("room_02_enemies_killed");
		petriNet.CreatePlace ("room_02_requirement");
		petriNet.CreateTransition ("room_02_open_door");
		petriNet.CreatePlace ("room_02_final");
		
		petriNet.CreateArc ("room_02_enemies", "room_02_enemies_killed");
		petriNet.CreateArc ("room_02_enemies_killed", "room_02_requirement");
		petriNet.CreateArc ("room_02_requirement", "room_02_open_door");
		
		petriNet.CreateArc ("room_02_open_door", "room_02_final");

		petriNet.AddListener("room_02_enemies_killed", () => {
			Debug.Log("room_02_enemies_killed");
		});
	
		petriNet.CreatePlace ("room_02_button_press");
		petriNet.CreatePlace ("room_02_button_is_down");
		petriNet.CreateTransition ("room_02_button_down");
		petriNet.CreatePlace ("room_02_button_is_up");
		petriNet.CreateTransition ("room_02_button_up");

		petriNet.CreateArc("room_02_button_is_down", "room_02_button_up");
		petriNet.CreateArc("room_02_button_press", "room_02_button_up");

		petriNet.CreateArc("room_02_button_is_up", "room_02_button_down");
		petriNet.CreateArc("room_02_button_press", "room_02_button_down");

		petriNet.CreateArc("room_02_button_down", "room_02_button_is_down");
		petriNet.CreateArc("room_02_button_up", "room_02_button_is_up");

		petriNet.AddMarkers("room_02_button_is_up", 1);
	}
	
	void SaveFile() {
		Debug.Log("File Saved");
        string path = "./petri_net.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(petriNet);
        writer.Close();
	}

	void LoadFile() {
		Debug.Log("File Loaded");
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