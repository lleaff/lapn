using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemPlacer : MonoBehaviour {
/*	// Config
	public bool autoInstantiate = true;
	public string ReceivingLayerName = "PlacementGrid";

	public GameObject ItemObj;
	public delegate bool PlacementCondition (GameObject hitObject, Vector3 position);
	PlacementCondition PlaceCondition;
	public delegate bool Outcome (GameObject obj);
	// if return true, keep itemPlacer selected, otherwise unselect
	public Outcome OutcomeValid;
	public Outcome OutcomeInvalid = null;

	void Init(GameObject itemObj, PlacementCondition placeCondition, Outcome outcomeValid, Outcome outcomeInvalid) {
		ItemObj = itemObj;
		PlaceCondition = placeCondition;
		OutcomeValid = outcomeValid;
		OutcomeInvalid = outcomeInvalid;
	}

	private GameObject hitObj;
	private GameObject previewItem;
	private bool clicked = false;
	private bool selected = false;



	void InstantiateItemAt(RaycastHit hit) {
		if (!autoInstantiate) {
			return;
		}
	}

	void MakePreview() {

	}

	void FixedUpdate() {
		if (!selected) {
			return;
		}
		clicked = Input.GetMouseButtonUp (0);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer (ReceivingLayerName))) {
			return;
		}
		bool validPlacement = PlaceCondition (hit.collider.gameObject, hit.point);
		if (clicked) {
			selected = validPlacement ? OutcomeValid (previewItem) : OutcomeInvalid (previewItem);
			InstantiateItemAt (hit);
			previewItem = null;
		} else {
			MakePreview(hit);
		}


		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && clicked && moneynb >= 10) {
			hitObj = hit.collider;
			if (hitObj.transform.childCount == 0 && hit.collider.name.Substring(0,9) == "FieldNode") {
				tmp = Instantiate (ItemObj);
				tmp.transform.parent = h.transform;
				tmp.transform.localRotation = Quaternion.identity;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = Vector3.one;
				if (previewItem != h) {
					if (previewItem)
						GameObject.Destroy (previewItem.transform.GetChild (0).gameObject);
					previewItem = h;
				}
			}
		}
	}
	*/
}
