using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class sell_carrot : MonoBehaviour {

	private Button my_button;
	
	private List<int>list_value = new List<int>();
	public Texture line;
	public Text carrot_value;

	void Awake ()
	{
		
		list_value = Globals.i.List;
		my_button = GetComponent<Button>();
		my_button.onClick.AddListener (sell);
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
			GUI.DrawTexture (new Rect (255 + ((450F / list_value.Count) * i), 431F - price * (300 / get_max()), 450F / list_value.Count, price * (300 / get_max())), line);

			i++;
		}
	}

	void sell()
	{
		if (Globals.i.Carrots > 0) {
			Globals.i.remove_carrots (1);
			Globals.i.add_money (list_value [list_value.Count - 1]);
		}
	}
		
	void Update()
	{
		carrot_value.text = "Valeur de la carotte: " + list_value [list_value.Count - 1].ToString ();
	}
}
