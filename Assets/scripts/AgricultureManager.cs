using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgricultureManager : MonoBehaviour {

	public static AgricultureManager i = null; /* Agriculture manager instance */

	public Vector3 CarrotGrowthVector;
	public int carrotGrowthBaseIntervalSeconds = 5;
	public int CarrotGrowthIntervalSeconds {
		get { return (int)(carrotGrowthBaseIntervalSeconds / GlobalGrowthRate); }
	}
	public int CarrotMaxGrowth = 12;
	public int CarrotDecayGrowth;
	public int CarrotMaturityGrowth;
	public float CarrotGrowthDistance;
	public int BiodegradationDelaySeconds;
	public Material DecayMaterial;
	private GameObject grid = null;


	//------------------------------------------------------------

	/*
	List<GameObject> Carrots;

	List<GameObject> GetCarrots() {
		if (grid == null) {
			grid = GameObject.Find ("Grid");
			if (grid == null)
				return null;
		}
		var carrots = new List<GameObject>();
		foreach (Transform node in grid.transform) {
			if (CellUtils.IsFieldNode (node)) {
				carrots.Add(CellUtils.GetCarrotObj (node));
			}
		}
		return carrots;
	}

	void UpdateCarrots() {
		Carrots = GetCarrots ();
	}

	public void GrowCarrots() {
		UpdateCarrots ();
		if (Carrots == null)
			return;
		foreach (GameObject carrot in Carrots) {
			Debug.Log ("Growing...");//DEBUG
			carrot.GetComponent<ia_carrots> ().Grow ();
		}
	}

	IEnumerator CarrotGrowth() {
		yield return new WaitForSeconds(CarrotGrowthIntervalSeconds);
		GrowCarrots ();
	}
	*/


	public float GlobalGrowthRate {
		get {
			float rate = 1f;
			float heat = WeatherManager.i.Heat;
			if (heat <= 0f) {
				rate = 0f;
			} else if (heat <= 25f) {
				rate *= 1f;
			} else if (heat <= 35f) {
				rate *= 1.25f;
			} else {
				rate *= 0.8f;
			}
			float humidity = WeatherManager.i.Humidity;
			if (humidity <= 1) {
				rate *= humidity;
			} else if (humidity <= 2f) {
				rate *= 1f;
			} else if (humidity <= 3f) {
				rate *= 1.25f;
			} else {
				rate *= 0.8f;
			}
			return rate;
		}
	}


	//------------------------------------------------------------

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		CarrotMaturityGrowth = CarrotMaxGrowth / 3;
		CarrotGrowthDistance = 0.6f / CarrotMaxGrowth;
		CarrotGrowthVector = new Vector3 (0, 0, CarrotGrowthDistance);
		CarrotDecayGrowth = CarrotMaxGrowth * 2;
		BiodegradationDelaySeconds = 20;
	}

	void Start() {
		/*
		StartCoroutine (CarrotGrowth ());
		*/

		grid = GameObject.Find ("Grid");
	}
}
