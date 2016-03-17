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
	private string[] words;
	private int timenb;
	private float lastUpdate = Time.time;
	public Text time;
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
		words = time.text.Split (' ');
		timenb = IntParseFast(words[1]);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButtonUp (0) && clicked) {
			if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && old) {
				if (timenb == 0) {
					name = old.name;
					GameObject.Destroy (old.transform.GetChild (0).gameObject);
					ttmp = Instantiate (field);
					ttmp.transform.parent = old.transform.parent;
					ttmp.transform.localRotation = old.transform.localRotation;
					ttmp.transform.localPosition = old.transform.localPosition;
					ttmp.transform.localScale = old.transform.localScale;
					ttmp.name = name;
					GameObject.Destroy (old);
					time.text = "Ground: 60 s";
					timenb = 60;
				}
				old = null;
			}
			clicked = false;
		}
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && clicked && timenb == 0) {
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
		if(Time.time - lastUpdate >= 1f && timenb != 0){
			timenb -= 1;
			lastUpdate = Time.time;
		}
		time.text = "Ground: " + timenb + " s";
	}
}
