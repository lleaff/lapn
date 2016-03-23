using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Death_trap : MonoBehaviour {

	private Button myButton;
	private GameObject cur;
	private GameObject old = null;
	private GameObject tmp;

	public Material mat;
	public GameObject trap;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (add);
	}

	void add()
	{
		if (globals.i.Button != 5 && globals.i.Money >= 100)
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
			globals.i.Money -= 100;
			old.transform.FindChild ("trap").gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = mat;
			old.transform.FindChild ("trap").gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material = mat;
			old = null;
			globals.i.Button = 0;
		}

		/*Moving object*/
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("PlacementGrid")) && globals.i.Button == 5) {
			cur = GameObject.Find (hit.collider.name);
			if (hit.collider.name.Substring(0,9) != "FieldNode" && check_pos(cur.transform)) {
				tmp = Instantiate (trap);
				tmp.transform.parent = cur.transform;
				tmp.transform.localRotation = Quaternion.Euler (270, 0, 0);
				tmp.transform.localPosition = new Vector3(-2.5F,2.5F,0.65F);
				tmp.transform.localScale = new Vector3(15F,1F,15F);
				tmp.name = "trap";
				if (old != cur) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("trap").gameObject);
					old = cur;
				}
			}
		}
	}
}
