using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//quando morrer
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float health = 100f;
    private float maxHealth;
    private int sceneId;
    PlayerMovement movement;

    private Text healthText;

    void Awake() {
        maxHealth = health;
        movement = GetComponent<PlayerMovement>();
    }

    void Start() {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
    }

    void Update() {
        // healthText.text = "Health: " + maxHealth;
    }

    public void TakeDamage(float dmg, Vector3 enemyPosition) {
        movement.AddExtraVelocity(Vector3.Normalize(transform.position - enemyPosition) * 10f);

        maxHealth -= dmg;
        if (maxHealth <= 0) {
            changeScene();
            //maxHealth = health;
            Destroy(this.gameObject);
        }
    }


    void changeScene()
    {
    
        GetComponent<PressRestart>().id = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Restart", LoadSceneMode.Additive);

    }

    int id() {
        return sceneId;
    }
}
