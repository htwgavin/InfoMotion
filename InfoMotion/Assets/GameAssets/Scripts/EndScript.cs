using UnityEngine;
using UnityEngine.SceneManagement;

// Die Klasse wird aufgerufen, wenn "Play Again!"-Button gedrückt wurde.
public class EndScript : MonoBehaviour {

	// Spiel wird neu gestartet
	public void RestartGame () {
		GameManagerScript.LoadScene(1);
	}
}
