using UnityEngine;
using System.Collections;

public class sampleagentscript : MonoBehaviour {

	NavMeshAgent agent;
	private GameObject nearest;
	private bool fence = false;
	public GameObject[] pos;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	void Update () {
		bool hasFoundPath;
			bool carrot_exist = find_nearest ("Carrot");
		if (nearest != null && carrot_exist == true) {
			agent.SetDestination (nearest.transform.position);
			
			NavMeshPath path = new NavMeshPath ();
			if (agent != null) {
				hasFoundPath = agent
				.CalculatePath (nearest
				.transform
				.position, path);
				/*destroy fence*/
				if (path.status == NavMeshPathStatus.PathPartial || hasFoundPath == false) {
					bunny_retreat ();	
					/*carrot = GameObject.FindGameObjectsWithTag ("noedit");
				if (carrot.Length != 0) {
					distance = 1000;
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
					fence = true;
					agent.SetDestination (nearest.transform.position);*/

				}
			}
		}
	}
		

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.name == "field" && !fence)
		{
			Destroy(col.collider.gameObject);
			Destroy (this.gameObject);
		}
		else if (col.collider.gameObject.tag == "noedit" && fence)
		{
			Destroy(col.collider.gameObject);
			fence = false;
		}
	}

	bool find_nearest(string to_find)
	{
		GameObject[] objects;
		float distance = 1000;
		float tmp;
		float distx;
		float distz;

		objects = GameObject.FindGameObjectsWithTag (to_find);
		if (objects.Length != 0) {
			foreach (GameObject obj in objects) {
				tmp = 0;
				distx = this.transform.position.x - obj.transform.position.x;
				if (distx < 0)
					distx *= -1;
				distz = this.transform.position.z - obj.transform.position.z;
				if (distz < 0)
					distz *= -1;
				tmp = distx + distz;
				if (tmp
					< distance) {
					distance = tmp;
					nearest = obj;
				}
			}
			return (true);
		}
		return (false);
	}

	void bunny_retreat()
	{
		GameObject new_dest = pos[0];
		float distance = 1000;
		float tmp;
		float distx;
		float distz;

		foreach (GameObject obj in pos) {
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
				new_dest = obj;
			}
		}
		agent.SetDestination (new_dest.transform.position);

		float dist = agent.remainingDistance; 

		if (dist != Mathf.Infinity && agent.pathStatus==NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
			Destroy (this.gameObject);
	}
}
