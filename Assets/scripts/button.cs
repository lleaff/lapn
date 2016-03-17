using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class button : MonoBehaviour {

	private Button myButton;
	private bool clicked = false;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;
	private string[] words;
	private int moneynb;
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

	void FixedUpdate() {
		words = money.text.Split (' ');
		moneynb = IntParseFast(words[1]);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButtonUp (0) && clicked) {
			if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && hit.collider.name.Substring(0,9) == "FieldNode") {
				if (moneynb >= 10) {
					money.text = "Money: " + (moneynb-10) + " $";
					old.transform.GetChild (0).gameObject.GetComponent<AudioSource> ().Play ();
					old.transform.GetChild (0).gameObject.tag = "Carrot";
				}
				old = null;
			}
			clicked = false;
		}
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && clicked && moneynb >= 10) {
			h = GameObject.Find (hit.collider.name);
			if (h.transform.childCount == 0 && hit.collider.name.Substring(0,9) == "FieldNode") {
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
