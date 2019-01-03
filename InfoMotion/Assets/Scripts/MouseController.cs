using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Util;

public class MouseController : MonoBehaviour {

	public GameObject canvas;

	private Controller controller;
	private Vector2Int handPosition;
	private int canvasWidth, canvasHeight;

	private const int leapCordSystemCap = 200;

	// Use this for initialization
	void Start () {
		controller = new Controller ();

		var rect = canvas.transform as RectTransform;
		canvasWidth = Mathf.RoundToInt (rect.rect.width);
		canvasHeight = Mathf.RoundToInt (rect.rect.height);
		Debug.Log ("canvas width: " + rect.rect.width + "; canvas height: " + rect.rect.height);
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame ();
		foreach (Hand hand in frame.Hands) {
			if (hand.IsRight || hand.IsLeft) {
				// Palm Position holen (x,y,z)
				handPosition.x = Mathf.RoundToInt (hand.PalmPosition.x);
				handPosition.y = Mathf.RoundToInt (hand.PalmPosition.y);
				// ungefähr x (-100 : 100 ) y (0 : 200) z (egal)

				// handPosition x von 0 - 200
				handPosition.x += 100;
				if (handPosition.x < 0) handPosition.x = 0;
				if (handPosition.x > 200) handPosition.x = 200;

				// handPosition y von 0 - 200
				if (handPosition.y < 0) handPosition.y = 0;
				if (handPosition.y > 200) handPosition.y = 200;

				// relative Position der Koordinaten berechnen
				float relPositionX = ((float)handPosition.x) / leapCordSystemCap;
				float relPositionY = ((float)handPosition.y) / leapCordSystemCap;

				// Position auf dem Canvas berechnen
				int canvasPositionX = Mathf.RoundToInt (canvasWidth * relPositionX);
				int canvasPositionY = Mathf.RoundToInt (canvasHeight * relPositionY);
				//Debug.Log ("canvas x: " + canvasPositionX + ", canvas y: " + canvasPositionY);

				// Cursor an richtige Stelle verschieben
				transform.position = new Vector3 (canvasPositionX, canvasPositionY, 0.0f);

			} 
		}
	}
}
