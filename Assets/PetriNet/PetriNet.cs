using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetriNet {

	private PetriNetPlace entryPoint;
	private List<PetriNetElement> elements;
	private List<PetriNetPlace> places;
	private List<PetriNetTransition> transitions;
	private List<PetriNetArc> arcs;

	public List<PetriNetArc> Arcs {
		get {
			return arcs;
		}
	}

	public PetriNet () {
		elements = new List<PetriNetElement> ();
		transitions = new List<PetriNetTransition> ();
		places = new List<PetriNetPlace> ();
		arcs = new List<PetriNetArc> ();
	}

	public PetriNetElement GetElement (string name) {
		return elements.Find (x => x.Name == name);
	}

	public PetriNetPlace GetPlace (string name) {
		return places.Find (x => x.Name == name);
	}

	public PetriNetTransition GetTransition (string name) {
		return transitions.Find (x => x.Name == name);
	}

	public void CreatePlace (string name) {
		PetriNetPlace place = new PetriNetPlace (name);
		elements.Add (place);
		places.Add (place);
	}

	public void CreateTransition (string name) {
		PetriNetTransition transition = new PetriNetTransition (name);
		elements.Add (transition);
		transitions.Add (transition);
	}

	public void AddMarkers (string name, int amount) {
		PetriNetPlace place = places.Find (x => x.Name == name);
		if (place != null) {
			place.AddMarkers (amount);
		}
	}

	public void CreateArc (string input, string output, int weight = 1) {
		PetriNetElement inputElement = elements.Find (x => x.Name == input);
		PetriNetElement outputElement = elements.Find (x => x.Name == output);

		if (inputElement != null && outputElement != null) {
			PetriNetArc arc = new PetriNetArc (inputElement, outputElement, weight);
			arcs.Add (arc);
		}
	}

	public void Process () {
		foreach (PetriNetTransition transition in transitions) {
			transition.Process ();
		}
	}

	public void Show () {
		string line = "";
		// PetriNetTransition transition;
		// PetriNetPlace place;
		// PetriNetArc arc = arcs[0];

		// foreach (PetriNetArc arc in arcs) {
		// 	if (line == "") {
		// 		line += arc.Input.Name + "|" + arc.Input.Markers;
		// 	}
		// 	line += " ---" + arc.Weight + "--> " + arc.Output.Name + "|" + arc.Output.Markers;
		// }

		Debug.Log (line);
	}

}