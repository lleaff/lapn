using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class show_ui_sell : MonoBehaviour {

	private Button myButton;
	private List<int>list_value = new List<int>();
	public Texture line;
	public GameObject ui_sell;

	void Awake ()
	{
		ui_sell.SetActive (false);
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (show);
	}

	void Update()
	{
		list_value = globals.i.List;
	}

	private int get_max()
	{
		int max = 0;
		foreach (int price  in list_value) {
			if (price > max)
				max = price;
		}
		return (max);
	}

	void OnGUI(){
		int i = 0;
		foreach (int price in list_value) {
			GUI.DrawTexture (new Rect (25F + ((100F / list_value.Count) * i) , 575F - (600 - Screen.height) - price * (100 / get_max()), 100F / list_value.Count, price * (100 / get_max())), line);
			i++;
		}
	}

	public void show()
	{
		if (ui_sell.activeInHierarchy) {
			ui_sell.SetActive (false);
		} else {
			ui_sell.SetActive (true);	
		}
	}
}
