using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Remover : MonoBehaviour {

	private Button myButton;
	
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
		if (globals.i.Button != 6 && globals.i.Money >= 20)
			globals.i.Button = 6;
		else 
			globals.i.Button = 0;
	}

	public void FixedUpdate() {
		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool raycast = Physics.Raycast (ray, out hit, 100);

		/*if left click + button selected + cursor on tile*/
		if (Input.GetMouseButtonUp (0) && globals.i.Button == 6 && raycast) {
			if ((hit.collider.name == "fence 0" || hit.collider.name == "fence 1" || hit.collider.name == "fence 2" || hit.collider.name == "fence 3" || hit.collider.name == "trap") && !hit.collider.CompareTag("noedit_destroy")) {
				Destroy (hit.collider.gameObject);
				globals.i.Money -= 20;
				globals.i.Button = 0;
			}
		}
	}
}
