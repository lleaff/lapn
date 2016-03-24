using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class time_speed : MonoBehaviour {

	private Button myButton;

	public int index;
	public GameObject play;
	public GameObject Pause;
	public GameObject speed;
	public GameObject normal;
			
	public void time_0() {   /*Pause*/
		Time.timeScale = 0;
		Pause.SetActive (false);
		play.SetActive (true);
		speed.SetActive (true);
		normal.SetActive (false);
	}

	public void time_1() {   /*Play*/
		Time.timeScale = 1;
		Pause.SetActive (true);
		play.SetActive (false);
		speed.SetActive (true);
		normal.SetActive (false);
	}

	public void time_2() {   /*Speed x2*/
		Time.timeScale = 2;
		speed.SetActive (false);
		normal.SetActive (true);
		Pause.SetActive (true);
		play.SetActive (false);
	}

	public void time_3() {   /*Normal speed*/
		Time.timeScale = 1;
		speed.SetActive (true);
		normal.SetActive (false);
		Pause.SetActive (true);
		play.SetActive (false);
	}

	// Monobehaviour
	//------------------------------------------------------------

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
}
