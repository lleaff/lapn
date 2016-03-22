using UnityEngine;
using System.Collections;

public class Recolter : MonoBehaviour
{

	public bool Selected = false;


	void Start ()
	{
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

		GameObject carrots = CellUtils.FindObjectWithTag (cell, "Carrot");
		if (!carrots) {
			return;
		}

		if (clicked) {
			Harvest (cell, carrots);
		}
	}

	bool Harvest (GameObject cell, GameObject carrots) {
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
