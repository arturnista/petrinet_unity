using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDoor_02 : RoomDoor {

    void OnTriggerEnter2D() {
        SceneManager.LoadScene("Final", LoadSceneMode.Single);
	}

}
