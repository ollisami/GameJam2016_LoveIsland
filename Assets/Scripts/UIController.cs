using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	private GameController gameController;

	private bool hasLevelEndBeenHandled = false;
	private bool didPlayerPassLevel = false;

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
		if(gameController.HasGameEnded && !hasLevelEndBeenHandled) {
			this.OnGameOver ();

			hasLevelEndBeenHandled = true;
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
		if (didPlayerPassLevel) {
			LevelManager.Instance.LoadNextLevel ();
		} else {
			LevelManager.Instance.ReloadCurrentScene ();
		}
	}

	private void OnGameOver() {
		int totalPeopleInLevel = gameController.PeopleToSpawn;
		int totalPeopleInfected = gameController.TotalInfections;

		if (WasLevelPassed (LevelManager.Instance.CurrentLevel, totalPeopleInLevel, totalPeopleInfected)) {
			// SHOW NEXT LEVEL UI
			if (LevelManager.Instance.IsLastLevel) {
				// last level passed, tadaa, player won
				ShowGameWonUI();
			} else {
				ShowLevelFinishedUI (true);
			}

			didPlayerPassLevel = true;
		} else {
			ShowLevelFinishedUI (false);
			didPlayerPassLevel = false;
		}

	}

	private static bool WasLevelPassed(int level, int totalPeople, int infections) {
		return infections > totalPeople * 0.75f; // TODO: modify this, atm if you infect
												// 70% people, you pass
	}

	private void ShowLevelFinishedUI(bool didPlayerPassLevel) {
		Text gameOverText = this.GameOverUI.transform.FindChild("Game Over Text").GetComponent<Text>();
		Text playAgainButtonText = this.GameOverUI.transform.FindChild ("Button").GetComponentInChildren<Text> ();

		gameOverText.text = (didPlayerPassLevel ? "Level passed!" : "Level failed!");
		playAgainButtonText.text = (didPlayerPassLevel ? "Next Level?" : "Play Again?");
		this.GameOverUI.SetActive (true);
	}

	private void ShowGameWonUI() {
		Text gameOverText = this.GameOverUI.transform.FindChild("Game Over Text").GetComponent<Text>();
		var playAgainButton = this.GameOverUI.transform.FindChild ("Button");

		gameOverText.text = "You won!";
		this.GameOverUI.SetActive (true);
		playAgainButton.gameObject.SetActive(false);
	}
}