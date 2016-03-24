using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cooldown : MonoBehaviour {

	public GameObject coolDown;

	// Monobehaviour
	//------------------------------------------------------------

	void Update () {
		if (this.GetComponent<Replace>().Time <= 5)
			coolDown.transform.localScale = new Vector3 (1, 1 - ((this.GetComponent<Replace>().Time) * 0.2F), 1);
		else
			coolDown.transform.localScale = new Vector3 (1, 0, 1);
	}
}
