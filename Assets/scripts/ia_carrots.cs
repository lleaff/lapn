using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ia_carrots : MonoBehaviour {

	public int Growth = 0;
	public int MaxGrowth;
	public int DecayGrowth;
	public int CarrotGrowthIntervalSeconds {
		get { return (int)(AgricultureManager.i.CarrotGrowthIntervalSeconds / GrowthRate); }
	}
	public float GrowthRate = 1;

	public List<GameObject> Carrots;

	void Awake() {
		
	}

	void Start () {
		Carrots = CellUtils.GetCarrots (gameObject);
		MaxGrowth = AgricultureManager.i.CarrotMaxGrowth;
		DecayGrowth = AgricultureManager.i.CarrotDecayGrowth;
		StartCoroutine (CarrotGrowth ());
	}

	IEnumerator CarrotGrowth() {
		while (true) {
			//Debug.Log ("Growth rate: " + AgricultureManager.i.CarrotGrowthIntervalSeconds.ToString() + "...." + CarrotGrowthIntervalSeconds.ToString ());//DEBUG
			yield return new WaitForSeconds (CarrotGrowthIntervalSeconds);
			if (!Grow ())
				break;
		}
	}

	public void Decay() {
		Set_mat (AgricultureManager.i.DecayMaterial);
		tag = globals.decayedTag;
		StartCoroutine (DecayBioDegradation ());
	}

	IEnumerator DecayBioDegradation() {
		yield return new WaitForSeconds (AgricultureManager.i.BiodegradationDelaySeconds);
		foreach (var carrot in Carrots) {
			if (carrot == null) {
				continue;
			}
			Destroy (carrot.gameObject);
		}
		Destroy (gameObject);
	}


	public bool Grow() {
		Growth++;
		if (Growth >= MaxGrowth) {
			if (Growth >= DecayGrowth) {
				Decay ();
				return false;
			}
			return true;
		}
		foreach (var carrot in Carrots) {
			if (carrot == null) {
				continue;
			}
			Vector3 pos = carrot.transform.localPosition;
			pos -= AgricultureManager.i.CarrotGrowthVector;
			carrot.transform.localPosition = pos;
		}
		if (Growth == AgricultureManager.i.CarrotMaturityGrowth) {
			tag = globals.carrotTag;
		}

		return true;
	}

	public bool RemoveCarrot() {
		int childCount = transform.childCount;
		if (childCount <= 0) {
			return false;
		}
		GameObject carrot = transform.GetChild (Random.Range(0, childCount - 1)).gameObject;
		if (!carrot || !CellUtils.IsCarrot(carrot)) {
			return false;
		}
		Destroy (carrot);
		if (childCount == 1) {
			Destroy (gameObject);
		}
		return true;
	}

	void Set_mat(Material material) {
		foreach (Transform child in transform) {
			child.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material = material;
			child.GetChild (1).gameObject.GetComponent<MeshRenderer> ().material = material;
		}
	}

	public void Eaten()
	{
		gameObject.tag = "eaten";
		StartCoroutine (check_eaten());
	}

	IEnumerator check_eaten()
	{
		yield return new WaitForSeconds (5);
		if (gameObject.CompareTag("eaten"))
			gameObject.tag = "Carrot";
	}
}
