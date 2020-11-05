using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionsController : MonoBehaviour {

	[SerializeField]
	private GameObject easySign, medSign, hardSign;

	// Use this for initialization
	void Start () {
		SetDifficulty ();
	}

	void SetInitialDifficulty(string difficulty) {
		switch (difficulty) {
		case "easy":
			medSign.SetActive (false);
			hardSign.SetActive (false);
			break;
		
		case "medium":
			easySign.SetActive (false);
			hardSign.SetActive (false);
			break;
		
		case "hard":
			easySign.SetActive (false);
			medSign.SetActive (false);
			break;
		}
	}

	void SetDifficulty() {
		if (GamePreferences.GetEasyDifficultyState () == 1) {
			SetInitialDifficulty ("easy");
		}
		if (GamePreferences.GetMediumDifficultyState () == 1) {
			SetInitialDifficulty ("medium");
		}
		if (GamePreferences.GetHardDifficultyState () == 1) {
			SetInitialDifficulty ("hard");
		}
	}

	public void EasyDifficulty() { //done on button press

		GamePreferences.SetEasyDifficultyState (1);
		GamePreferences.SetMediumDifficultyState (0);
		GamePreferences.SetHardDifficultyState (0);

		easySign.SetActive (true);
		medSign.SetActive (false);
		hardSign.SetActive (false);

	}

	public void MediumDifficulty() {

		GamePreferences.SetEasyDifficultyState (0);
		GamePreferences.SetMediumDifficultyState (1);
		GamePreferences.SetHardDifficultyState (0);

		easySign.SetActive (false);
		medSign.SetActive (true);
		hardSign.SetActive (false);

	}

	public void HardDifficulty() {

		GamePreferences.SetEasyDifficultyState (0);
		GamePreferences.SetMediumDifficultyState (0);
		GamePreferences.SetHardDifficultyState (1);

		easySign.SetActive (false);
		medSign.SetActive (false);
		hardSign.SetActive (true);
	}
	
	public void BackButton() {
		SceneManager.LoadScene ("MainMenu");
	}
}
