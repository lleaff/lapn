using UnityEngine;
using System.Collections;

public class ia_bunny : MonoBehaviour {

	private GameObject carot;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (carot = GameObject.Find ("FieldOb(Clone)")) {
			if (this.transform.position.x < carot.transform.position.x)
				this.transform.Translate (Vector3.right * 5.0f * Time.deltaTime);
			else if (this.transform.position.x > carot.transform.position.x)
				this.transform.Translate (Vector3.left * 5.0f * Time.deltaTime);
			if (this.transform.position.z < carot.transform.position.z)
				this.transform.Translate (Vector3.forward * 5.0f * Time.deltaTime);
			else if (this.transform.position.z > carot.transform.position.z)
				this.transform.Translate (Vector3.back * 5.0f * Time.deltaTime);
		}
	}
}
