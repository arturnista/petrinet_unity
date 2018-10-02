using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenController : MonoBehaviour {

	public void Restart() {
        SceneManager.LoadScene("Room_01", LoadSceneMode.Single);		
	}
}
