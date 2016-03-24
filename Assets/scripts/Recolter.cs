using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Recolter : MonoBehaviour
{

	public bool Selected = false;
	private Button myButton;


	void Awake ()
	{
		myButton = GetComponent<Button> ();
		myButton.onClick.AddListener (click);
	}

	void click()
	{
		if (Selected)
			Selected = false;
		else
			Selected = true;
	}

	void Update ()
	{
		if (!Selected) {
			return;
		}

		bool clicked = Input.GetMouseButtonUp (0);

		GameObject cell = getPointedObj ();
		if (!cell) {
			return;
		}

		GameObject carrots = CellUtils.FindObjectWithTag (cell, globals.carrotTag);
		if (!carrots) {
			carrots = CellUtils.FindObjectWithTag (cell, "unattainable");
			if (!carrots) {
				carrots = CellUtils.FindObjectWithTag (cell, globals.decayedTag);
				if (!carrots) {
					return;
				}
			}
		}

		if (clicked) {
			Harvest (cell, carrots);
		}

		clicked = false;
	}

	bool Harvest (GameObject cell, GameObject carrots)
	{
		GameObject field = CellUtils.FindObjectWithNameBeginsWith (cell, globals.fieldName);
		if (!field) {
			return false;
		} else if (field.CompareTag ("eaten")) {
			return false;
		} else if (field.CompareTag (globals.decayedTag)) {
			Destroy (field);
		} else if (!carrots.GetComponent<ia_carrots>().RemoveCarrot ()) {
			return false;
		}
		globals.i.add_carrots (1);
		return true;
	}

	GameObject getPointedObj ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("PlacementGrid"))) {
			return hit.collider.gameObject;
		}
		return null;
	}
}
