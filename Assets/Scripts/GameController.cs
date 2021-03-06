﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {
	private InfectionStart infectionStart = new LoveProjectileInfectionStarter();

	private int infectedCount = 0;
	private int infectionsTotal = 0;

	public float mapSize_X { get { return 10 * GameObject.Find ("Map").transform.localScale.x; } }
	public float mapSize_Y { get { return 10 * GameObject.Find ("Map").transform.localScale.y; } }

	// when user clicks somewhere, this will be set true
	private bool hasStartedInfection = false;

	public GameObject[] peoplePrefabs;
	public List<PersonMovement> personMovments = new List<PersonMovement>();

	public int PeopleToSpawn = 6;

	// Game can be temporarily frozen with a powerup
	private bool frozen = false;
	private float freezeTimeLeft = 5f;

	public int leve_id = 0;

	public bool HasGameEnded {
		get {
			// if infection is started and there are no infected people OR  
			// everyone has been infected, then the game is over
			return hasStartedInfection && ((infectedCount == 0 && FindObjectsOfType<LoveProjectile>().Length == 0) ||
				infectionsTotal == PeopleToSpawn);
		}
	}

	public int TotalInfections {
		get { return infectionsTotal; } 
	}

	public bool isFrozen {
		get { return frozen; }
		set { frozen = value; }
	}

	// Use this for initialization
	void Start () {
		spawnPeople (this.PeopleToSpawn);
	}

	// Update is called once per frame
	void Update () {
		if (!hasStartedInfection && Input.GetKeyDown (KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()) {
			startInfection (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}

		if (frozen && freezeTimeLeft <= 0) {
			frozen = false;
			freezeTimeLeft = 3f;
		} else {
			freezeTimeLeft -= Time.deltaTime;
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
		var wasInfectionStarted = infectionStart.StartInfection (this, infectionStartPoint);
		hasStartedInfection = wasInfectionStarted;
	}

	public void OnNewInfection() {
		infectedCount++;
		infectionsTotal++;
	}

	public void OnInfectionEnded() {
		infectedCount--;
	}

	public void freezeGame() {
		isFrozen = true;
		freezeTimeLeft = 3f;
	}

	public void setPowerup(int index) {
		if (index == 0) {
			//speed lovers
			if (CoinManager.Instance.UseCoins (5)) {
				foreach (PersonMovement p in personMovments) {
					p.speedUp ();
				}
			}
		}
		if (index == 1) {
			// Freeze powerup
			if (!isFrozen && CoinManager.Instance.UseCoins (7)) {
				freezeGame ();
			}
		}
		if (index == 2) {
			if (CoinManager.Instance.UseCoins (10)) {
				hasStartedInfection = false;
			}
		}
	}
}
