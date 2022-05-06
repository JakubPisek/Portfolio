using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRes : MonoBehaviour {

	// PUBLIC komponenty/objekty
	public GameObject mainMenu;
	public GameObject resolutions;

	void Start () {
		mainMenu.SetActive(true);
		resolutions.SetActive(false);
	}

	// Metody pro práci s panely
	// Slouží jako funkce onClick
	public void ResOnClick () {
		resolutions.SetActive(true);
		mainMenu.SetActive(false);
	}
}