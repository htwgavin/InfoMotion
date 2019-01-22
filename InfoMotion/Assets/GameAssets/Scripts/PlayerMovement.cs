using UnityEngine;
using Leap;
using Leap.Util;

// Die Klasse wird zur Steuerung des Spielers verwendet.
public class PlayerMovement : MonoBehaviour {

	public HandController hc;

	private Rigidbody rb; // Referenz zum Rigidbody
	private float handPosition = 0.0f;
	public float sidewaysSpeed = 100.0f; 
	public float forwardSpeed = 1000.0f;

	public static float distanceTravelled; // zurückgelegte Distanz des Spielers

	// Funktion wird einmalig am Anfang ausgeführt
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	// Funktion wird jeden Frame ein Mal aufgerufen
	void FixedUpdate () {
		Frame frame = hc.GetFrame ();
		if (frame.Hands.Count == 1) {
			// Steuerung des Cursors
			foreach (Hand hand in frame.Hands) {
				float temp = hand.PalmPosition.x;
				handPosition = temp / 100.0f; // geraten
			}
		}

		// Bewegung vorwärts
		rb.AddForce(0, 0, forwardSpeed * Time.deltaTime);

		// Bewegung zur Seite
		rb.AddForce(handPosition * (sidewaysSpeed * Time.deltaTime), 0, 0, ForceMode.VelocityChange);

		// Testen, ob der Ball von der Plattform gefallen ist
		if (transform.position.y < -0.5f) {
			FindObjectOfType<GameManagerScript>().EndGame();
		}

		// Workaround: Ball springt manchmal ohne Fremdeinwirkung -> Problem wird hiermit behoben
		if (transform.position.y > 0.0f) {
			float tempX = transform.position.x;
			float tempZ = transform.position.z;
			transform.position = new Vector3 (tempX, 0.0f, tempZ);
		}

		// Variable updaten
		distanceTravelled = transform.localPosition.z;
	}
}