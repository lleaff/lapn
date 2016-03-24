using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cooldown : MonoBehaviour {

	private Button myButton;
	private float time_click;
	private float time;
	private bool clicked = false;

	public GameObject coolDown;

	private void reset()
	{
		if (!clicked)
			time_click = TimeManager.i.Seconds;
		clicked = true;
	}

	// Monobehaviour
	//------------------------------------------------------------

	void Awake() {
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (reset);
	}

	void Update () {
		time = TimeManager.i.Seconds;
		if (time - time_click <= 5 && clicked)
			coolDown.transform.localScale = new Vector3 (1, 1 - ((time - time_click) * 0.2F), 1);
		else if (time - time_click > 5 && clicked)
			clicked = false;
		else
			coolDown.transform.localScale = new Vector3(1, 0 ,1);
	}
}
