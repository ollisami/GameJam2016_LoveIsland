using UnityEngine;
using System.Collections;

public class Decoration : MonoBehaviour {

	SpriteRenderer rend;

	bool isCovered = false;

	// Use this for initialization
	void Start () {
		rend = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Color tmp = rend.color;

		tmp.a = isCovered ? 0.5f : 1f;
		rend.color = tmp;
	}
		
	void OnTriggerStay2D(Collider2D collider) {
		// Lower alpha when something is underneath

		if (collider.GetComponent<PersonMovement> () != null) {
			isCovered = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		// Show decoration entirely when nothing is under
		if (collider.GetComponent<PersonMovement> () != null) {
			isCovered = false;
		}
	}
}
