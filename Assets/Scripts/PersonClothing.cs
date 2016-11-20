using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonClothing : MonoBehaviour {

	public Sprite[] clothingSprites;
	public SpriteRenderer rend; 

	// Use this for initialization
	void Start () {
		// 50% of people have clothing decorations
		if (Random.Range (0f, 1f) > 0.5F) {
			int frame = Random.Range (0, clothingSprites.Length);
			rend.sprite = clothingSprites [frame];
		} else {
			rend.enabled = false;
		}
	}
}
