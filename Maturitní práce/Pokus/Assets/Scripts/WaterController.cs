using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

	// PRIVATE proměnné
	private bool pushE;
	private bool isPressed;
	private float distance = 0.5f; // dosah paprsku
	private float distanceP = 0.3f; // vzdálenost sběru
	private float distanceH = 0.7f; // vzdálenost zobrazení

	// PUBLIC proměnné
	public LayerMask whatIsPlayer;

	// PUBLIC komponenty/objekty
	public GameObject popupE;
	public ExitController exit;

	void Start () {		
		popupE.SetActive(false); // Skrytí ukazatele interakce
	}

	void Update () {
		
		// Neviditelné paprsky pro sběr
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsPlayer);
		RaycastHit2D hitInfoRightPickUp = Physics2D.Raycast(transform.position, Vector2.right, distanceP, whatIsPlayer);
		RaycastHit2D hitInfoLeftPickUp = Physics2D.Raycast(transform.position, Vector2.left, distanceP, whatIsPlayer);
		// Neviditelné paprsky pro zobrazení 
		RaycastHit2D hitInfoRight = Physics2D.Raycast(transform.position, Vector2.right, distanceH, whatIsPlayer);
		RaycastHit2D hitInfoLeft = Physics2D.Raycast(transform.position, Vector2.left, distanceH, whatIsPlayer);

		// Kontrola kolize paprsků Raycast 
		if (hitInfo.collider != null || hitInfoRight.collider != null || hitInfoLeft.collider != null) {
			popupE.SetActive(true); // Odkrytí ukazatele interakce
			if (hitInfo.collider != null || hitInfoRightPickUp.collider != null || hitInfoLeftPickUp.collider != null) {
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
		if (isPressed == true && (hitInfo.collider != null || hitInfoRightPickUp.collider != null || hitInfoLeftPickUp.collider != null)) {		
			Destroy(gameObject); // Zničení objektu
			exit.WaterCI();

			// For cyklus pro snížení hodnoty v proměnné 'thrist' ve scriptu PlayerController
			for (int i = 0; i < 20; i++) {
				if (PlayerController.thirst < PlayerController.maxThirst && PlayerController.thirst > 0)
					PlayerController.thirst--;
			}
		}

	}
}