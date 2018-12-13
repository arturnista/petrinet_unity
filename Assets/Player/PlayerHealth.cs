using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float health = 100f;
    private float maxHealth;
    PlayerMovement movement;

    private float invulnerabilityTime;

    private RectTransform healthBar;

    void Awake() {
        maxHealth = health;
        movement = GetComponent<PlayerMovement>();
    }

    void Start() {
        healthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
    }

    void Update() {
        if(invulnerabilityTime > 0) {
            invulnerabilityTime -= Time.deltaTime;
        }
        healthBar.localScale = new Vector3(1f, health / maxHealth, 1f);
    }

    public void TakeDamage(float dmg, Vector3 enemyPosition) {
        if(invulnerabilityTime > 0) return;
        
        movement.AddExtraVelocity(Vector3.Normalize(transform.position - enemyPosition) * 10f);
		CameraShaker.Instance.ShakeOnce(3f, 3f, .1f, .4f);

        health -= dmg;
        if (health <= 0) {
            ChangeScene();
            //maxHealth = health;
            Destroy(this.gameObject);
        }
    }


    void ChangeScene() {
    
        PlayerStatus.main.LastIdScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Restart", LoadSceneMode.Single);

    }

    public void AddInvulnerabilityTime(float time) {
        invulnerabilityTime = time;
    }

}
