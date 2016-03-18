using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fence : MonoBehaviour {

	private Button myButton;
	private bool clicked = false;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;
	private string[] words;
	private int moneynb;
	private int rota = 0;
	public GameObject field;
	public Text money;

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

	public static int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}

	void rotate(Transform obj) {
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

	void FixedUpdate() {
		words = money.text.Split (' ');
		moneynb = IntParseFast(words[1]);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButtonUp (0) && clicked) {
			if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground"))) {
				if (moneynb >= 20) {
					money.text = "Money: " + (moneynb-20) + " $";
					old.transform.GetChild (0).gameObject.tag = "noedit";
				}
				old = null;
			}
			clicked = false;
		}
		if (Input.GetMouseButtonUp (1) && clicked) {
			if (rota != 3)
				rota++;
			else
				rota = 0;
		}
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("ground")) && clicked && moneynb >= 20) {
			h = GameObject.Find (hit.collider.name);
			if (h.transform.childCount == 0) {
				tmp = Instantiate (field);
				tmp.transform.parent = h.transform;
				rotate (tmp.transform);
				tmp.transform.localScale = new Vector3 (3F, 0.3F, 3F);
				if (old != h) {
					if (old)
						GameObject.Destroy (old.transform.GetChild (0).gameObject);
					old = h;
				}
			}
			foreach (Transform child in old.transform) {
				if (child.tag == "edit") {
					rotate (child.transform);
				}
			}
		}
	}
}