using UnityEngine;
using System.Collections;

public class PersonBehavior : MonoBehaviour {

	float speed;
	float size;

	public float MinSize = 0.75f;
	public float MaxSize = 1.25f;

	public float MinSpeed = 0.7f;
	public float MaxSpeed = 1.3f;

	// Use this for initialization
	void Start () {
		// Give person a randomized set of features
		// eg. speed, size, infection length...

		// speed and size are multipliers
		speed = Random.Range(this.MinSpeed, this.MaxSpeed);
		size = 1.0F;//Random.Range (this.MinSize, this.MaxSpeed);

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
