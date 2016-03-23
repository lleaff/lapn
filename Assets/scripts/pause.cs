using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pause : MonoBehaviour {

	private Button my_button;

	void Awake () {
		my_button = GetComponent<Button> ();
		my_button.onClick.AddListener (time_2);
	}

	public void time_0() {
		Time.timeScale = 0;
	}

	public void time_1() {
		Time.timeScale = 1;
	}

	public void time_2() {
		Time.timeScale = 2;
	}

}
