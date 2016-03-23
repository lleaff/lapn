using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FieldPlacerButton : MonoBehaviour {

	Button button;

	void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener (FieldPlacement);
	}

	void FieldPlacement () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
