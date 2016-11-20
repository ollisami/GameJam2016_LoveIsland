using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public int DECORATION_COUNT = 15;

	GameObject gameObject;

	public Transform palmTree;

	// Use this for initialization
	void Start () {
		GameController controller = FindObjectOfType<GameController> ();
		for (int i = 0; i < DECORATION_COUNT; i++) {
			float x = Random.Range (-controller.mapSize_X / 2, controller.mapSize_X / 2);
			float y = Random.Range (-controller.mapSize_Y / 2, controller.mapSize_Y / 2);

			Instantiate (palmTree, new Vector3 (x, y, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
