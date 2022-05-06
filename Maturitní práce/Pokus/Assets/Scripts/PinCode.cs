using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PinCode : MonoBehaviour {

	// PUBLIC komponenty/objekty
	public GameObject panel;
	public TMP_InputField pin;
	public TMP_Text wrong;
	public GameObject door_opened;
	public GameObject door_closed;
	public GameObject codeLock;
	public PlayerController player;

	void Start () {
		// Skrytí/odkrytí objektů
		door_closed.SetActive(true);
		door_opened.SetActive(false);
	}

	// Metody pro práci s panely, objekty
	// Slouží jako funkce onClick
	public void OnClickExit () {
		panel.SetActive(false);
		pin.text = "";
		wrong.text = "";		
	}

	public void OnClickOk () {		
		if (pin.text.Equals("1234")) {
			// Skrytí/odkrytí objektů
			panel.SetActive(false);
			door_opened.SetActive(true);
			door_closed.SetActive(false);
			codeLock.SetActive(false);
			Time.timeScale = 1;
		}
		else {
			wrong.text = "Wrong password";
		}
	}
}