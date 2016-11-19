using UnityEngine;
using System.Collections;

public class LoopTexture : MonoBehaviour {
		public float scrollSpeed = 0.5F;
		public Renderer rend;
		void Start() {
			rend = GetComponent<Renderer>();
		}
		void Update() {
			float offset = Time.time * scrollSpeed;
			rend.material.mainTextureOffset = new Vector2(offset, 0);

		}
	}
