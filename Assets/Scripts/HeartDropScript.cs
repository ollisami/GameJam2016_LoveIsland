using UnityEngine;
using System.Collections;

public class HeartDropScript : MonoBehaviour {

	private Vector2 dropPosition;
	private bool isCollected = false;
	private float lerpToUI = 0;
	// Use this for initialization
	void Start () {
		dropPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = Vector3.one * (1.65f + Mathf.Sin (Time.time * 6) / 6f);

		if (isCollected) {
			lerpToUI += Time.deltaTime * 2f;
			this.transform.position = Vector2.Lerp (dropPosition, GetCameraTopLeftCoordinate (), lerpToUI * lerpToUI);
			if (lerpToUI > 1) {

				CoinManager.Instance.AddCoins (1);
				Destroy (this.gameObject);
			}
		}
	}

	void OnMouseEnter() {

		isCollected = true;
	}

	private Vector2 GetCameraTopLeftCoordinate() {
		var camera = Camera.main;

		float left = camera.transform.position.x - camera.orthographicSize * camera.aspect * 0.95f;
		float top = camera.transform.position.y + camera.orthographicSize * 0.95f;

		return new Vector2 (left, top);
	}
}
