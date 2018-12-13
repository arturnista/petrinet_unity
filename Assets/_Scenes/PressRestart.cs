using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressRestart : MonoBehaviour
{
    public int id;

    public void Quit() {
        Application.Quit();        
    }

    public void Restart() {
        SceneManager.LoadScene(PlayerStatus.main.LastIdScene);
    }
    
}
