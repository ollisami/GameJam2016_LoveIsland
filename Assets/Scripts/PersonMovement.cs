﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonMovement : MonoBehaviour {
	private GameController gameController;

	private int mapSize_X;
	private int mapSize_Y;

	private Vector2 target;
	public bool infected = false;
	private float infectionTime = 3.0F;
	private bool hasBeenInfected = false;

	// Features generated by PersonBehavior
	private float speedMultiplier;

	public Sprite[] normalSprites;
	public Sprite[] infectedSprites;
	public Sprite[] usedSprites;
	SpriteRenderer rend;

	public float InfectionLength = 3.0f; // can be set in editor
	public GameObject targetObject = null;
	private int dir = 0;

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
				Instantiate (HeartDropPrefab, this.transform);
			}
		}
		setSprite ();
	}

	public void setMapSize (int x, int y) {
		mapSize_X = x;
		mapSize_Y = y;
	}

	private void move() {
		if (infected)
			target = setTargetPos ();
		//Rotate ();
		if (Mathf.Round(transform.position.x) != Mathf.Round(target.x) || Mathf.Round(transform.position.y) != Mathf.Round(target.y)) {
		//Liikkuminen x ja y akselilla diagonaalien sijaan?
			Vector3 pos = transform.position;
			float mvspeed = 1F * speedMultiplier;
			if (Mathf.Round(transform.position.x) < Mathf.Round(target.x)) {
				pos.x += mvspeed * Time.deltaTime;
				dir = 1;
			} else if (Mathf.Round(transform.position.y) < Mathf.Round(target.y)) {
				pos.y += mvspeed * Time.deltaTime;
				dir = 0;
			}  
			else if (Mathf.Round(transform.position.x) > Mathf.Round(target.x)) {
				pos.x -= mvspeed * Time.deltaTime;
				dir = 3;
			}
			else {//(transform.position.y > target.y) {
				pos.y -= mvspeed * Time.deltaTime;
				dir = 2;
			}
			transform.position = pos;
			checkCollision ();
		} else {
			target = setTargetPos ();
		}
	}

	const float Threshold = 1e-3f;
	void Rotate()
	{
		//Pitäis saada smootattuu?!?
		// Juoksee nopeammin kun zombeja lähettyvillä?
		Vector2 dir = transform.InverseTransformPoint(target);
		float angle = Vector2.Angle(Vector2.right, dir);
		angle = dir.y < 0 ? -angle : angle;
		if (Mathf.Abs(angle) > Threshold)
		{
			transform.Rotate(Vector3.forward, 500 * Time.deltaTime * Mathf.Sign(angle));
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

	public Vector2 setTargetPos() {
		Vector2 pos = Vector2.zero;
		if (!infected)
			pos = new Vector2 (Random.Range ((mapSize_X / 2) * -1, mapSize_X / 2), Random.Range ((mapSize_Y / 2) * -1, mapSize_Y / 2));
		else {
			if (targetObject == null || targetObject.GetComponent<PersonMovement>().isInfected()) {
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
		return pos;
	}

	public void setTargetPos (Vector2 newpos) {
		target = newpos;
	}

	private void checkCollision () {
		Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, 0.4F);
		foreach (Collider2D hit in hits) {
			if (hit.gameObject != this.gameObject && hit.gameObject.tag == "people") {
				if (infected) {
					hit.gameObject.GetComponent<PersonMovement> ().setInfected ();
				} else if (hit.gameObject.GetComponent<PersonMovement> ().isInfected ()) {
					//Juokse karkuun
				}

			}
		}
	}

	public bool isInfected() {
		return this.infected;
	}

	private void setSprite() {
		if (infected) {
			rend.sprite = infectedSprites [dir];
		} else if (hasBeenInfected) {
			rend.sprite = usedSprites [dir];
		} else {
			rend.sprite = normalSprites [dir];
		}
	}
		
}
