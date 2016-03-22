using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_life : MonoBehaviour {

	private Button myButton;
	public int index;
	
	private Transform image;
	private KeyCode[] keys;

	void Awake()
	{
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (eat);
		keys = new KeyCode[4];
		keys [0] = KeyCode.Alpha1;
		keys [1] = KeyCode.Alpha2;
		keys [2] = KeyCode.Alpha3;
		keys [3] = KeyCode.Alpha4;
	}

	void Update () {
		image = this.transform.GetChild (1);
		image.localScale = new Vector3 (globals.i.get_life (index) * 0.01F, 1, 1);
		if (Input.GetKey(keys[index]))
			eat();
		if (globals.i.get_life (index) >= 50) {
			this.transform.GetChild (2).gameObject.SetActive (true);
			this.transform.GetChild (3).gameObject.SetActive (false);
		} else {
			this.transform.GetChild (2).gameObject.SetActive (false);
			this.transform.GetChild (3).gameObject.SetActive (true);
		}
	}

	void eat()
	{
		if (globals.i.Carrots >= 5) {
			globals.i.remove_carrots (5);
			globals.i.add_life (5, index);
		}
	}
}
