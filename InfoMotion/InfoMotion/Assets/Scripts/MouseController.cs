﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Leap.Util;


	
public class MouseController : MonoBehaviour {

	public GameObject canvas;
	public HandController hc;
	public GameObject scriptManager;
	private panelScript pS;

	// Menus
	public GameObject mainMenu;
	private List<Button> mainButtons = new List<Button>();

	// Panels
	public GameObject musicPanel;
	public GameObject moviePanel;
	public GameObject radioPanel;
	public GameObject navigationPanel;

	public Button gamesButton;

	// Movies
	public GameObject movieOne;
	public GameObject movieTwo;

	// Toggles
	public GameObject toggleMenu;
	public GameObject giveControlMenu;
	public GameObject takeControlMenu;

	private Vector2Int handPosition;
	private int canvasWidth, canvasHeight;

	private bool clickGestureActive = false;
	private bool grabGestureActive = false;

	private bool automaticDrivingActive = true;

	private const int leapCordSystemCap = 200;

	// Use this for initialization
	void Start () {
		
		// Gesten aktivieren
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPESWIPE);

		// Breite & Höhe des Fensters
		var rect = canvas.transform as RectTransform;
		canvasWidth = Mathf.RoundToInt (rect.rect.width);
		canvasHeight = Mathf.RoundToInt (rect.rect.height);
		//Debug.Log ("canvas width: " + rect.rect.width + "; canvas height: " + rect.rect.height);

		// ScriptManager
		pS = scriptManager.GetComponent<panelScript>();

		// alle Buttons holen aus dem MainMenu
		GameObject[] btns = GameObject.FindGameObjectsWithTag("MainButton");
		foreach (GameObject obj in btns) {
			mainButtons.Add (obj.GetComponent<Button>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = hc.GetFrame ();

		if (!giveControlMenu.activeInHierarchy && !takeControlMenu.activeInHierarchy) {
			// Gesten
			GestureList gestures = frame.Gestures ();
			foreach (Gesture g in gestures) {
				// Swipe Geste
				if (g.Type == Gesture.GestureType.TYPESWIPE) {
					SwipeGesture swipeGesture = new SwipeGesture (g);
					Vector swipeDirection = swipeGesture.Direction;

					if (Mathf.Abs (swipeDirection.x) < Mathf.Abs (swipeDirection.y)) {
						if (swipeDirection.y < 0) {
							// Debug.Log ("Runter");
							if (!navigationPanel.activeInHierarchy && gamesButton.interactable == true) {
								pS.showhideNavigationPanel ();
							}
						} else if (swipeDirection.y > 0) {
							// Debug.Log ("Hoch");
							if (navigationPanel.activeInHierarchy) {
								pS.showhideNavigationPanel ();
							}
						} 
					} 
				} 
			}
		}

		if (frame.Hands.Count == 1) {
			// Steuerung des Cursors
			foreach (Hand hand in frame.Hands) {

				// Click-Gesture
				if (clickGesture(hand)) {
					//Debug.Log ("Click!");
					if (mainMenu.activeInHierarchy) {
						clickedMainButton (transform.position);
					} else if (movieOne.activeInHierarchy || movieTwo.activeInHierarchy) {
						clickedButton (transform.position, "movieButtons");
					} else {
						clickedButton (transform.position, "playButton");
					}

					break;
				} 

				// Grab-Gesture
				if (grabGesture(hand)) {
					// Debug.Log ("Grab!");
					if (!mainMenu.activeInHierarchy) {
						if (musicPanel.activeInHierarchy) {
							pS.showhideMusicPanel ();
						} else if (radioPanel.activeInHierarchy) {
							pS.showhideRadioPanel ();
						} else if (moviePanel.activeInHierarchy) {
							pS.showhideMoviesPanel ();
						} else if (movieOne.activeInHierarchy) {
							pS.showhideMovies1 ();
						} else if (movieTwo.activeInHierarchy) {
							pS.showhideMovies2 ();
						}
					}
					break;
				} 

				// Palm Position holen (x,y,z)
				handPosition.x = Mathf.RoundToInt (hand.PalmPosition.x);
				handPosition.y = Mathf.RoundToInt (hand.PalmPosition.y);
				// ungefähr x (-100 : 100 ) y (0 : 200) z (egal)

				// handPosition x von 0 - 200
				handPosition.x += 100;
				if (handPosition.x < 0)
					handPosition.x = 0;
				if (handPosition.x > 200)
					handPosition.x = 200;

				// handPosition y von 50 - 250
				if (handPosition.y < 50)
					handPosition.y = 50;
				if (handPosition.y > 250)
					handPosition.y = 250;
				handPosition.y -= 50;

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

	// Returns true at the start of the gesture
	bool clickGesture (Hand h) {
		FingerList fingers = h.Fingers;

		// Mindestens 2 Finger müssen zu sehen sein
		if (fingers.Count > 1) { 

			// Position von Daumen & Zeigefinger holen (wenn sie gestreckt sind)
			Vector3 thumbDirection = Vector3.zero;
			Vector3 indexDirection = Vector3.zero;
			bool thumbShown = false, indexShown = false;

			foreach (Finger f in fingers) {

				// Daumen
				if (f.Type == Finger.FingerType.TYPE_THUMB) {
					thumbDirection = new Vector3 (f.Direction.x, f.Direction.y, f.Direction.z);
					thumbShown = true;
				} 

				// Zeigefinger
				if (f.Type == Finger.FingerType.TYPE_INDEX) {
					indexDirection = new Vector3 (f.Direction.x, f.Direction.y, f.Direction.z);
					indexShown = true;
				}
			}
				
			if (thumbShown && indexShown) {
				// wenn Skalarprodukt > 0.9, dann sind die Richtungsvektoren ähnlich genug
				float dotProduct = Vector3.Dot (thumbDirection, indexDirection);
				float limit = 0.90f;

				if (dotProduct > limit && !clickGestureActive) {
					clickGestureActive = true;
					return true;
				} else if (dotProduct <= limit && clickGestureActive) {
					clickGestureActive = false;
					return false;
				} else {
					return false;
				}
			} else {
				return false;
			}

		} else {
			clickGestureActive = false;
			return false;
		}
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
		
	private void clickedMainButton (Vector3 screenPoint) {
		if (!giveControlMenu.activeInHierarchy && !takeControlMenu.activeInHierarchy) {
			// Check if button is pressed
			foreach (Button b in mainButtons) {
				Collider2D buttonCollider = b.GetComponent<Collider2D> ();
				if (buttonCollider != null && buttonCollider.bounds.Contains (screenPoint) && b.interactable) {
					b.onClick.Invoke ();
					return;
				}
			}

			// Check if drive toggle is pressed
			Collider2D toggleCollider = toggleMenu.GetComponent<Collider2D> ();
			if (toggleCollider != null && toggleCollider.bounds.Contains (screenPoint)) {
				Toggle temp = toggleMenu.GetComponent<Toggle> ();
				temp.isOn = !temp.isOn; 
				return;
			}
		} else if (giveControlMenu.activeInHierarchy || takeControlMenu.activeInHierarchy) {
			GameObject temp = GameObject.FindGameObjectWithTag ("okButton");
			Button b = temp.GetComponent<Button> ();
			if (b != null) {
				Collider2D controlCollider = b.GetComponent<Collider2D> ();
				if (controlCollider != null && controlCollider.bounds.Contains (screenPoint)) {
					automaticDrivingActive = !automaticDrivingActive;
					b.onClick.Invoke ();
				}
			}
		} 
	}

	private void clickedButton (Vector3 screenPoint, string tagName) {
		// alle Buttons holen aus dem aktiven Menü
		List<Button> playButton = new List<Button>();
		GameObject[] btns = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject obj in btns) {
			playButton.Add(obj.GetComponent<Button>());
		}

		foreach (Button b in playButton) {
			Collider2D buttonCollider = b.GetComponent<Collider2D> ();
			if (buttonCollider != null && buttonCollider.bounds.Contains (screenPoint) && b.interactable) {
				b.onClick.Invoke ();
				return;
			}
		}
	}
}

