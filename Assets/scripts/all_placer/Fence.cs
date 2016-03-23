using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fence : MonoBehaviour {
	
	private Button myButton;
	private GameObject cur;
	private GameObject old = null;
	private GameObject tmp;
	private int rota = 0;

	public Material mat;
	public GameObject fence;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (add);
	}

	void add()
	{
		if (globals.i.Button != 2 && globals.i.Money >= 20)
			globals.i.Button = 2;
		else {
			if (old) {
				GameObject.Destroy (old.transform.FindChild ("fence " + rota).gameObject);
				old = null;
			}
			globals.i.Button = 0;
		}
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

		/*You can place the fence if you leftclick + you have pressed the button + you are on a tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 2 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid"))) {
			globals.i.Money -= 20;
			old.transform.FindChild ("fence " + rota).gameObject.tag = "noedit";
			old.transform.FindChild ("fence " + rota).gameObject.GetComponent<MeshRenderer>().material = mat;
			old.transform.FindChild ("fence " + rota).gameObject.GetComponents<NavMeshObstacle>()[0].enabled = true;
			old.transform.FindChild ("fence " + rota).gameObject.GetComponents<BoxCollider>()[0].enabled = true;
			old = null;
			globals.i.Button = 0;
		}

		/*Handle the rotation*/
		if (Input.GetMouseButtonUp (1) && globals.i.Button == 2 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid"))) {
			if (rota != 3)
				rota += 1;
			else
				rota = 0;
			if (cur) {
				foreach (Transform child in cur.transform) {
					if (child.tag == "edit")
						GameObject.Destroy (child.gameObject);
				}
			}
		}

		/*Moving object*/
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid")) && globals.i.Button == 2) {
			cur = GameObject.Find (hit.collider.name);
			if (check_pos(cur.transform)) {
				tmp = Instantiate (fence);
				tmp.transform.parent = cur.transform;
				rotate (tmp.transform);
				tmp.name = "fence " + rota;
				tmp.transform.localScale = new Vector3 (3F, 0.3F, 3F);
				if (old != cur) {
					if (old) {
						foreach (Transform child in old.transform) {
							if (child.tag == "edit")
								GameObject.Destroy (child.gameObject);
						}
					}
					old = cur;
				}
			}
		} else if (old) {
			GameObject.Destroy (old.transform.FindChild ("fence " + rota).gameObject);
			old = null;
		}
	}
}