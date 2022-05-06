using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour {

	// Metody pro práci se scénami
	// Slouží jako funkce onClick
	public void StartOnClick () {
		SceneManager.LoadScene("Home");
	}
}