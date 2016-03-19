using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_life : MonoBehaviour {

	private Button myButton;
	public int index;
	private GameObject globalValue;
	private Transform image;

	void Awake()
	{
		globalValue = GameObject.Find ("GlobalValue");
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (eat);
	}

	void Update () {
		image = this.transform.GetChild (0);
		image.localScale = new Vector3 (1, globalValue.GetComponent<globalValue> ().get_life (index) * 0.01F, 1);
	}

	void eat()
	{
		globalValue.GetComponent<globalValue> ().remove_carrots (5);
		globalValue.GetComponent<globalValue> ().add_life (5, index);
	}

}
