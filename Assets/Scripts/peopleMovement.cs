﻿using UnityEngine;
using System.Collections;

public class peopleMovement : MonoBehaviour {
	private GameController gameController;

	private int mapSize_X;
	private int mapSize_Y;

	private Vector2 target;
	public bool infected = false;
	private float infectionTime = 3.0F;
	private bool hasBeenInfected = false;

	public Sprite normalSprite;
	public Sprite infectedSprite;
	public Sprite usedSprite;
	SpriteRenderer rend;

	public float InfectionLength = 3.0f; // can be set in editor


	// Use this for initialization
	void Start () {
		rend = GetComponentInChildren<SpriteRenderer>();
		gameController = FindObjectOfType<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		move ();
		if (infected) {
			infectionTime -= Time.deltaTime;
			if (infectionTime <= 0) {
				rend.sprite = usedSprite;
				hasBeenInfected = true;
				infected = false;

				gameController.OnInfectionEnded ();
			}
		}
	}

	public void setMapSize (int x, int y) {
		mapSize_X = x;
		mapSize_Y = y;
	}

	private void move() {
		Rotate ();
		if (Mathf.Round(transform.position.x) != Mathf.Round(target.x) && Mathf.Round(transform.position.y) != Mathf.Round(target.y)) {
		//transform.Translate(Vector2.right * (2 * Time.deltaTime));
			Vector3 pos = transform.position;
			float mvspeed = 1F;
			if (transform.position.x < target.x) {
				pos.x += mvspeed * Time.deltaTime;
			} else if (transform.position.y < target.y) {
				pos.y += mvspeed * Time.deltaTime;
			}  
			if (transform.position.x > target.x) {
				pos.x -= mvspeed * Time.deltaTime;
			}
			else if (transform.position.y > target.y) {
				pos.y -= mvspeed * Time.deltaTime;
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
		rend.sprite = infectedSprite;

		gameController.OnNewInfection ();
	}

	public Vector2 setTargetPos() {
		Vector2 pos;
		if (!infected)
			pos = new Vector2 (Random.Range ((mapSize_X / 2) * -1, mapSize_X / 2), Random.Range ((mapSize_Y / 2) * -1, mapSize_Y / 2));
		else {
			GameObject[] people = GameObject.FindGameObjectsWithTag ("people");
			GameObject go = people [Random.Range (0, people.Length)];
			if (go == this.gameObject || go.GetComponent<peopleMovement> ().isInfected ()) {
				return Vector2.zero;
			}
			Vector3 goPos = go.transform.position;
			pos.x = goPos.x;
			pos.y = goPos.y;
		}
		return pos;
	}

	public void setTargetPos (Vector2 newpos) {
		target = newpos;
	}

	private void checkCollision () {
		RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, 0.5F, Vector2.zero);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider.gameObject != this.gameObject && hit.collider.gameObject.tag == "people") {
				if (infected) {
					hit.collider.gameObject.GetComponent<peopleMovement> ().setInfected();
				}
				/*Vector3 newPos = Vector3.MoveTowards(transform.position, hit.collider.gameObject.transform.position, -2.0F);
				target.x = newPos.x;
				target.y = newPos.y;
				*/
			}
		}
	}

	public bool isInfected() {
		return this.infected;
	}
		
}
