using UnityEngine;
using System.Collections.Generic;

// Die Klasse generiert die Plattformen + Hindernisse.
// Die Objekte werden hierbei recycled.
public class PlattformSpawnScript : MonoBehaviour {

	public Transform plattform; 	// Plattform prefab
	public Transform obstacle;		// Hindernis prefab
	public int numberOfObjects;		// Anzahl an Plattformen, die generiert werden
	public float obstacleOffsetZ;	// Hindernisoffset
	public float recycleOffset;		// Pixeloffset, danach wird die Plattform recycled
	public Vector3 startPosition;	// Startvektor für die erste Plattform

	private Vector3 nextPosition;
	private Queue<Transform> pQueue;
	private Queue<Transform> oQueue;
	private int numberOfObstacles;	
	private int obstaclesPerPlattform = 4;			// Anzahl der Hindernisse pro Plattform
	private int[] positionsX = { -6, -3, 0, 3, 6 }; // X Positionen der Hindernisse

	// Funktion wird einmalig am Anfang aufgerufen.
	void Start () {

		numberOfObstacles = numberOfObjects * obstaclesPerPlattform;
		pQueue = new Queue<Transform> (numberOfObjects);
		oQueue = new Queue<Transform> (numberOfObstacles);

		// Plattformobjekte instanzieren und zur Queue hinzufügen
		for (int i = 0; i < numberOfObjects; i++) {
			pQueue.Enqueue ((Transform)Instantiate(plattform));
		}
		// Hindernisobjekte instanzieren und zur Queue hinzufügen
		for (int i = 0; i < numberOfObstacles; i++) {
			oQueue.Enqueue ((Transform)Instantiate (obstacle));
		}

		nextPosition = startPosition;
		// jede Plattform wird zum Beginn ein Mal recycled  
		for (int i = 0; i < numberOfObjects; i++) {
			Recycle ();
		}
	}

	// Funktion wird ein Mal pro Frame aufgerufen.
	void FixedUpdate () {
		// wenn die erste Plattform komplett hinter dem Spieler ist, dann wird sie recycled 
		if (pQueue.Peek ().localPosition.z + recycleOffset < PlayerMovement.distanceTravelled) {
			Recycle ();
		}
	}
		
	private void Recycle () {
		Transform p = pQueue.Dequeue ();	// erstes Objekt aus der Plattform-Queue lokal speichern
		p.localPosition = nextPosition;		// Position ändern
		nextPosition.z += p.localScale.z;

		// Hindernisse zufällig anordnen
		int randomPos = Random.Range (0, positionsX.Length);	// zufällige Zahl zwischen 0 (inkl.) und 5 (exkl.) generieren
		// jedes Hindernis durchläuft diese Schleife
		for (int i = 0; i < positionsX.Length; i++) {
			// wenn der Index mit der zufällig generierten Zahl übereinstimmt, dann wird der nachfolgenden Teil übersprungen -> kein Hindernis
			if (randomPos != i) {
				Transform o = oQueue.Dequeue (); 	// erstes Objekt aus der Hindernis-Queue lokal speichern

				// Position des Hindernisses berechnen
				Vector3 oPosition;
				oPosition = nextPosition;
				oPosition.x = positionsX[i];
				oPosition.y = 0;
				oPosition.z -= (p.localScale.z / 2) - obstacleOffsetZ;

				o.localPosition = oPosition; 		// Hindernisposition ändern
				oQueue.Enqueue (o);					// Objekt wieder zur Queue hinzufügen
			}
		}

		pQueue.Enqueue(p); // Objekt wieder zur Queue hinzufügen
	}
}
