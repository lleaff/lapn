using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemPlacer : MonoBehaviour {
	
	// Config
	//------------------------------------------------------------
	public bool autoInstantiate = true;
	public string GridNodesLayerName = "PlacementGrid";
	public string PreviewLayerName = "Immaterial";
	public GameObject ItemObj;
	public string[] PreviewActivatedComponents = { "MeshRenderer", "SpriteRenderer" };
	public static string ItemPlacerContainerObjectName = "ItemPlacers";

	//------------------------------------------------------------

	public delegate bool PlacementCondition (GameObject cell);
	PlacementCondition PlaceCondition;
	public delegate bool Outcome (GameObject obj);
	// if return true, keep itemPlacer selected, otherwise unselect
	Outcome OutcomeValid;
	Outcome OutcomeInvalid = null;

	LayerMask PreviewLayer;
	LayerMask PlacementLayer;
	GameObject hitObj;
	GameObject previewItem;
	GameObject previewObj;
	GameObject previousCell;
	GameObject cell;
	bool clicked = false;
	bool selected = false;

	// Initialization
	//------------------------------------------------------------

	GameObject initPreviewItem(GameObject itemObj) {
		string[] activatedComponents = PreviewActivatedComponents;

		GameObject previewItem;
		previewItem = Instantiate (ItemObj);
		previewItem.SetActive(false);
		MonoBehaviour[] disabledComponents = itemObj.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour c in disabledComponents) {
			c.enabled = false;
		}
		foreach (string c in activatedComponents) {
			var ci = GetComponent(c) as MonoBehaviour;
			if (ci) {
				ci.enabled = true;
			}
		}
		return previewItem;
	}

	public static GameObject Init(GameObject itemObj, PlacementCondition placeCondition, Outcome outcomeValid, Outcome outcomeInvalid = null) {
		GameObject container = GameObject.Find (ItemPlacerContainerObjectName);
		if (!container) {
			throw new UnityException ("No container object by name \"" + ItemPlacerContainerObjectName + "\" found.");
		}
		GameObject obj = new GameObject(itemObj.name + "Placer");
		GameObject instance = Instantiate (obj);
		instance.transform.parent = container.transform;
		ItemPlacer iPlacer = instance.AddComponent<ItemPlacer> ();
		iPlacer.__Init(itemObj, placeCondition, outcomeValid, outcomeInvalid);
		return instance;
	}

	public void __Init(GameObject itemObj, PlacementCondition placeCondition, Outcome outcomeValid, Outcome outcomeInvalid = null) {
		ItemObj = itemObj;
		PlaceCondition = placeCondition;
		OutcomeValid = outcomeValid;
		OutcomeInvalid = outcomeInvalid;

		PreviewLayer = LayerMask.NameToLayer (PreviewLayerName);
		PlacementLayer = LayerMask.NameToLayer (GridNodesLayerName);
		previewItem = initPreviewItem(itemObj);
	}

	//------------------------------------------------------------


	void InstantiateItemAt(GameObject cell) {
		if (!autoInstantiate) {
			return;
		}
		GameObject item;
		item = Instantiate (ItemObj);
		item.SetActive (true);

	}

	void MakePreview(bool valid) {
		if (previousCell == cell) {
			return;
		}
		cell.GetComponent<Cell> ().InstantiateIn (previewItem);
	}

	void ClearPreview() {
		Destroy (previewObj);
	}


	// Monobehaviour
	//------------------------------------------------------------

	void FixedUpdate() {
		if (!selected) {
			return;
		}
		clicked = Input.GetMouseButtonUp (0);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit, 100, 1 << PlacementLayer)) {
			Debug.Log ("Not currently hovering over any grid cell");
			return;
		}
		cell = hit.collider.gameObject;
		bool validPlacement = PlaceCondition (cell);
		if (clicked) {
			selected = validPlacement ? OutcomeValid (cell) : (OutcomeInvalid != null ? OutcomeInvalid (cell) : true);
			InstantiateItemAt (cell);
		} else {
			MakePreview(validPlacement);
		}
		if (!selected) {
			ClearPreview ();
		}
	}
}
