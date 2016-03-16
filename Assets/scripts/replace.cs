using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class replace : MonoBehaviour {

	private Button myButton;
	private bool clicked = false;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;
	private GameObject ttmp;
	private string name;

	public GameObject field;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (addCarote);
	}

	void addCarote()
	{
		if (!clicked)
			clicked = true;
		else {
			if (old) {
				GameObject.Destroy (old.transform.GetChild (0).gameObject);
				old = null;
			}
			clicked = false;
		}
	}

	void FixedUpdate() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButtonUp (0) && clicked) {
			if (Physics.Raycast (ray, out hit, 100) & old) {
				name = old.name;
				GameObject.Destroy (old.transform.GetChild (0).gameObject);
				ttmp = Instantiate (field);
				ttmp.transform.parent = old.transform.parent;
				ttmp.transform.localRotation = old.transform.localRotation;
				ttmp.transform.localPosition = old.transform.localPosition;
				ttmp.transform.localScale = old.transform.localScale;
				ttmp.name = name;
				GameObject.Destroy (old);
				old = null;
			}
			clicked = false;
		}
		if (Physics.Raycast (ray, out hit, 100) && clicked) {
			h = GameObject.Find (hit.collider.name);
			if (h.transform.childCount == 0) {
				tmp = Instantiate (field);
				tmp.transform.parent = h.transform;
				tmp.transform.localRotation = Quaternion.identity;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = Vector3.one;
				if (old != h) {
					if (old)
						GameObject.Destroy (old.transform.GetChild (0).gameObject);
					old = h;
				}
			}
		}
	}
}
