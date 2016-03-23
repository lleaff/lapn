using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class refresh_value : MonoBehaviour {

	public Text text;

	void Update () {
		text.text = globals.i.List [globals.i.List.Count - 1].ToString();
	}
}
