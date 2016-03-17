using UnityEngine;
using System.Collections;

public class ia_bunny : MonoBehaviour {

	private GameObject[] carrot;
	private GameObject nearest;
	public float speed;

	void Start () {

	}
	void Update () {
		carrot = GameObject.FindGameObjectsWithTag ("Carrot");
		if (carrot.Length != 0) {
			float distance = 100;
			float tmp;
			float distx;
			float distz;
			foreach (GameObject obj in carrot) {
				tmp = 0;
				distx = this.transform.position.x - obj.transform.position.x;
				if (distx < 0)
					distx *= -1;
				distz = this.transform.position.z - obj.transform.position.z;
				if (distz < 0)
					distz *= -1;
				tmp = distx + distz;
				if (tmp < distance) {
					distance = tmp;
					nearest = obj;
				}
			}					
			if (this.transform.position.x < nearest.transform.position.x)
				this.transform.Translate (Vector3.right * speed * Time.deltaTime);
			else if (this.transform.position.x > nearest.transform.position.x)
				this.transform.Translate (Vector3.left * speed * Time.deltaTime);
			if (this.transform.position.z < nearest.transform.position.z)
				this.transform.Translate (Vector3.forward * speed * Time.deltaTime);
			else if (this.transform.position.z > nearest.transform.position.z)
				this.transform.Translate (Vector3.back * speed * Time.deltaTime);
		
			if (this.transform.position.x >= nearest.transform.position.x - 1 && this.transform.position.x <= nearest.transform.position.x + 1 && this.transform.position.z >= nearest.transform.position.z - 1 && this.transform.position.z <= nearest.transform.position.z + 1)
				GameObject.Destroy (nearest);
		}
	}
}
