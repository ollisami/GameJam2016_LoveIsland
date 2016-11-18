using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private int infectedCount = 0;
	private int infectionsTotal = 0;

	private int mapSize_X = 10;
	private int mapSize_Y = 10;

	// when user clicks somewhere, this will be set true
	private bool hasStartedInfection = false;

	public GameObject[] peoplePrefabs;
	public Text totalInfectionsText;

	// Use this for initialization
	void Start () {
		spawnPeople (5);
	}

	// Update is called once per frame
	void Update () {
		if (!hasStartedInfection && Input.GetKeyDown (KeyCode.Mouse0)) {
			startInfection (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}
	}

	private void spawnPeople(int count) {
		for (int i = 0; i < count; i++) {
			Vector2 pos = getRandomPos ();
			GameObject go = Instantiate (peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], pos, Quaternion.identity) as GameObject;
			go.GetComponent<peopleMovement> ().setMapSize (mapSize_X, mapSize_Y);
		}
	}


	private Vector2 getRandomPos() {
		return new Vector2(Random.Range((mapSize_X/2)*-1, mapSize_X/2), Random.Range((mapSize_Y/2)*-1, mapSize_Y/2));
	}

	private void startInfection(Vector2 infectionStartPoint) {
		// todo: if the click position is out of map bounds then dont
		// set this to true or something?
		hasStartedInfection = true; 

		const float HumanRadius = 1; // the width and height of human is 1
		float infectionRadius = 0.1f; // todo: bigger radius if you have upgrades?

		foreach (var human in FindObjectsOfType<peopleMovement>()) {
			if (Vector2.Distance (human.transform.position, infectionStartPoint) < HumanRadius + infectionRadius) {
				human.setIfected();
			}
		}
	}

	public void OnNewInfection() {
		infectedCount++;
		infectionsTotal++;

		totalInfectionsText.text = "Lovers: " + infectionsTotal.ToString ();
	}

	public void OnInfectionEnded() {
		infectedCount--;
	}
}
