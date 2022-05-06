using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	// PUBLIC proměnné
	public float waitTime; // časovač

	// PRIVATE komponenty/objekty
	private PlatformEffector2D effector;

	void Start () {
		// Načtení komponentů objektu
		effector = GetComponent<PlatformEffector2D>();
	}

	void Update () {
		// Časovač pro projití platformy
		if (Input.GetKeyUp(KeyCode.S)) {
			waitTime = 0.5f;
		}

		if (Input.GetKey(KeyCode.S)) {
			if (waitTime <= 0) { // Časovač na 0
				effector.rotationalOffset = 180f; // Otočení snímání kolize platformy o 180°
				waitTime = 0.5f; // Resetování časovače
			}
			else {
				waitTime -= Time.deltaTime; // Odpočítávání z časovače
			}
		}

		if (Input.GetKey(KeyCode.W)) {
			effector.rotationalOffset = 0f; // Otočení snímání kolize platformy o 0° (defaultní)
		}
	}
}