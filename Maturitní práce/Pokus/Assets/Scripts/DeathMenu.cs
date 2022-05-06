using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour {

	// PUBLIC komponenty/objekty
	public DeathObserver dO;
	public TMP_Text reason;

	void Start () {		
		// Načtení stringu do textu ve scéně
		reason.text = dO.GetObserver();
	}

	// Metody pro práci se scénami
	// Slouží jako funkce onClick
	public void RetryOnClick () {
		SceneManager.LoadScene("Home");
	}

	public void MainMenuOnClick () {
		SceneManager.LoadScene("Menu");
	}
}