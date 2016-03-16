using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class button : MonoBehaviour {

	private Button myButton;
	private bool clicked = false;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;

	public GameObject field;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (addCarote);
	}

	void addCarote()
	{
		clicked = true;
		print ("Carrroooottte");		
	}

	void FixedUpdate() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButtonUp (0) && clicked) {
			if (Physics.Raycast (ray, out hit, 100)) {
				print (hit.collider.name);
			}
			clicked = false;
		}
		if (Physics.Raycast (ray, out hit, 100)) {
			h = GameObject.Find (hit.collider.name);
			if (h.transform.childCount == 0) {
				tmp = Instantiate (field);
				tmp.transform.parent = h.transform;
				tmp.transform.localRotation = Quaternion.identity;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = Vector3.one;
				if (old != h) {
					if (old) {
						GameObject.Destroy (old.transform.GetChild (0).gameObject);
					}
					old = h;
				}
			}
		}
	}
}
