using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgricultureManager : MonoBehaviour {

	public static AgricultureManager i = null; /* Scene manager instance */

	public Vector3 CarrotGrowDistance;
	private GameObject grid;

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);


		CarrotGrowDistance = new Vector3(0, 0, 0.005f);
		grid = GameObject.Find ("Grid");
	}



	List<GameObject> Carrots;

	List<GameObject> GetCarrots() {
		var carrots = new List<GameObject>();
		foreach (Transform node in grid.transform) {
			if (CellUtils.IsFieldNode (node)) {
			}
		}
		return carrots;
	}

	void GrowCarrots() {
		
	}
}
