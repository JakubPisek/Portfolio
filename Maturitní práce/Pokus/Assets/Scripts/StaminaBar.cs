using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour {

	// Vektor
	Vector3 localScaleV;

	void Start () {
		// Načtení rozměrů objektu
		localScaleV = transform.localScale;	
	}	
	
	void Update () {
		// Do rozměru X se načte hodnota pro aktuální proměnnou 'stamina' v %
		localScaleV.x = PlayerController.stamina / 100.0f;
		transform.localScale = localScaleV;

		// Skrytí 'staminaBar', pokud je proměnná 'stamina' na 100%
		if (localScaleV.x >= 1.0f) {
			gameObject.GetComponent<Renderer>().enabled = false;
		}
		else {
			gameObject.GetComponent<Renderer>().enabled = true;
		}
	}
}