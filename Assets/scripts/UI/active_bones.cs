using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class active_bones : MonoBehaviour {

	public GameObject bone_button;

	void Update () {
		if (GameObject.Find ("Dog"))
			bone_button.SetActive (true);
		else
			bone_button.SetActive (false);
	}
}
