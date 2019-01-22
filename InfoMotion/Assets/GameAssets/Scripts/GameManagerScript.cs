using UnityEngine;
using UnityEngine.SceneManagement;
using Leap;
using Leap.Util;

// Die Klasse regelt die einzelnen 'States' des Spiels.
public class GameManagerScript : MonoBehaviour {

	public HandController hc;
	private bool grabGestureActive = false;

	private bool gameHasEnded = false;
	private float waitTime = 3.0f;

	// Update is called once per frame
	void Update () {
		Frame frame = hc.GetFrame ();
		if (frame.Hands.Count == 1) {
			// Steuerung des Cursors
			foreach (Hand hand in frame.Hands) {

				// Grab-Gesture
				if (grabGesture(hand)) {
					SceneManager.LoadScene (1, LoadSceneMode.Single);
					break;
				} 
			}
		}
	}

	// Methode wird aufgerufen, wenn das Spiel beendet wurde.
	public void EndGame () {
		if (!gameHasEnded) {
			gameHasEnded = true;

			// Steuerung deaktivieren
			FindObjectOfType<PlayerMovement>().enabled = false;

			Invoke("Reload", waitTime);
		} 
	}

	// lädt die aktuelle Szene neu
	public void Reload () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public static void LoadScene(int index) {
		SceneManager.LoadScene (index);
	}

	// Returns true at the start of the gesture
	bool grabGesture (Hand h) {
		float grabStrength = h.GrabStrength;
		float limit = 0.9f;
		float direction = -0.8f;
		FingerList fingers = h.Fingers;

		if (grabStrength > limit && !grabGestureActive && h.PalmNormal.y < direction) {
			grabGestureActive = true;
			return true;
		} else if (grabStrength < limit && grabGestureActive) {
			grabGestureActive = false;
			return false;
		} else {
			return false;
		}
	}
}
