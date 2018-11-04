using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController main;

	void Awake () {
		if(main != null) {
			Destroy(this.gameObject);
			return;
		}
		
		main = this;
        DontDestroyOnLoad(this);
		
        gameObject.AddComponent<PlayerStatus>();
	}
	
	public void Restart() {
		
	}
	
	void SaveFile() {
		Debug.Log("File Saved");
        string path = "./petri_net.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("Stuff");
        writer.Close();
	}

	void LoadFile() {
		Debug.Log("File Loaded");
        string path = "./petri_net.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
		string fileContent = reader.ReadToEnd();
        reader.Close();
	}

	void Update() {
		
		if(Input.GetKeyDown(KeyCode.I)) SaveFile();
		else if(Input.GetKeyDown(KeyCode.O)) LoadFile();
		
	}

}