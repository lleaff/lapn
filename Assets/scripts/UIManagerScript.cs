using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public Animator startButton;
	public Animator optionsButton;

	public void StartGame() {
		SceneManager.LoadScene("mezon");
	}

	public void OpenSettings() {
		startButton.SetBool("isHidden", true);
		optionsButton.SetBool("isHidden", true);
	}
}
