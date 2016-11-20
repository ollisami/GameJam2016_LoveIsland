using System;
using UnityEngine.SceneManagement;

public class LevelManager
{
	public int CurrentLevel = 1;
	public const int LevelCount = 2; // TODO: modify this when you add new levels

	public static readonly LevelManager Instance = new LevelManager();
	private LevelManager ()
	{
	}

	public void LoadNextLevel() {
		if (!this.IsLastLevel) {
			this.CurrentLevel++; // move to next scene

			this.ReloadCurrentScene ();
		}
	}

	public void ReloadCurrentScene() {
		SceneManager.LoadScene ("Level" + this.CurrentLevel);
	}

	public bool IsLastLevel {
		get {
			return this.CurrentLevel == LevelManager.LevelCount;
		}
	}
}
