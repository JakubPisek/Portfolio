using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour {

	// PUBLIC proměnné
	public int width;
	public int height;

	// Setter pro nastavení šířky
	public void SetWidth (int newWidth) {
		width = newWidth;
	}

	// Setter pro nastavení výšky
	public void SetHeight (int newHeight) {
		height = newHeight;
	}

	// Setter pro nastavení rozlišení
	public void SetRes () {
		Screen.SetResolution(width, height, false);
	}
}