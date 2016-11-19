using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	private int DECORATION_COUNT = 5;

	GameObject gameObject;

	public Transform palmTree;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < DECORATION_COUNT; i++) {
			float x = Random.Range (-4, 4);
			float y = Random.Range (-4, 4);

			Instantiate (palmTree, new Vector3 (x, y, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
