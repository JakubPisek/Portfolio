using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThristBar : MonoBehaviour {

	// Vektor
	Vector3 localScale;

	void Start() {
		// Načtení rozměrů objektu
		localScale = transform.localScale;
	}

	void Update() {
		// Do rozměru X se načte hodnota pro aktuální proměnnou 'thirst' v %
		localScale.x = PlayerController.thirst / 100.0f;
		transform.localScale = localScale;
	}
}