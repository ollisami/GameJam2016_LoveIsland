using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	private GameController gameController;

	public Text ScoreText;
	public GameObject GameOverUI;

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

		this.ScoreText.text = "Lovers Infected: " + gameController.TotalInfections;
	}

	private void OnPlayAgainButtonClicked() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}