using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class sell_carrot : MonoBehaviour {

	private Button my_button;
	private List<int>list_value = new List<int>();

	public void Sell()
	{
		if (globals.i.Carrots > 0) {
			globals.i.remove_carrots (1);
			globals.i.add_money (list_value [list_value.Count - 1]);
			this.GetComponent<AudioSource> ().Play ();
		} else
			this.transform.parent.GetComponent<AudioSource> ().Play ();
	}

	// Monobehaviour
	//------------------------------------------------------------

	void Awake ()
	{
		my_button = GetComponent<Button>();
		my_button.onClick.AddListener (Sell);
	}

	void Update()
	{
		list_value = globals.i.List;
	}
}
