using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class sell_carrot : MonoBehaviour {

	private Button my_button;
	private GameObject globalValue;

	void Awake ()
	{
		my_button = GetComponent<Button>();
		my_button.onClick.AddListener (sell);
	}

	void sell()
	{
		globalValue = GameObject.Find ("GlobalValue");
		globalValue.GetComponent<globalValue>().remove_carrots (1);
		globalValue.GetComponent<globalValue> ().add_money (10);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
