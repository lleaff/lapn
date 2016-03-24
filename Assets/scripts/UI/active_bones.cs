using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class active_bones : MonoBehaviour {

	public GameObject bone;
	public GameObject bone_e;

	void Update () {
		if (GameObject.Find ("Dog") && globals.i.Money >= 10) {
			bone.SetActive (true);
			bone_e.SetActive (true);
		} else if (GameObject.Find ("Dog")) {
			bone_e.SetActive (true);
		} else {
			bone.SetActive (false);
			bone_e.SetActive (false);
		}
	}
}
