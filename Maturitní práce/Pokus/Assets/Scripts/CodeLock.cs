using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodeLock : MonoBehaviour {

	// PRIVATE proměnné
	private bool isPressed;
	private float distance = 0.5f; // dosah paprsku
	private float distanceP = 0.3f; // vzdálenost sběru
	private float distanceH = 0.7f; // vzdálenost zobrazení

	// PUBLIC proměnné
	public LayerMask whatIsPlayer;

	// PUBLIC komponenty/objekty
	public GameObject popupE;
	public GameObject panel;
	public PlayerController player;

	void Start () {
		panel.SetActive(false); // Skrytí panelu
		popupE.SetActive(false); // Skrytí ukazatele interakce
	}
	
	void Update () {

		// Neviditelné paprsky pro sběr
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsPlayer);
		RaycastHit2D hitInfoLeftPickUp = Physics2D.Raycast(transform.position, Vector2.left, distanceP, whatIsPlayer);
		// Neviditelné paprsky pro zobrazení 
		RaycastHit2D hitInfoLeft = Physics2D.Raycast(transform.position, Vector2.left, distanceH, whatIsPlayer);

		// Kontrola kolize paprsků Raycast 
		if (hitInfo.collider != null || hitInfoLeft.collider != null) {
			popupE.SetActive(true); // Odkrytí ukazatele interakce
			if (hitInfo.collider != null || hitInfoLeftPickUp.collider != null) {
				if (Input.GetKeyDown(KeyCode.E)) {
					isPressed = true;
				}
			}
		}
		else {
			isPressed = false;
			popupE.SetActive(false);
		}

		// Potvrzení akce
		if (isPressed == true && (hitInfo.collider != null || hitInfoLeftPickUp.collider != null)) {
			Time.timeScale = 0; // Zastavení času
			panel.SetActive(true);
		}
		else {
			Time.timeScale = 1; // Nahození času
			panel.SetActive(false);
			isPressed = false;
		}
	}
}