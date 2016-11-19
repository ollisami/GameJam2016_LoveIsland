using UnityEngine;
using System.Collections;

public class LoveProjectile : MonoBehaviour {

	public float Speed = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 dir = (Vector2)(Quaternion.Euler(0,0,this.transform.eulerAngles.z - 270) * Vector2.right);
		this.transform.position += new Vector3(dir.x, dir.y, 0) * Speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		var personMovement = collider.GetComponent<PersonMovement> ();
		if (personMovement) {
			personMovement.setInfected ();
			Destroy (this.gameObject);
		}

	}
}
