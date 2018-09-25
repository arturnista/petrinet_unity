using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestNet : MonoBehaviour {

	private PetriNet petriNet;
	private Button btnPlace01;
	private Button btnPlace02;
	private Button btnPlace03;
	private Button btnPlace04;
	private Button btnTransition01;
	private Button btnTransition02;

	void Start () {
		petriNet = new PetriNet ();
		petriNet.CreatePlace ("place_01");
		petriNet.CreatePlace ("place_02");
		petriNet.CreatePlace ("place_03");
		petriNet.CreatePlace ("place_04");
		petriNet.CreateTransition ("transition_01");
		petriNet.CreateTransition ("transition_02");
		petriNet.CreateArc ("place_01", "transition_01");
		petriNet.CreateArc ("place_02", "transition_01");
		petriNet.CreateArc ("transition_01", "place_03");
		petriNet.CreateArc ("place_03", "transition_02");
		petriNet.CreateArc ("transition_02", "place_04");
		petriNet.CreateArc ("transition_02", "place_02");

		btnPlace01 = GameObject.Find ("place_01").GetComponent<Button> ();
		btnPlace02 = GameObject.Find ("place_02").GetComponent<Button> ();
		btnPlace03 = GameObject.Find ("place_03").GetComponent<Button> ();
		btnPlace04 = GameObject.Find ("place_04").GetComponent<Button> ();
		btnTransition01 = GameObject.Find ("transition_01").GetComponent<Button> ();
		btnTransition02 = GameObject.Find ("transition_02").GetComponent<Button> ();

		btnPlace01.onClick.AddListener (() => this.AddMarker ("place_01"));
		btnPlace02.onClick.AddListener (() => this.AddMarker ("place_02"));
		btnPlace03.onClick.AddListener (() => this.AddMarker ("place_03"));
		btnPlace04.onClick.AddListener (() => this.AddMarker ("place_04"));
		btnTransition01.onClick.AddListener (() => this.Process ("transition_01"));
		btnTransition02.onClick.AddListener (() => this.Process ("transition_02"));
	}

	public void Process (string name) {
		PetriNetTransition transition = petriNet.GetTransition (name);
		transition.Process ();
	}

	public void AddMarker (string name) {
		petriNet.AddMarkers (name, 1);
	}

	void Update () {
		btnPlace01.GetComponentInChildren<Text> ().text = "" + petriNet.GetPlace ("place_01").Markers;
		btnPlace02.GetComponentInChildren<Text> ().text = "" + petriNet.GetPlace ("place_02").Markers;
		btnPlace03.GetComponentInChildren<Text> ().text = "" + petriNet.GetPlace ("place_03").Markers;
		btnPlace04.GetComponentInChildren<Text> ().text = "" + petriNet.GetPlace ("place_04").Markers;
	}
}