using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Leap;
using Leap.Util;

public class MyScrollRect : MonoBehaviour {

    private ScrollRect myscrollRect;

	public HandController hc;
	private int currentGestureId = -1;

    public float switchSpeed;
    public float currentHorizontalPosition ;
    public float anfangPosition;
    public float startPosition;
    public float endPosition;
    public Button selectedButton;
    public Button firstButton;
    public Button lastButton;

    void Start() {
        myscrollRect = GetComponent<ScrollRect>();
        selectedButton.Select();
        myscrollRect.horizontalNormalizedPosition = anfangPosition;

		// Gesten aktivieren
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPESWIPE);
    }


    // Update is called once per frame
    void Update() {
		Frame frame = hc.GetFrame ();

		GestureList gestures = frame.Gestures ();
		foreach (Gesture g in gestures) {
			// Swipe Geste
			if (g.Type == Gesture.GestureType.TYPESWIPE && g.State == Gesture.GestureState.STATE_STOP && g.Id != currentGestureId) {
				currentGestureId = g.Id;
				SwipeGesture swipeGesture = new SwipeGesture (g);
				Vector swipeDirection = swipeGesture.Direction;

				if (Mathf.Abs (swipeDirection.x) > Mathf.Abs (swipeDirection.y)) {
					if (swipeDirection.x < 0) {
						// Debug.Log ("Links");
						swipeLeft ();
						break;
					} else if (swipeDirection.x > 0) {
						// Debug.Log ("Rechts");
						swipeRight ();
						break;
					} 
				} 
			}
		}
			
		if (Input.GetKeyDown ("left")) {
			swipeLeft ();
		} else if (Input.GetKeyDown("right")) {
			swipeRight ();
		}
    }
   
	public void swipeLeft() {
		if (currentHorizontalPosition <= startPosition) {
			myscrollRect.horizontalNormalizedPosition = endPosition;
			lastButton.Select();
		} else if (currentHorizontalPosition > startPosition) {
			myscrollRect.horizontalNormalizedPosition -= switchSpeed;
		}
		currentHorizontalPosition = myscrollRect.horizontalNormalizedPosition;
	}

	public void swipeRight() {
		if (currentHorizontalPosition >= endPosition) {
			myscrollRect.horizontalNormalizedPosition = startPosition;
			firstButton.Select();
		} else if (currentHorizontalPosition < endPosition) {
			myscrollRect.horizontalNormalizedPosition += switchSpeed;
		}
		currentHorizontalPosition = myscrollRect.horizontalNormalizedPosition;
	}
}
