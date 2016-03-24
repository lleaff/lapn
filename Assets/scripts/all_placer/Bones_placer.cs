using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bones_placer : MonoBehaviour {

	private Button myButton;
	private GameObject cur;
	private GameObject old = null;
	private GameObject tmp;
	private Vector3 tmppos;

	public GameObject bones;

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
		if (globals.i.Button != 8 && globals.i.Money >= 10)
			globals.i.Button = 8;
		else
			globals.i.Button = 0;
	}

	public void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool raycast = Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("PlacementGrid"));

		/*if left click + button selected + cursor on tile + not field tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 8 && raycast && hit.collider.name.Substring(0,9) != "FieldNode") {
			// Place bone
			globals.i.Money -= 10;
			tmppos = tmp.transform.position;
			Destroy (tmp.gameObject);
			tmp = Instantiate (bones);
			tmp.transform.localPosition = tmppos;
			tmp.name = "bones";
/*			tmp.GetComponent<BoxCollider> ().enabled = true;
			tmp.GetComponent<SphereCollider> ().enabled = true;
			tmp.GetComponent<NavMeshAgent> ().enabled = true;
			tmp.GetComponent<ia_dog> ().enabled = true;*/
			old = null;
			globals.i.Button = 0;
			BonesManager.i.Add (tmp);
		}

		/*if cursor on tile + button selected*/
		if (raycast && globals.i.Button == 8) {
			cur = GameObject.Find (hit.collider.name);
			if (hit.collider.name.Substring(0,9) != "FieldNode" && cur.transform.FindChild ("bones") == null) {
				tmp = Instantiate (bones);
				tmp.transform.parent = cur.transform;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = new Vector3(10F,1F,10F);
				tmp.name = "bones";
				if (old != cur) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("bones").gameObject);
					old = cur;
				}
			}
		} else if (old) {
			GameObject.Destroy (old.transform.FindChild ("bones").gameObject);
			old = null;
		}
	}


	public GameObject InstantiateBoneAt(){
		return null;
	}
}
