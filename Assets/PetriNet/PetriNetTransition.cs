using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetriNetTransition : PetriNetElement {

    public PetriNetTransition (string name) : base (name) {

    }

    public bool Process () {
        foreach (PetriNetArc inputArc in inputs) {
            PetriNetPlace place = inputArc.Input as PetriNetPlace;
            if (place.Markers < inputArc.Weight) return false;
        }

        foreach (PetriNetArc inputArc in inputs) {
            PetriNetPlace place = inputArc.Input as PetriNetPlace;
            place.RemoveMarkers (inputArc.Weight);
        }

        foreach (PetriNetArc outputArc in outputs) {
            PetriNetPlace place = outputArc.Output as PetriNetPlace;
            place.AddMarkers (outputArc.Weight);
        }

        return true;
    }

}