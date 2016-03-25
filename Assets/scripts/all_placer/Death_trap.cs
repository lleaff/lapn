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
		if (globals.i.Button != 5 && globals.i.Money >= 100)
			globals.i.Button = 5;
		else
			globals.i.Button = 0;
	}

	public void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool raycast = Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("PlacementGrid"));

		/*if left click + button selected + cursor on tile + not field tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 5 && raycast && hit.collider.name.Substring(0,9) != "FieldNode") {
			globals.i.Money -= 100;
			old.transform.FindChild ("trap").gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = mat;
			old.transform.FindChild ("trap").gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material = mat;
			old.transform.FindChild ("trap").GetComponent<BoxCollider> ().enabled = true;
			old = null;
			globals.i.Button = 0;
		}

		/*if cursor on tile + button selected*/
		if (raycast && globals.i.Button == 5) {
			cur = GameObject.Find (hit.collider.name);
			if (hit.collider.name.Substring(0,9) != "FieldNode" && cur.transform.FindChild ("trap") == null) {
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
		} else if (old) {
			GameObject.Destroy (old.transform.FindChild ("trap").gameObject);
			old = null;
		}
	}
}
