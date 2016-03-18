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
	private string names;
	private string[] words;
	private int timenb;
	private float lastUpdate = 16F;
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
				GameObject.Destroy (old.transform.FindChild ("fieldtile").gameObject);
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
		/*Split the money count*/
		words = time.text.Split (' ');
		timenb = IntParseFast(words[1]);

		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		/*You can place the fieldtile if you leftclick + you have pressed the button + you are on a tile + time is at 0 */
		if (Input.GetMouseButtonUp (0) && clicked && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && old && timenb == 0) {
			names = "FieldNode" + old.name.Substring (8);
		/*	GameObject.Destroy (old.transform.FindChild ("fieldtile").gameObject);*/
			ttmp = Instantiate (field);
			ttmp.transform.parent = old.transform.parent;
			ttmp.transform.localRotation = old.transform.localRotation;
			ttmp.transform.localPosition = old.transform.localPosition;
			ttmp.transform.localScale = old.transform.localScale;
			ttmp.name = names;

			/*dafuq c# sucks*/
			foreach (Transform child in old.transform) {
				if (child.name != "fieldtile")
					child.parent = ttmp.transform;
			}
			foreach (Transform child in old.transform) {
				if (child.name != "fieldtile")
					child.parent = ttmp.transform;
			}
			foreach (Transform child in old.transform) {
				if (child.name != "fieldtile")
					child.parent = ttmp.transform;
			}
			/*Madness end here*/

			GameObject.Destroy (old.transform.FindChild ("fieldtile").gameObject);
			GameObject.Destroy (old);
			time.text = "Ground: 60 s";
			timenb = 60;
			old = null;
			clicked = false;
		}
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && clicked && timenb == 0) {
			h = GameObject.Find (hit.collider.name);
			if (hit.collider.name.Substring(0,9) != "FieldNode" && h.transform.FindChild ("fieldtile") == null) {
				tmp = Instantiate (field);
				tmp.transform.parent = h.transform;
				tmp.transform.localRotation = Quaternion.identity;
				tmp.transform.localPosition = Vector3.zero;
				tmp.transform.localScale = Vector3.one;
				tmp.name = "fieldtile";
				if (old != h) {
					if (old)
						GameObject.Destroy (old.transform.FindChild ("fieldtile").gameObject);
					old = h;
				}
			}
		}
		if (lastUpdate == 16F)
			lastUpdate = Time.time;
		if(Time.time - lastUpdate >= 1f && timenb != 0){
			timenb -= 1;
			lastUpdate = Time.time;
		}
		time.text = "Ground: " + timenb + " s";
	}
}
