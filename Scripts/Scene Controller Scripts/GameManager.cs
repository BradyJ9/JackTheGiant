using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[HideInInspector]
	public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

	[HideInInspector]
	public int score, coinScore, lifeScore;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
	}

	void Start() {
		InitializeVariables ();
	}

	void OnEnable() {
		SceneManager.sceneLoaded += LevelFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= LevelFinishedLoading;
	}

	public void LevelFinishedLoading(Scene scene, LoadSceneMode mode) {

		if (scene.name == "scene") {
			if (gameRestartedAfterPlayerDied) {
				GameplayController.instance.SetScore (score);
				GameplayController.instance.SetCoinScore (coinScore);
				GameplayController.instance.SetLifeSCore (lifeScore);

				PlayerScore.scoreCount = score;
				PlayerScore.coinCount = coinScore;
				PlayerScore.lifeCount = lifeScore;

			} else if (gameStartedFromMainMenu) {

				PlayerScore.scoreCount = 0;
				PlayerScore.coinCount = 0;
				PlayerScore.lifeCount = 2;

				GameplayController.instance.SetScore (0);
				GameplayController.instance.SetCoinScore (0);
				GameplayController.instance.SetLifeSCore (2);
			}
		}

	}

	public void InitializeVariables() {

		if (!PlayerPrefs.HasKey ("Game Initialized")) {

			GamePreferences.SetEasyDifficultyState (0);
			GamePreferences.SetEasyDifficultyHighScore (0);
			GamePreferences.SetEasyDifficultyCoinScore (0);

			GamePreferences.SetMediumDifficultyState (1);
			GamePreferences.SetMediumDifficultyHighScore (0);
			GamePreferences.SetMediumDifficultyCoinScore (0);

			GamePreferences.SetHardDifficultyState (0);
			GamePreferences.SetHardDifficultyHighScore (0);
			GamePreferences.SetHardDifficultyCoinScore (0);

			GamePreferences.SetMusicState (0);

			PlayerPrefs.SetInt ("Game Initialized", 123);

		}
	}

	public void MakeSingleton() {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void CheckGameStatus(int score, int coinScore, int lifeScore) {
		if (lifeScore < 0) { //game over

			if (GamePreferences.GetEasyDifficultyState () == 1) {

				int high = GamePreferences.GetEasyDifficultyHighScore ();
				int coin = GamePreferences.GetEasyDifficultyCoinScore();

				if (high < score) {
					GamePreferences.SetEasyDifficultyHighScore (score);
				}
				if (coin < coinScore) {
					GamePreferences.SetEasyDifficultyCoinScore (coinScore);
				}

			}

			if (GamePreferences.GetMediumDifficultyState () == 1) {

				int high = GamePreferences.GetMediumDifficultyHighScore ();
				int coin = GamePreferences.GetMediumDifficultyCoinScore();

				if (high < score) {
					GamePreferences.SetMediumDifficultyHighScore (score);
				}
				if (coin < coinScore) {
					GamePreferences.SetMediumDifficultyCoinScore (coinScore);
				}

			}

			if (GamePreferences.GetHardDifficultyState () == 1) {

				int high = GamePreferences.GetHardDifficultyHighScore ();
				int coin = GamePreferences.GetHardDifficultyCoinScore();

				if (high < score) {
					GamePreferences.SetHardDifficultyHighScore (score);
				}
				if (coin < coinScore) {
					GamePreferences.SetHardDifficultyCoinScore (coinScore);
				}

			}


			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = false;

			GameplayController.instance.GameOverPopUp (score, coinScore);

		} else {

			this.score = score;
			this.lifeScore = lifeScore;
			this.coinScore = coinScore;

			GameplayController.instance.SetScore (score);
			GameplayController.instance.SetCoinScore (coinScore);
			GameplayController.instance.SetLifeSCore (lifeScore);

			gameRestartedAfterPlayerDied = true;
			gameStartedFromMainMenu = false;

			GameplayController.instance.PlayerDiedRestartGame ();

		}

	}




}
