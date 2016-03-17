using UnityEngine;
using System.Collections;

public class ia_bunny : MonoBehaviour {

	private GameObject[] carrot;
	private GameObject nearest;
	private GameObject[] bunnys;
	public float speed;

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
				move_right ();
			else if (this.transform.position.x > nearest.transform.position.x)
				move_left ();
			 if (this.transform.position.z < nearest.transform.position.z)
				move_forward ();
			else if (this.transform.position.z > nearest.transform.position.z)
				move_back ();
		
			if (this.transform.position.x >= nearest.transform.position.x - 1 && this.transform.position.x <= nearest.transform.position.x + 1 && this.transform.position.z >= nearest.transform.position.z - 1 && this.transform.position.z <= nearest.transform.position.z + 1)
				GameObject.Destroy (nearest);
		}
	}

	void move_right()
	{
		Ray ray = new Ray (this.transform.position, this.transform.right);
		RaycastHit hit;
		Debug.DrawRay (this.transform.position, this.transform.right * 1, Color.red);
		bool collision = false;
		if (Physics.Raycast(this.transform.position, Vector3.right, 1, 1 << LayerMask.NameToLayer("Bunny")))
				collision = true;
		if (collision != true)
			this.transform.Translate (Vector3.right * speed * Time.deltaTime);
	}

	void move_left()
	{
		Ray ray = new Ray (this.transform.position, this.transform.right);
		RaycastHit hit;
		Debug.DrawRay (this.transform.position, this.transform.right * -1, Color.red);
		bool collision = false;
		if (Physics.Raycast(this.transform.position, Vector3.left, 1, 1 << LayerMask.NameToLayer("Bunny")))
			collision = true;
		if (collision != true)
			this.transform.Translate (Vector3.left * speed * Time.deltaTime);
	}

	void move_forward()
	{
		Ray ray = new Ray (this.transform.position, this.transform.forward);
		RaycastHit hit;
		Debug.DrawRay (this.transform.position, this.transform.forward * 1, Color.red);
		bool collision = false;
		if (Physics.Raycast(this.transform.position, Vector3.forward, 1, 1 << LayerMask.NameToLayer("Bunny")))
			collision = true;
		if (collision != true)
			this.transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void move_back()
	{
		Ray ray = new Ray (this.transform.position, this.transform.forward);
		RaycastHit hit;
		Debug.DrawRay (this.transform.position, this.transform.forward * -1, Color.red);
		bool collision = false;
		if (Physics.Raycast(this.transform.position, Vector3.back, 1, 1 << LayerMask.NameToLayer("Bunny")))
			collision = true;
		if (collision != true)
			this.transform.Translate (Vector3.back * speed * Time.deltaTime);
	}
}
