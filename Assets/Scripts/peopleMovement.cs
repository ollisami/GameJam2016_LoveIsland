using UnityEngine;
using System.Collections;

public class peopleMovement : MonoBehaviour {

	private int mapSize_X;
	private int mapSize_Y;

	private bool isInfected = false;

	private Vector2 target;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		move ();
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
			target =  new Vector2(Random.Range((mapSize_X/2)*-1, mapSize_X/2), Random.Range((mapSize_Y/2)*-1, mapSize_Y/2));
		}
	}

	public void Infect() {
		isInfected = true;
		this.GetComponentInChildren<SpriteRenderer> ().color = Color.green; // just temporary to visualize which are infected and which are not
		// todo: start moving towards non-infected people?
		Debug.Log ("Infected!");
	}
}
