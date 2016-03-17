using UnityEngine;
using System.Collections;

public class ia_bunny : MonoBehaviour {

	private GameObject carot;
	void Start () {

	}
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
		
			if (this.transform.position.x >= carot.transform.position.x - 1 && this.transform.position.x <= carot.transform.position.x + 1 && this.transform.position.z >= carot.transform.position.z - 1 && this.transform.position.z <= carot.transform.position.z + 1) {
				print ("On est dessus chef");
				GameObject.Destroy (carot);
			}
		}
	}
}
