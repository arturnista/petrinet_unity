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
	}

	public void Process (string name) {
		PetriNetTransition transition = petriNet.GetTransition (name);
		transition.Process ();
	}

	public void AddMarker (string name) {
		petriNet.AddMarkers (name, 1);
	}

}