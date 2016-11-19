﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private int infectedCount = 0;
	private int infectionsTotal = 0;

	private int mapSize_X = 10;
	private int mapSize_Y = 10;

	// when user clicks somewhere, this will be set true
	private bool hasStartedInfection = false;

	public GameObject[] peoplePrefabs;
	public List<PersonMovement> personMovments = new List<PersonMovement>();

	public int PeopleToSpawn = 6;

	public bool HasGameEnded {
		get {
			// if infection is started and there are no infected people OR  
			// everyone has been infected, then the game is over
			return hasStartedInfection && (infectedCount == 0 ||
				infectionsTotal == PeopleToSpawn);
		}
	}

	public int TotalInfections {
		get { return infectionsTotal; } 
	}

	// Use this for initialization
	void Start () {
		spawnPeople (this.PeopleToSpawn);
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
			PersonMovement p = go.GetComponent<PersonMovement> ();
			p.setMapSize (mapSize_X, mapSize_Y);
			personMovments.Add (p);
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

		foreach (var human in FindObjectsOfType<PersonMovement>()) {
			if (Vector2.Distance (human.transform.position, infectionStartPoint) < HumanRadius + infectionRadius) {
				human.setInfected();
			}
		}
	}

	public void OnNewInfection() {
		infectedCount++;
		infectionsTotal++;
	}

	public void OnInfectionEnded() {
		infectedCount--;
	}

	public void setPowerup(int index) {
		if (index == 0) {
			//speed lovers
			foreach (PersonMovement p in personMovments) {
				p.speedUp ();
			}
		}
	}
}
