using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_life : MonoBehaviour {

	private Button myButton;
	public int index;
	private GameObject globalValue;
	private Transform image;
	private KeyCode[] keys;

	void Awake()
	{
		globalValue = GameObject.Find ("GlobalValue");
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (eat);
		keys = new KeyCode[4];
		keys [0] = KeyCode.Alpha1;
		keys [1] = KeyCode.Alpha2;
		keys [2] = KeyCode.Alpha3;
		keys [3] = KeyCode.Alpha4;    
	}

	void Update () {
		image = this.transform.GetChild (0);
		image.localScale = new Vector3 (1, globalValue.GetComponent<globalValue> ().get_life (index) * 0.01F, 1);
		if (Input.GetKey(keys[index]))
			eat();
	}

	void eat()
	{
		if (globalValue.GetComponent<globalValue> ().Carrots >= 5) {
			globalValue.GetComponent<globalValue> ().remove_carrots (5);
			globalValue.GetComponent<globalValue> ().add_life (5, index);
		}
	}
}
