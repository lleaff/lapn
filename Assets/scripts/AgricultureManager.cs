using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgricultureManager : MonoBehaviour {

	public static AgricultureManager i = null; /* Scene manager instance */

	public Vector3 CarrotGrowthVector;
	public int carrotGrowthBaseIntervalSeconds = 5;
	public float GlobalGrowthRate = 1;
	public int CarrotGrowthIntervalSeconds {
		get { return (int)(carrotGrowthBaseIntervalSeconds * GlobalGrowthRate); }
	}
	public int CarrotMaxGrowth = 12;
	public int CarrotMaturityGrowth;
	public float CarrotGrowthDistance;
	private GameObject grid = null;


	//------------------------------------------------------------

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
				Debug.Log (node);
				carrots.Add(CellUtils.GetCarrotObj (node));
			}
		}
		return carrots;
	}

	void UpdateCarrots() {
		Carrots = GetCarrots ();
	}

	/*
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
	}

	void Start() {
		/*
		StartCoroutine (CarrotGrowth ());
		*/

		grid = GameObject.Find ("Grid");
	}
}
