using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	#region VAR
	// PRIVATE proměnné
	[SerializeField] private float originalSpeed = 4.0f;
	[SerializeField] private float sprintSpeed = 6.0f;
	private float moveHorizontal;
	private float moveVertical;
	private float movementSpeed;
	private bool isClimbing;
	private bool isFalling;
	private string deathReason;

	// PUBLIC proměnné
	public float distance; // dosah paprsku
	public LayerMask whatIsLadder;
	// PUBLIC STATIC proměnné										
	public static float maxHunger = 100.0f;
	public static float maxThirst = 100.0f;
	public static float maxHygiene = 100.0f;
	public static float maxStamina = 100.0f;
	public static float hunger, thirst, hygiene, stamina;
	
	// PRIVATE komponenty/objekty
	private Rigidbody2D rb;
	private SpriteRenderer sR;

	// PUBLIC komponenty/objekty
	public GameObject panel;
	public Animator animator;
	public DeathObserver dO;
	#endregion VAR
	
	// Start se zavolá při vytvoření objektu
	void Start () {
		// Načtení komponentů objektu
		rb = GetComponent<Rigidbody2D>();
		sR = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		// Skrytí panelu
		panel.SetActive(false); 

		// Nastavení defaultních hodnot
		SetOriginalStats();
	}

	// Update se volá každý jeden snímek
	void Update () {
		// Přičítání potřeb
		if (hunger < maxHunger) {
			hunger += 1.0f * Time.deltaTime;
		}

		if (thirst < maxThirst) {
			thirst += 0.8f * Time.deltaTime;
		}

		if (hygiene < maxHygiene) {
			hygiene += 0.5f * Time.deltaTime;
		}

		// Kontrola smrti
		if (hunger >= maxHunger || thirst >= maxThirst || hygiene >= maxHygiene) {
			Die();
		}

	}

	// FixedUpdate lépe řeší fyziku - pohyb, gravitaci, výstřely
	void FixedUpdate () {

		#region MOVEMENT 
		// Načítání hodnot z klávesnice - horizontální input (A, D, ←, →)
		moveHorizontal = Input.GetAxisRaw("Horizontal");

		// Přetočení postavy podle směru chůze
		if (moveHorizontal < 0) {
			sR.flipX = true;
		}
		else if (moveHorizontal > 0) {
			sR.flipX = false;
		}

		// Animace	
		animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

		// Předání hodnot do Rigidbody2D
		Vector2 movement = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);
		rb.velocity = movement;
		#endregion

		#region SPRINT
		// Podmínka - zda hráč drží Levý Shift a pohybuje se
		if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && moveHorizontal != 0 && isClimbing != true) {		
			movementSpeed = sprintSpeed;
			stamina -= 10.0f * Time.deltaTime;
			thirst += 5.0f * Time.deltaTime;
		}
		else {
			movementSpeed = originalSpeed;	
			// Stamina se doplňuje, pokud hráč neběží a stamina není plná
			if (stamina < maxStamina) {
				stamina += Time.deltaTime;
			}
		}

		if (stamina <= 0) {
			movementSpeed = originalSpeed;
		}
		#endregion

		#region LADDER
		// Vystřelení neviditelného paprsku
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

		// Kontrola kolize paprsků Raycast 
		if (hitInfo.collider != null) {
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) {
				isClimbing = true;
			}
			else { 
				// Pokud se zaznamená horizontální input
				if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
					isClimbing = false;
				}
			}
		}
		
		// Zjistění, jestli hráč padá
		if (rb.velocity.y <= - 1) {
			isFalling = true;
		}
		else if (rb.velocity.y > - 1) {
			isFalling = false;
		}

		// Pohyb po žebříku
		if (isClimbing == true && hitInfo.collider != null) {
			// Načítání hodnot z klávesnice - vertikální input (W, S, ↑, ↓)
			moveVertical = Input.GetAxisRaw("Vertical");
			// Předání hodnot do Rigidbody2D
			rb.velocity = new Vector2(rb.velocity.x, moveVertical * movementSpeed);
			rb.gravityScale = 0;
			// Animace
			animator.SetBool("IsClimbing", true);
		}
		else {
			rb.gravityScale = 5;
			isClimbing = false;
			// Animace
			if (isFalling) {
				animator.SetBool("IsClimbing", true);
			}
			else {
				animator.SetBool("IsClimbing", false);
			}
		}
		#endregion

	}

	// Metoda pro inkrementaci proměnné
	public void HygieneIncrement () {
		hygiene += 20.0f;
	}

	// Metoda pro nastavení proměnných na původní hodnoty
	public void SetOriginalStats () {
		thirst = 0;
		hunger = 0;
		hygiene = 0;
		stamina = maxStamina;
		movementSpeed = originalSpeed;
	}

	// Metoda řešící smrt hráče
	public void Die () {
		// Zjištění důvodu smrti
		if (hunger >= maxHunger) {
			deathReason = "Hunger";
		}
		else if(thirst >= maxThirst) {
			deathReason = "Thirst";
		}
		else if(hygiene >= maxHygiene) {
			deathReason = "Hygiene";
		}

		// Předání parametru do metody jiného objektu
		dO.SetObserver(deathReason);
		// Zavolání metody a načtení scény
		SetOriginalStats();
		SceneManager.LoadScene("Death");
	}
}