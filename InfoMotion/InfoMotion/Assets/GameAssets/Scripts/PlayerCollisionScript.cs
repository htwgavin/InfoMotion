using UnityEngine;

// Die Klasse verarbeitet Kollisionen zwischen dem Spieler und anderen Objekten.
public class PlayerCollisionScript : MonoBehaviour {

	// Tag der geprüft werden soll, ob der Spieler mit ihm kollidiert
	private string tag = "Obstacle";

	// Funktion wird aufgerufen, wenn der Spieler ein anderes Objekt trifft
	void OnCollisionEnter (Collision collisionInfo) {
		// testen, ob der Spieler mit einem Objekt mit dem Tag 'tag' zusammengestoßen ist 
		if (collisionInfo.collider.tag == tag) {
			FindObjectOfType<GameManagerScript> ().EndGame ();
		}

	}
}
