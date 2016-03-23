using UnityEngine;
using System.Collections;

public class agressive : MonoBehaviour {
	ia agg;
	public Material white;
	public Material brown;

	// Use this for initialization
	void Start () {
		agg = transform.parent.GetComponent<ia>();
	}
	
	// Update is called once per frame
	void Update () {
		if (agg.aggressive)
			gameObject.GetComponent<SkinnedMeshRenderer>().material = white;
		else
			gameObject.GetComponent<SkinnedMeshRenderer>().material = brown;
			
	}
}
