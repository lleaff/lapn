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
		image = this.transform.GetChild (0);
		image.localScale = new Vector3 (1, globals.i.get_life (index) * 0.01F, 1);
		if (Input.GetKey(keys[index]))
			eat();
	}

	void eat()
	{
		if (globals.i.Carrots >= 5) {
			globals.i.remove_carrots (5);
			globals.i.add_life (5, index);
		}
	}
}
