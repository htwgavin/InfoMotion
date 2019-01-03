using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Util;

public class MouseController : MonoBehaviour {

	Controller controller;
	int counterRight, counterLeft;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		counterRight = 0;
		counterLeft = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame ();
		foreach (Hand hand in frame.Hands) {
			if (hand.IsRight) {
				counterRight++;
				Debug.Log("Right: " + counterRight);
			}
			if (hand.IsLeft) {
				counterLeft++;
				Debug.Log("Left: " + counterLeft);
			}
		}
	}
}
