using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour {
	Button myButton;
	bool clicked = false;
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
		
	void Update()
	{
		if (Input.GetMouseButtonUp (0) && clicked) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				print (hit.collider.name);
			}
			clicked = false;
		}
	}
}
