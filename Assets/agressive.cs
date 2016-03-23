using UnityEngine;
using System.Collections;

public class agressive : MonoBehaviour {
	private static bool agg;
	public Material white;
	public Material brown;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		agg = ia.aggressive;
		if (agg)
			gameObject.GetComponent<SkinnedMeshRenderer>().material = white;
		else
			gameObject.GetComponent<SkinnedMeshRenderer>().material = brown;
			
	}
}
