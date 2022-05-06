using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour {

	// PUBLIC proměnné
	public int foodC;
	public int waterC;
	public int hygieneC;

	// PUBLIC komponenty/objekty
	public GameObject bed;

	void Start () {
		bed.SetActive(false);
    }

    void Update() {     
		
		if (foodC == 2 && waterC == 2 && hygieneC == 2) {
			bed.SetActive(true);
		}
		
    }

	public void FoodCI () {
		foodC++;
	}

	public void WaterCI () {
		waterC++;
	}

	public void HygieneCI () {
		hygieneC++;
	}
}
