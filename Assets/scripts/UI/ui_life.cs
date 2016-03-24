using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_life : MonoBehaviour {

	public int index;
	public int delay;

	private Button myButton;
	private Transform image;
	private KeyCode[] keys;
	private int time = 0;

	void eat()
	{
		if (globals.i.Carrots >= 5 && globals.i.Family[index] > 0) {
			globals.i.remove_carrots (5);
			globals.i.add_life (5, index);
		}
	}

	// Monobehaviour
	//------------------------------------------------------------

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

	void FixedUpdate () {
		image = this.transform.GetChild (1);
		image.localScale = new Vector3 (globals.i.get_life (index) * 0.01F, 1, 1);
		if (Input.GetKey (keys [index]) && time == 0) {
			eat ();
			time = delay;
		}
		if (globals.i.get_life (index) >= 50) {
			this.transform.GetChild (2).gameObject.SetActive (true);
			this.transform.GetChild (3).gameObject.SetActive (false);
		} else {
			this.transform.GetChild (2).gameObject.SetActive (false);
			this.transform.GetChild (3).gameObject.SetActive (true);
		}
		if (time > 0)
			time--;
	}
}
