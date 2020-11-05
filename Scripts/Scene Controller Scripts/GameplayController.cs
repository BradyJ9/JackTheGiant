using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[SerializeField]
	private Text scoreText, coinText, lifeText, finalScoreText, finalCoinScoreText;

	[SerializeField]
	private GameObject pausePanel, gameOverPanel;

	[SerializeField]
	private GameObject readyButton;
	
	void Awake() {
		MakeInstance ();
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeInstance() {
		if (instance == null) {
			instance = this;
		}
	}


	public void GameOverPopUp(int finalScore, int finalCoinScore) {
		gameOverPanel.SetActive (true);
		finalScoreText.text = finalScore.ToString ();
		finalCoinScoreText.text = finalCoinScore.ToString ();
		StartCoroutine (GameOverToMainMenu ());
	}

	IEnumerator GameOverToMainMenu() {
		yield return new WaitForSeconds (4f);
		SceneManager.LoadScene ("MainMenu");
	}

	public void PlayerDiedRestartGame() {
		StartCoroutine (PlayerDiedRestart ());
	}

	IEnumerator PlayerDiedRestart() {
		yield return new WaitForSeconds (1f);
		SceneFader.instance.LoadLevel ("scene");
		//SceneManager.LoadScene ("scene");
	}

	public void SetScore (int score) {
		scoreText.text = "x" + score;
	}

	public void SetCoinScore (int coinScore) {
		coinText.text = "x" + coinScore;
	}

	public void SetLifeSCore (int lifeScore) {
		lifeText.text = "x" + lifeScore;
	}

	public void PauseTheGame() {
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}

	public void ResumeGame() {
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}

	public void QuitGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}

	public void StartGame() {
		Time.timeScale = 1f;
		readyButton.SetActive (false);
	}
}
