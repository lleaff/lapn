using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Replace : MonoBehaviour {

	private Button myButton;
	private GameObject cur;
	private GameObject old = null;
	private GameObject tmp;
	private GameObject ttmp;
	private int time;
	private int oldtime = -1;

	public GameObject ground;
	public int timer = 5;

	/**********
	 * Bind the button with a listener
	 * ********/
	public void Awake() {
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (add);
	}

	/**********
	 * Set current button
	 * and check for the timer
	 * ********/
	public void add() {
		if (globals.i.Button != 3 && (time - oldtime) >= timer)
			globals.i.Button = 3;
		else 
			globals.i.Button = 0;
	}

	/**********
	 * Update the time
	 * with timemanager
	 * ********/
	public void Update() {
		if (oldtime == -1)
			oldtime = TimeManager.i.Seconds;
		time = TimeManager.i.Seconds;
		if ((time - oldtime) >= timer)
			time = timer + oldtime;
	}

	public void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool raycast = Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("PlacementGrid"));

		/*if left click + button selected + cursor on tile + old tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 3 && raycast && old) {
			ttmp = Instantiate (ground);
			ttmp.transform.parent = old.transform.parent;
			ttmp.transform.localRotation = old.transform.localRotation;
			ttmp.transform.localPosition = old.transform.localPosition;
			ttmp.transform.localScale = old.transform.localScale;
			ttmp.name = "FieldNode" + old.name.Substring (8);
			ttmp.GetComponents<BoxCollider> ()[0].enabled = true;
			int count = old.transform.childCount;
			int off = 0;
			for (int i = 0; i < count; i++) {
				if (old.transform.GetChild (off).name != "fieldtile")
					old.transform.GetChild (off).parent = ttmp.transform;
				else
					off++;
			}
			GameObject.Destroy (old.transform.FindChild ("fieldtile").gameObject);
			GameObject.Destroy (old);
			old = null;
			globals.i.Button = 0;
			oldtime = TimeManager.i.Seconds;
		}

		/*if cursor on tile + button selected*/
		if (raycast && globals.i.Button == 3) {
			cur = GameObject.Find (hit.collider.name);
			if (hit.collider.name.Substring(0,9) != "FieldNode" && cur.transform.FindChild ("fieldtile") == null && cur.transform.FindChild ("trap") == null) {
				tmp = Instantiate (ground);
				tmp.transform.parent = cur.transform;
				tmp.transform.localRotation = Quaternion.identity;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = Vector3.one;
				tmp.name = "fieldtile";
				if (old != cur) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("fieldtile").gameObject);
					old = cur;
				}
			}
		} else if (old) {
			GameObject.Destroy (old.transform.FindChild ("fieldtile").gameObject);
			old = null;
		}
	}
}
