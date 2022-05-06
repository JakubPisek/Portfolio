using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerBar : MonoBehaviour {

	// Vektor
	Vector3 localScale;

	void Start() {
		// Načtení rozměrů objektu
		localScale = transform.localScale;
	}

	void Update() {
		// Do rozměru X se načte hodnota pro aktuální proměnnou 'hunger' v %
		localScale.x = PlayerController.hunger / 100.0f;
		transform.localScale = localScale;
	}
}