using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fence : MonoBehaviour {
	
	private Button myButton;
	private GameObject cur;
	private GameObject old = null;
	private GameObject tmp;
	private int rota = 0;
	private int delay = 15;

	public Material mat;
	public GameObject fence;

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
		if (globals.i.Button != 2 && globals.i.Money >= 20)
			globals.i.Button = 2;
		else 
			globals.i.Button = 0;
	}

	/**********
	 * Handle rotation
	 * ********/
	private void rotate(Transform obj) {
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

	/**********
	 * Check the pos for the 
	 * current rotation
	 * ********/
	private bool check_pos(Transform obj) {
		string fencerot = "fence " + rota;
		foreach (Transform child in obj) {
			if (child.name == fencerot)
				return (false);
		}
		return (true);
	}
		
	public void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool raycast = Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("PlacementGrid"));

		/*if left click + button selected + cursor on tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 2 && raycast) {
			globals.i.Money -= 20;
			old.transform.FindChild ("fence " + rota).gameObject.tag = "noedit";
			old.transform.FindChild ("fence " + rota).gameObject.GetComponent<MeshRenderer>().material = mat;
			old.transform.FindChild ("fence " + rota).gameObject.GetComponents<NavMeshObstacle>()[0].enabled = true;
			old.transform.FindChild ("fence " + rota).gameObject.GetComponents<BoxCollider>()[0].enabled = true;
			old = null;
			globals.i.Button = 0;
		}

		/*if press r + button selected + cursor on tile*/
		if (Input.GetKey(KeyCode.R) && delay == 0 && globals.i.Button == 2 && raycast) {
			if (rota != 3)
				rota += 1;
			else
				rota = 0;
			/*Clean last prev*/
			if (cur) {
				foreach (Transform child in cur.transform) {
					if (child.tag == "edit")
						GameObject.Destroy (child.gameObject);
				}
			}
			delay = 15;
		}

		/*if cursor on tile + button selected*/
		if (raycast && globals.i.Button == 2) {
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
		} else if (old && old.transform.FindChild ("fence " + rota)) {
			GameObject.Destroy (old.transform.FindChild ("fence " + rota).gameObject);
			old = null;
		}
		if (delay > 0)
			delay--;
	}
}