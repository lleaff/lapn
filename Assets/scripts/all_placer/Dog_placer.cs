﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dog_placer : MonoBehaviour
{

	private Button myButton;
	private GameObject cur;
	private GameObject old = null;
	private GameObject tmp;
	private Vector3 tmppos;

/*	public Material mat;*/
	public GameObject Dog;

	/**********
	 * Bind the button with a listener
	 * ********/
	public void Awake() {
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (add);
	}

	/**********
	 * Set current button
	 * and check for the money
	 * ********/
	public void add() {
		if (globals.i.Button != 7 && globals.i.Money >= 1300)
			globals.i.Button = 7;
		else
			globals.i.Button = 0;
	}

	public void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool raycast = Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("PlacementGrid"));

		/*if left click + button selected + cursor on tile + not field tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 7 && raycast && hit.collider.name.Substring(0,9) != "FieldNode") {
			globals.i.Money -= 1300;
			tmppos = tmp.transform.position;
			Destroy (tmp.gameObject);
			tmp = Instantiate (Dog);
			tmp.transform.localPosition = tmppos;
			tmp.name = "Dog";
			tmp.GetComponent<ia_dog> ().enabled = true;
			tmp.GetComponent<BoxCollider> ().enabled = true;
			tmp.GetComponent<SphereCollider> ().enabled = true;
			tmp.GetComponent<NavMeshAgent> ().enabled = true;

			old = null;
			globals.i.Button = 0;
		}

		/*if cursor on tile + button selected*/
		if (raycast && globals.i.Button == 7) {
			cur = GameObject.Find (hit.collider.name);
			if (hit.collider.name.Substring(0,9) != "FieldNode" && cur.transform.FindChild ("Dog") == null) {
				tmp = Instantiate (Dog);
				tmp.transform.parent = cur.transform;
				tmp.transform.localRotation = Quaternion.Euler (270, 0, 0);
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = new Vector3(8F,0.8F,8F);
				tmp.name = "Dog";
				if (old != cur) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("Dog").gameObject);
					old = cur;
				}
			}
		} else if (old) {
			GameObject.Destroy (old.transform.FindChild ("Dog").gameObject);
			old = null;
		}
	}
}