using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenController : MonoBehaviour {

	public void Restart() {
        GameController.main.Restart();
        SceneManager.LoadScene("Rooms/Room_01/Scene", LoadSceneMode.Single);		
	}
}
