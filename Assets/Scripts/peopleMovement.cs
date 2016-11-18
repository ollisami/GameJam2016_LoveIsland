﻿using UnityEngine;
using System.Collections;

public class peopleMovement : MonoBehaviour {

	private int mapSize_X;
	private int mapSize_Y;

	private bool isInfected = false;

	private Vector2 target;
	public bool infected = false;
	private float infectionTime = 3.0F;


	public Sprite infectedSprite;
	SpriteRenderer rend;


	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		move ();
		if(infected)
			infectionTime -= Time.deltaTime;
	}

	public void setMapSize (int x, int y) {
		mapSize_X = x;
		mapSize_Y = y;
	}

	private void move() {
		if (Mathf.Round(transform.position.x) != Mathf.Round(target.x) && Mathf.Round(transform.position.y) != Mathf.Round(target.y)) {
			Vector3 pos = transform.position;
			pos.x = Mathf.Lerp(transform.position.x, target.x, 1 * Time.deltaTime);
			pos.y = Mathf.Lerp(transform.position.y, target.y, 1 * Time.deltaTime);
			transform.position = pos;
		} else {
			target = setTargetPos ();
		}
	}
		

	public void setIfected () {
		infected = true;
		infectionTime = 3.0F;
		rend.sprite = infectedSprite;
	}

	private Vector2 setTargetPos() {
		Vector2 pos;
		if (!infected)
			pos = new Vector2 (Random.Range ((mapSize_X / 2) * -1, mapSize_X / 2), Random.Range ((mapSize_Y / 2) * -1, mapSize_Y / 2));
		else {
			GameObject[] people = GameObject.FindGameObjectsWithTag ("people");
			GameObject go = people[Random.Range(0,people.Length)];
			Vector3 goPos = go.transform.position;
			pos.x = goPos.x;
			pos.y = goPos.y;
		}
		return pos;
	}

	public void Infect() {
		isInfected = true;
		this.GetComponentInChildren<SpriteRenderer> ().color = Color.green; // just temporary to visualize which are infected and which are not
		// todo: start moving towards non-infected people?
		Debug.Log ("Infected!");
	}
}
