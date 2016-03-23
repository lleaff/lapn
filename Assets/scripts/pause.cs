using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pause : MonoBehaviour {

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
