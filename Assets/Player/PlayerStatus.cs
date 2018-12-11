using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

	public static PlayerStatus main;
	
	private bool hasWeapon;
	private bool hasOrb;
    private bool hasHammer;
    private bool hasCharger;

	public bool HasOrb { get; set; }
    public bool HasWeapon { get; set; }
    public bool HasHammer { get; set; }
    public bool HasCharger { get; set; }

	void Awake () {
		if(main != null) return;

        main = this;
		DontDestroyOnLoad(gameObject);	
	}
	
	
	void Update () {
		
	}
}
