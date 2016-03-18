using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public Animator startButton;
	public Animator optionsButton;
	public Animator dialog;

	public void StartGame() {
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
}
