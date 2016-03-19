using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hide_ui_sell : MonoBehaviour {

	public GameObject ui_sell;
	private Button myButton;

	void Awake()
	{
		ui_sell.SetActive (false);
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (show);
	}

	private void show()
	{
		if (ui_sell.activeInHierarchy) {
			ui_sell.SetActive (false);
		} else {
			ui_sell.SetActive (true);
		}
	}
}