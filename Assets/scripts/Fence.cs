using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fence : MonoBehaviour {

	private Button myButton;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;
	private int rota = 0;
	public GameObject field;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (addCarote);
	}

	void addCarote()
	{
		if (Globals.i.Button != 2)
			Globals.i.Button = 2;
		else {
			if (old) {
				GameObject.Destroy (old.transform.FindChild ("fence " + rota).gameObject);
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

	void rotate(Transform obj) {
		if (rota == 0) {
			obj.localRotation = Quaternion.Euler (90, 180, 0);
			obj.localPosition = new Vector3 (0F, -5F, -0.1F);
		} else if (rota == 1) {
			obj.localRotation = Quaternion.Euler (90, 180, 0) * Quaternion.Euler (0, 90, 0);
			obj.localPosition = new Vector3 (-5F, 0F, -0.1F);
		} else if (rota == 2) {
			obj.localRotation = Quaternion.Euler (90, 180, 0) * Quaternion.Euler (0, 180, 0);
			obj.localPosition = new Vector3 (0F, 5F, -0.1F);
		} else if (rota == 3) {
			obj.localRotation = Quaternion.Euler (90, 180, 0) * Quaternion.Euler (0, 270, 0);
			obj.localPosition = new Vector3 (5F, 0F, -0.1F);
		}
	}

	bool check_pos(Transform obj) {
		string fencerot = "fence " + rota;
		foreach (Transform child in obj) {
			if (child.name == fencerot)
				return (false);
		}
		return (true);
	}

	void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		/*You can place the fence if you leftclick + you have pressed the button + you are on a tile + you have the money*/
		if (Input.GetMouseButtonUp (0) && Globals.i.Button == 2 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid")) && Globals.i.Money >= 20) {
			Globals.i.Money -= 20;
			old.transform.FindChild ("fence " + rota).gameObject.tag = "noedit";
			old.transform.FindChild ("fence " + rota).gameObject.GetComponents<NavMeshObstacle>()[0].enabled = true;
			old.transform.FindChild ("fence " + rota).gameObject.GetComponents<BoxCollider>()[0].enabled = true;
			old = null;
			Globals.i.Button = 0;
		}

		/*Handle the rotation*/
		if (Input.GetMouseButtonUp (1) && Globals.i.Button == 2 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid"))) {
			if (rota != 3)
				rota += 1;
			else
				rota = 0;
			if (h) {
				foreach (Transform child in h.transform) {
					if (child.tag == "edit")
						GameObject.Destroy (child.gameObject);
				}
			}
		}

		/*Moving object*/
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid")) && Globals.i.Button == 2 && Globals.i.Money >= 20) {
			h = GameObject.Find (hit.collider.name);
			if (check_pos(h.transform)) {
				tmp = Instantiate (field);
				tmp.transform.parent = h.transform;
				rotate (tmp.transform);
				tmp.name = "fence " + rota;
				tmp.transform.localScale = new Vector3 (3F, 0.3F, 3F);
				if (old != h) {
					if (old) {
						foreach (Transform child in old.transform) {
							if (child.tag == "edit")
								GameObject.Destroy (child.gameObject);
						}
					}
					old = h;
				}
			}
		}
	}
}