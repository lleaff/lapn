﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class button : MonoBehaviour {

	private Button myButton;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;
	private string[] words;
	private int moneynb;
	public GameObject field;
	public Text money;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (addCarote);
	}

	void addCarote()
	{
		if (Globals.i.Button != 1)
			Globals.i.Button = 1;
		else {
			if (old) {
				GameObject.Destroy (old.transform.FindChild ("field").gameObject);
				old = null;
			}
			Globals.i.Button = 0;
		}
	}

	public static int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}

	bool check_pos(Transform obj) {
		foreach (Transform child in obj) {
			if (child.name == "field")
				return (false);
		}
		return (true);
	}

	void FixedUpdate() {
		/*Split the money count*/
		words = money.text.Split (' ');
		moneynb = IntParseFast(words[1]);

		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		/*You can place the field if you leftclick + you have pressed the button + you are on a tile + you have the money + it's a field tile*/
		if (Input.GetMouseButtonUp (0) && Globals.i.Button == 1 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid")) && hit.collider.name.Substring(0,9) == "FieldNode" && moneynb >= 10) {

			money.text = "Money: " + (moneynb-10) + " $";
			old.transform.FindChild ("field").gameObject.GetComponent<AudioSource> ().Play ();
			old.transform.FindChild ("field").gameObject.tag = "Carrot";
			old = null;
			Globals.i.Button = 0;
		}

		/*Moving object*/
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid")) && Globals.i.Button == 1 && moneynb >= 10) {
			h = GameObject.Find (hit.collider.name);
			if (check_pos(h.transform) && hit.collider.name.Substring(0,9) == "FieldNode") {
				tmp = Instantiate (field);
				tmp.transform.parent = h.transform;
				tmp.transform.localRotation = Quaternion.identity;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = Vector3.one;
				tmp.name = "field";
				if (old != h) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("field").gameObject);
					old = h;
				}
			}
		}
	}
}
