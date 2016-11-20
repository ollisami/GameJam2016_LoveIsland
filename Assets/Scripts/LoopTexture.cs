using UnityEngine;
using System.Collections;

public class LoopTexture : MonoBehaviour {
		private float scrollSpeed = 0.2F;
		public Renderer rend;
	private float offset = 0;
		void Start() {
			rend = GetComponent<Renderer>();
		}
		void Update() {
		if (offset > 0.1F || offset < -0.1F)
			scrollSpeed = -scrollSpeed;	
		offset += Time.deltaTime * scrollSpeed;
		rend.material.mainTextureOffset = new Vector2(offset, 0);

		}
	}
