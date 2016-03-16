using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class button : MonoBehaviour {
	private Button myButton;
	private bool clicked = false;
	private GameObject h;
	private Renderer[] r;
	private GameObject old = null;
	public Color highlight = new Color(159,159,159,0.4F);

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
			r = h.GetComponents<Renderer>();
			r[0].material.color = highlight;
			if (old != h) {
				if (old) {
					r = old.GetComponents<Renderer> ();
					r [0].material.color = Color.white;
				}
				old = h;
			}
		}
	}
}
