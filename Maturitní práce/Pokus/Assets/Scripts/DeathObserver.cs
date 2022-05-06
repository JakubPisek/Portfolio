using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathObserver : MonoBehaviour {

	// PUBLIC STATIC proměnné
	public static string observer;

	// Setter pro nastavení důvodu smrti
	public void SetObserver (string newObserver) {
		observer = newObserver;
	}

	// Metoda string, která vrací řetězec znaků
	public string GetObserver () {
		return observer;
	}
}