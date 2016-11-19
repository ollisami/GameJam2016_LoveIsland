using UnityEngine;
using System.Collections;

public class HeartDropScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = Vector3.one * (1.65f + Mathf.Sin (Time.time * 6) / 6f);
	}

	void OnMouseEnter() {
		CoinManager.Instance.AddCoins (1);
		Destroy (this.gameObject);
	}
}
