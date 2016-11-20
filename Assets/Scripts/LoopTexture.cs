using UnityEngine;
using System.Collections;

public class LoopTexture : MonoBehaviour {
		public Renderer rend;
		private float offset2 = 0;
		private int dir = 1;
		void Start() {
			rend = GetComponent<Renderer>();
		}
		void Update() {
		if (dir == 1) {
			offset2 += Time.deltaTime * 0.5f;
			if (offset2 > 1) {
				offset2 = 1 - (offset2 - 1);
				dir = -1;
			}
		} else {
			offset2 -= Time.deltaTime * 0.5f;
			if (offset2 < 0) {
				offset2 = -offset2;
				dir = 1;
			}
		}



			rend.material.mainTextureOffset = new Vector2(Mathf.SmoothStep(-0.15f, 0.15f, offset2), 0);
		}
	}
