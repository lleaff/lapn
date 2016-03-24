using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Carrots : MonoBehaviour {

	private Button myButton;
	private GameObject cur;
	private GameObject old = null;
	private GameObject tmp;

	public Material[] mat;
	public GameObject field;


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
	private void add() {
		if (globals.i.Button != 1 && globals.i.Money >= 10)
			globals.i.Button = 1;
		else
			globals.i.Button = 0;
	}

	/**********
	 * Set materials to end prev
	 * ********/
	private void set_mat(GameObject obj) {
		foreach (Transform child in obj.transform) {
			child.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material = mat [0];
			child.GetChild (1).gameObject.GetComponent<MeshRenderer> ().material = mat [1];
		}
	}

	/**********
	 * Find the last field, play sound
	 * and start the script
	 * ********/
	private void PlantCarrots() {
		GameObject field = old.transform.FindChild ("field").gameObject;
		if (field) {
			set_mat (field);
			field.tag = "seed";
			field.GetComponent<AudioSource> ().Play ();
			field.GetComponent<ia_carrots> ().enabled = true;
		}
		old = null;
	}
		
	/**********
	 * Check field tag for 
	 * single placement
	 * ********/
	private bool check_tag(Transform obj) {
		string[] tags = new string[5];
		tags [0] = "seed";
		tags [1] = "Carrot";
		tags [2] = "eaten";
		tags [3] = "unattainable";
		tags [4] = "decayed";
		for (int i = 0; i < 5; i++) {
			if (obj.FindChild ("field").CompareTag (tags [i]))
				return (false);
		}
		return (true);
	}

	public void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool raycast = Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("PlacementGrid"));

		/*if left click + button selected + cursor on tile + field tile + not tagged as placed*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 1 && raycast && hit.collider.name.Substring(0,9) == "FieldNode" && check_tag(hit.collider.transform)) {
			globals.i.Money -= 10;
			PlantCarrots ();
			globals.i.Button = 0;
		}

		/*if cursor on tile + button selected*/
		if (raycast && globals.i.Button == 1) {
			cur = GameObject.Find (hit.collider.name);
			if (cur.transform.FindChild ("field") == null && hit.collider.name.Substring (0, 9) == "FieldNode") {
				tmp = Instantiate (field);
				tmp.transform.parent = cur.transform;
				tmp.transform.localRotation = Quaternion.identity;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = Vector3.one;
				tmp.name = "field";
				tmp.tag = "edit";
				if (old != cur) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("field").gameObject);
					old = cur;
				}
			}
		} else if (old) {
			GameObject.Destroy (old.transform.FindChild ("field").gameObject);
			old = null;
		}
	}
}
