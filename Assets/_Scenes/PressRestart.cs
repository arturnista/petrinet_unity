using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PressRestart : MonoBehaviour
{
   public int id;
   // PlayerHealth ph;
    // Use this for initialization
    void Start()
    {
        SceneManager.LoadScene(id);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
