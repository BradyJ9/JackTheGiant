using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreContoller : MonoBehaviour {

	[SerializeField]
	private Text hiScoreText, coinScoreText;

	// Use this for initialization
	void Start () {
		SetScoreBasedOnDifficulty ();
	}

	void SetScore (int score, int coinScore) {
		hiScoreText.text = score.ToString();
		coinScoreText.text = coinScore.ToString();
	}

	void SetScoreBasedOnDifficulty() {
		if (GamePreferences.GetEasyDifficultyState() == 1) {
			SetScore (GamePreferences.GetEasyDifficultyHighScore(), GamePreferences.GetEasyDifficultyCoinScore() );
		}
		if (GamePreferences.GetMediumDifficultyState() == 1) {
			SetScore (GamePreferences.GetMediumDifficultyHighScore(), GamePreferences.GetMediumDifficultyCoinScore());

		}
		if (GamePreferences.GetHardDifficultyState() == 1) {
			SetScore (GamePreferences.GetHardDifficultyHighScore(), GamePreferences.GetHardDifficultyCoinScore());

		}

	}

	public void BackButton() {
		SceneManager.LoadScene ("MainMenu");
	}
}
