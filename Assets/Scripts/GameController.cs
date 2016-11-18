using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int mapSize_X = 10;
	private int mapSize_Y = 10;


	public GameObject[] peoplePrefabs;
	// Use this for initialization
	void Start () {
		spawnPeople (5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void spawnPeople(int count) {
		for (int i = 0; i < count; i++) {
			Vector2 pos = getRandomPos ();
			GameObject go = Instantiate (peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], pos, Quaternion.identity) as GameObject;
			go.GetComponent<peopleMovement> ().setMapSize (mapSize_X, mapSize_Y);
		}
	}


	private Vector2 getRandomPos() {
		return new Vector2(Random.Range((mapSize_X/2)*-1, mapSize_X/2), Random.Range((mapSize_Y/2)*-1, mapSize_Y/2));
	}

}
