using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ia_carrots : MonoBehaviour {

	public int Growth = 0;
	public int MaxGrowth;
	public int CarrotGrowthIntervalSeconds;

	public List<GameObject> Carrots;

	void Awake() {
		
	}

	void Start () {
		Carrots = CellUtils.GetCarrots (gameObject);
		CarrotGrowthIntervalSeconds = AgricultureManager.i.CarrotGrowthIntervalSeconds;
		MaxGrowth = AgricultureManager.i.CarrotMaxGrowth;
		StartCoroutine (CarrotGrowth ());
	}

	IEnumerator CarrotGrowth() {
		while (true) {
			yield return new WaitForSeconds (CarrotGrowthIntervalSeconds);
			if (!Grow ())
				break;
		}
	}


	public bool Grow() {
		Growth++;
		if (Growth >= MaxGrowth) {
			return false;
		}
		foreach (var carrot in Carrots) {
			Vector3 pos = carrot.transform.localPosition;
			pos -= AgricultureManager.i.CarrotGrowthVector;
			carrot.transform.localPosition = pos;
		}
		if (Growth == AgricultureManager.i.CarrotMaturityGrowth) {
			tag = globals.carrotTag;
		}

		return true;
	}
}
