using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class show_ui_sell : MonoBehaviour {

	public GameObject ui_sell;
	private Button myButton;
	static private bool is_active = false;

	void Awake()
	{
		ui_sell.SetActive (false);
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (show);
	}

	void show()
	{
		if (is_active == true) {
			ui_sell.SetActive (false);
			is_active = false;
		} else {
			ui_sell.SetActive (true);
			is_active = true;	
		}
	}
}
