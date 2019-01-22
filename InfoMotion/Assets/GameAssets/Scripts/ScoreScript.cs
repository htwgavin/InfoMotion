using UnityEngine;
using UnityEngine.UI;

// Die Klasse zeigt den Spielstand an.
public class ScoreScript : MonoBehaviour {

	private Text scoreText; // Referenz zur Text-Komponente

	// Funktion wird einmalig am Anfang ausgeführt
	void Start () {
		scoreText = GetComponent<Text> ();
	}

	// Funktion wird jeden Frame ein Mal aufgerufen
	void Update () {
		// die zurückgelegte Distanz des Spielers wird als ganze Zahl im Textfeld wiedergegeben
		scoreText.text = PlayerMovement.distanceTravelled.ToString("0");
	}
}
