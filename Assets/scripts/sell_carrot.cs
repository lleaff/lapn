using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class sell_carrot : MonoBehaviour {

	private Button my_button;
	private GameObject globalValue;
	private List<int>list_value = new List<int>();

	void Awake ()
	{
		my_button = GetComponent<Button>();
		my_button.onClick.AddListener (sell);
	}

	void sell()
	{
		globalValue = GameObject.Find ("GlobalValue");
		if (globalValue.GetComponent<globalValue> ().get_carrots () > 0) {
			globalValue.GetComponent<globalValue> ().remove_carrots (1);
			list_value = globalValue.GetComponent<globalValue> ().get_list ();
			globalValue.GetComponent<globalValue> ().add_money (list_value [list_value.Count - 1]);
		}
	}

}
