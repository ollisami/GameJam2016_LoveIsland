using UnityEngine;
using System.Collections;

public class PersonBehavior : MonoBehaviour {

	float speed;
	float size;

	// Use this for initialization
	void Start () {
		// Give person a randomized set of features
		// eg. speed, size, infection length...

		// speed and size are multipliers
		speed = Random.Range(0.6f, 1.5f);
		size = Random.Range (0.60f, 1.25f);

		transform.localScale = new Vector3 (size, size, 0);
	}

	public float getSpeed() {
		return speed;
	}

	public float getSize() {
		return size;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
