using UnityEngine;

// Die Klasse lässt die Kamera den Spieler verfolgen.
public class FollowPlayer : MonoBehaviour {

	public Transform player; // Referenz zur Position des Spielers
	public Vector3 offset = new Vector3(0.0f, 1.0f, 5.0f); // Offset der Kamera zum Spieler

	// Funktion wird jeden Frame ein Mal aufgerufen
	void Update() {
		// Position der Kamera verändern 
		transform.position = player.position + offset;
	}
}
