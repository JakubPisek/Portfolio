using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HygieneBar : MonoBehaviour {

	// Vektor
	Vector3 localScale;

	void Start() {
		// Načtení rozměrů objektu
		localScale = transform.localScale;
	}

	void Update() {
		// Do rozměru X se načte hodnota pro aktuální proměnnou 'hygiene' v %
		localScale.x = PlayerController.hygiene / 100.0f;
		transform.localScale = localScale;		
	}
}