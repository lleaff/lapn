using UnityEngine;
using System.Collections;

public class ia_bunny : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.right * 5.0f * Time.deltaTime);
	
	}
}
