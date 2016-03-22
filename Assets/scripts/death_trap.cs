﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class death_trap : MonoBehaviour {

	private Button myButton;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;

	public GameObject trap;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (addCarote);
	}

	void addCarote()
	{
		if (globals.i.Button != 5 && globals.i.Money >= 30)
			globals.i.Button = 5;
		else {
			if (old) {
				GameObject.Destroy (old.transform.FindChild ("trap").gameObject);
				old = null;
			}
			globals.i.Button = 0;
		}
	}

	bool check_pos(Transform obj) {
		foreach (Transform child in obj) {
			if (child.name == "trap")
				return (false);
		}
		return (true);
	}

	void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		/*You can place the trap if you leftclick + you have pressed the button + you are on a tile + you have the money + it's a trap tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 5 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid"))) {
			globals.i.Money -= 30;
			old = null;
			globals.i.Button = 0;
		}

		/*Moving object*/
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid")) && globals.i.Button == 5) {
			h = GameObject.Find (hit.collider.name);
			if (check_pos(h.transform)) {
				tmp = Instantiate (trap);
				tmp.transform.parent = h.transform;
				tmp.transform.localRotation = Quaternion.Euler (270, 0, 0);
				tmp.transform.localPosition = new Vector3(-2.5F,2.5F,0.6F);
				tmp.transform.localScale = new Vector3(15F,1F,15F);
				tmp.name = "trap";
				if (old != h) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("trap").gameObject);
					old = h;
				}
			}
		}
	}
}
