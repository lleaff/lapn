using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public UI i = null;

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}


	bool itemPlacerSelected = false;
	public bool ItemPlacerSelect() {
		if (itemPlacerSelected) {
			return false;
		}
		itemPlacerSelected = true;
		return true;
	}
}
