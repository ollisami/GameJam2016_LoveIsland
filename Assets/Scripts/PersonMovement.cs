﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonMovement : MonoBehaviour {
	private GameController gameController;

	private float mapSize_X;
	private float mapSize_Y;

	private Vector2 target;
	public bool infected = false;
	private float infectionTime = 3.0F;
	private bool hasBeenInfected = false;

	// Features generated by PersonBehavior
	private float speedMultiplier;

	public Sprite normalSprite;
	public Sprite infectedSprite;
	public Sprite usedSprite;
	SpriteRenderer rend;

	public float InfectionLength = 3.0f; // can be set in editor
	private GameObject targetObject = null;
	private Transform chasingTrans; 
	public int dir = 0;

	//POWERUPS 
	private bool speedUpMode = false;
	private float speedUpTimer = 3.0F;

	public GameObject HeartDropPrefab;


	// Use this for initialization
	void Start () {
		rend = GetComponentInChildren<SpriteRenderer>();
		gameController = FindObjectOfType<GameController> ();

		speedMultiplier = GetComponentInChildren<PersonBehavior> ().getSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
		move ();
		if (infected) {
			infectionTime -= Time.deltaTime;
			if (infectionTime <= 0) {
				hasBeenInfected = true;
				infected = false;

				gameController.OnInfectionEnded ();
				Instantiate (HeartDropPrefab, this.transform.position, Quaternion.identity);
			}
		}

		if (speedUpMode) {
			speedUpTimer -= Time.deltaTime;
			if (speedUpTimer <= 0) {
				speedMultiplier = speedMultiplier / 2F;
				speedUpMode = false;
			}
		}
	}

	public void setMapSize (float x, float y) {
		mapSize_X = x;
		mapSize_Y = y;
	}

	private void move() {
		if (infected)
			setTargetPos ();
		if (Mathf.Round(transform.position.x) != Mathf.Round(target.x) || Mathf.Round(transform.position.y) != Mathf.Round(target.y)) {
		//Liikkuminen x ja y akselilla diagonaalien sijaan?
			Vector3 pos = transform.position;
			float mvspeed = 1F * speedMultiplier;
			if (Mathf.Round(transform.position.x) < Mathf.Round(target.x)) {
				pos.x += mvspeed * Time.deltaTime;
				dir = 270;
			} else if (Mathf.Round(transform.position.y) < Mathf.Round(target.y)) {
				pos.y += mvspeed * Time.deltaTime;
				dir = 0;
			}  else if (Mathf.Round(transform.position.x) > Mathf.Round(target.x)) {
				pos.x -= mvspeed * Time.deltaTime;
				dir = 90;
			}
			else if (transform.position.y > target.y) {
				pos.y -= mvspeed * Time.deltaTime;
				dir = 180;
			}
			transform.position = pos;
			checkCollision ();
			setSprite ();
		} else {
			setTargetPos ();
		}
	}


		

	public void setInfected () {
		if (infected || hasBeenInfected) {
			return;
		}

		infected = true;
		infectionTime = this.InfectionLength;
		gameController.OnNewInfection ();
	}
		

	public void setTargetPos() {
		if (!infected && chasingTrans != null) {
			Vector3 dir = transform.position - chasingTrans.position;
			dir.Normalize ();
			target = new Vector2 (transform.position.x + dir.x, transform.position.y + dir.y);
			chasingTrans = null;
		}
		Vector2 pos = Vector2.zero;
		pos = new Vector2 (Random.Range ((mapSize_X / 2) * -1, mapSize_X / 2), Random.Range ((mapSize_Y / 2) * -1, mapSize_Y / 2));
		if (infected) {
			if (targetObject == null || targetObject.GetComponent<PersonMovement> ().isInfected ()) {
				GameObject[] people = GameObject.FindGameObjectsWithTag ("people");
				for (int i = 0; i < people.Length; i++) {
					if (people [i].GetComponent<PersonMovement> ().isInfected () == false) {
						targetObject = people [i];
					}
				}
			}
			if (targetObject != null) {
				pos.x = targetObject.transform.position.x;
				pos.y = targetObject.transform.position.y;
			}
		}
		target = pos;

	}
		

	private void checkCollision () {
		Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, 1.0F);
		foreach (Collider2D hit in hits) {
			if (hit.gameObject != this.gameObject && hit.gameObject.tag == "people") {
				if (infected && Vector3.Distance(transform.position, hit.transform.position) < 0.4F) {
					hit.gameObject.GetComponent<PersonMovement> ().setInfected ();
				} else if (!hasBeenInfected && hit.gameObject.GetComponent<PersonMovement> ().isInfected ()) {
					//Juokse karkuun
					chasingTrans = hit.transform;
				}

			}
		}
	}

	public bool isInfected() {
		if (hasBeenInfected)
			return true;
		return this.infected;
	}

	private void setSprite() {
		if (infected) {
			rend.sprite = infectedSprite;

			if (infectionTime < 1.5f) {
				rend.sprite = infectionTime % 0.4 > 0.2 ? usedSprite : infectedSprite; 
			}
		} else if (hasBeenInfected) {
			rend.sprite = usedSprite;
		} else {
			rend.sprite = normalSprite;
		}
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z = dir;
		transform.eulerAngles = euler;

	}

	public void speedUp() {
		if (!speedUpMode && infected) {
			speedUpTimer = 3F;
			speedMultiplier = speedMultiplier * 2F;
			speedUpMode = true;
		}
	}
		
}
