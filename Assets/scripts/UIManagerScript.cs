using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public Animator startButton;
	public Animator optionsButton;
	public Animator dialog;
	public Animator scoreboard;
	public GameObject Board;

	public void StartGame() {
		Destroy (GameObject.Find ("GlobalValue"));
		print ("start game");
		SceneManager.LoadScene("mezon");
	}

	public void OpenSettings() {
		startButton.SetBool("isHidden", false);
		optionsButton.SetBool("isHidden", true);
		dialog.SetBool("isHidden", false);
	}
	public void CloseSettings() {
		startButton.SetBool("isHidden", true);
		optionsButton.SetBool("isHidden", false);
		dialog.SetBool("isHidden", true);
	}
	public void CloseScoreBoard() {
		startButton.SetBool("isHidden", true);
		optionsButton.SetBool("isHidden", false);
		scoreboard.SetBool("show_score", false);
	}
}
