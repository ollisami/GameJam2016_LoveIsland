using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	private GameController gameController;

	public Text ScoreText;
	public GameObject GameOverUI;
	public GameObject FreezeOverlay;

	// Use this for initialization
	private void Start () {
		gameController = FindObjectOfType<GameController> ();

		Button playAgainButton = this.GameOverUI.GetComponentInChildren<Button> ();
		playAgainButton.onClick.AddListener(this.OnPlayAgainButtonClicked);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.HasGameEnded) {
			this.GameOverUI.SetActive (true);
		}

		setFreezeOverlay (gameController.isFrozen);

		this.ScoreText.text = CoinManager.Instance.CoinCount.ToString();
	}

	private void setFreezeOverlay(bool visible) {
		
		if (visible) {
			FreezeOverlay.GetComponent<SpriteRenderer> ().enabled = true;
			Color tmp = FreezeOverlay.GetComponent<SpriteRenderer> ().color;
			tmp.a = 0.5f;

			FreezeOverlay.GetComponent<SpriteRenderer> ().color = tmp;
		} else {
			FreezeOverlay.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	private void OnPlayAgainButtonClicked() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}