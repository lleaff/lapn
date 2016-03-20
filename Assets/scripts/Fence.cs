using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fence : MonoBehaviour {

	private Button myButton;
	private GameObject global;
	private GameObject h;
	private GameObject old = null;
	private GameObject tmp;
	private string[] words;
	private int moneynb;
	private static int rota = 0;
	public GameObject field;
	public Text money;

	void Awake()
	{
		myButton = GetComponent<Button>();
		global = GameObject.Find ("GlobalValue");
		myButton.onClick.AddListener (addCarote);
	}

	void addCarote()
	{
		if (global.GetComponent<globalValue> ().Button != 2)
			global.GetComponent<globalValue> ().Button = 2;
		else {
			if (old) {
				GameObject.Destroy (old.transform.FindChild ("fence " + rota).gameObject);
				old = null;
			}
			global.GetComponent<globalValue> ().Button = 0;
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

	bool check_pos(Transform obj) {
		string fencerot = "fence " + rota;
		foreach (Transform child in obj) {
			if (child.name == fencerot)
				return (false);
		}
		return (true);
	}

	void FixedUpdate() {
		/*Split the money count*/
		words = money.text.Split (' ');
		moneynb = IntParseFast(words[1]);

		/*Raycast for the cusor position*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		/*You can place the fence if you leftclick + you have pressed the button + you are on a tile + you have the money*/
		if (Input.GetMouseButtonUp (0) && global.GetComponent<globalValue> ().Button == 2 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground")) && moneynb >= 20) {
			money.text = "Money: " + (moneynb-20) + " $";
			old.transform.FindChild ("fence " + rota).gameObject.tag = "noedit";
			old.transform.FindChild ("fence " + rota).gameObject.GetComponents<NavMeshObstacle>()[0].enabled = true;
			old = null;
			global.GetComponent<globalValue> ().Button = 0;
		}

		/*Handle the rotation*/
		if (Input.GetMouseButtonUp (1) && global.GetComponent<globalValue> ().Button == 2 && Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer("ground"))) {
			if (old) {
				foreach (Transform child in old.transform) {
					if (child.tag == "edit")
						GameObject.Destroy (child.gameObject);
				}
			}
			if (rota != 3)
				rota++;
			else
				rota = 0;
		}

		/*Moving object*/
		if (Physics.Raycast (ray, out hit, 100, 1 << LayerMask.NameToLayer ("ground")) && global.GetComponent<globalValue> ().Button == 2 && moneynb >= 20) {
			h = GameObject.Find (hit.collider.name);
			if (check_pos(h.transform)) {
				tmp = Instantiate (field);
				tmp.transform.parent = h.transform;
				rotate (tmp.transform);
				tmp.name = "fence " + rota;
				tmp.transform.localScale = new Vector3 (3F, 0.3F, 3F);
				if (old != h) {
					if (old) {
						foreach (Transform child in old.transform) {
							if (child.tag == "edit")
								GameObject.Destroy (child.gameObject);
						}
					}
					old = h;
				}
			}

			/*Real time update*/
			foreach (Transform child in old.transform) {
				if (child.tag == "edit") {
					rotate (child.transform);
					tmp.name = "fence " + rota;
				}
			}
		}
	}
}