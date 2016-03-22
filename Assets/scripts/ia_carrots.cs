using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ia_carrots : MonoBehaviour {

	public int Growth = 0;
	public int MaxGrowth = 3;

	public List<GameObject> Carrots;

	void Awake() {
		
	}

	void Start () {
		Carrots = CellUtils.GetCarrots (gameObject);
	}

	int i = 0;
	void Update() {
		i++;
		if ((i % 100) == 0)
			Grow ();
	}

	public bool Grow() {
		Growth++;
		if (Growth >= MaxGrowth) {
			return false;
		}
		foreach (var carrot in Carrots) {
			Vector3 pos = carrot.transform.localPosition;
			pos -= AgricultureManager.i.CarrotGrowDistance;
			carrot.transform.localPosition = pos;
		}
		return true;
	}
}
