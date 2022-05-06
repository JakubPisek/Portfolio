using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBack : MonoBehaviour {

	// PUBLIC komponenty/objekty
	public GameObject mainMenu;
	public GameObject resolutions;

	// Metody pro práci s panely
	// Slouží jako funkce onClick
	public void BackOnClick () {
		mainMenu.SetActive(true);
		resolutions.SetActive(false);
	}
}