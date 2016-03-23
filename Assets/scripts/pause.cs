using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pause : MonoBehaviour {

	private Button myButton;
	public int index;
	public GameObject play;
	public GameObject Pause;
	public GameObject speed;
	public GameObject normal;

	void Awake(){
		myButton = GetComponent<Button> ();
		if (index == 0)
			myButton.onClick.AddListener (time_0);
		else if (index == 1)
			myButton.onClick.AddListener (time_1);
		else if (index == 2)
			myButton.onClick.AddListener (time_2);
		else if (index == 3)
			myButton.onClick.AddListener (time_3);
	}

	public void time_0() {
		Time.timeScale = 0;
		Pause.SetActive (false);
		play.SetActive (true);
	}

	public void time_1() {
		Time.timeScale = 1;
		Pause.SetActive (true);
		play.SetActive (false);
	}

	public void time_2() {
		speed.SetActive (false);
		normal.SetActive (true);
		Pause.SetActive (true);
		play.SetActive (false);
		Time.timeScale = 2;
	}

	public void time_3() {
		speed.SetActive (true);
		normal.SetActive (false);
		Pause.SetActive (true);
		play.SetActive (false);
		Time.timeScale = 1;
	}
}
