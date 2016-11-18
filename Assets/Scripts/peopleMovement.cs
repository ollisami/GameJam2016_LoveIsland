using UnityEngine;
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
			}
		}
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
			checkCollision ();
			transform.position = pos;
		} else {
			target = setTargetPos ();
		}
	}
		

	public void setInfected () {
		if (infected || hasBeenInfected) {
			return;
		}

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

	private void checkCollision () {
		RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, 0.8F, Vector2.zero);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider.gameObject != this.gameObject && hit.collider.gameObject.tag == "people") {
				if (infected) {
					hit.collider.gameObject.GetComponent<peopleMovement> ().setInfected();
				}
				setTargetPos();
			}
		}
	}

	public bool isInfected() {
		return this.infected;
	}
		
}
