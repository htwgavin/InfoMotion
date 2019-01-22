using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap;
using Leap.Util;

public class ControllerScript : MonoBehaviour {

	public HandController hc;

	private bool grabGestureActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
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
