using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonClothing : MonoBehaviour {

	public List<Sprite> clothingSprites;

	// Use this for initialization
	void Start () {
		// 50% of people have clothing decorations
		if (Random.Range (0f, 1f) > 0.5) {
			int frame = Random.Range (0, clothingSprites.Count);

			GetComponent<SpriteRenderer> ().sprite = clothingSprites [frame];
		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
