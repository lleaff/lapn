using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class display_graph : MonoBehaviour {

	private List<int>list_value = new List<int>();
	public Texture line;

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
}
