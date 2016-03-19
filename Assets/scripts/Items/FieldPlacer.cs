using UnityEngine;
using System.Collections;

public class FieldPlacer : MonoBehaviour {

	public GameObject ItemObj;

	bool PlaceCondition (GameObject cell) {
		// Cell must be empty
		if (cell.transform.childCount != 0) {
			return false;
		}
		if (SM.i.TryPay(Items.Field.Cost)) {
			return false;
		}
		return true;
	}

	bool PlaceOutcome(GameObject cell) {
		Debug.Log ("Placed field");
		return true;
	}

	void Awake () {
		ItemPlacer.Init(
			itemObj: ItemObj,
			placeCondition: PlaceCondition,
			outcomeValid: PlaceOutcome);
		GameObject.Destroy (this);
	}
		
}
