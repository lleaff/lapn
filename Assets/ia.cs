using UnityEngine;
using System.Collections;

public class ia : MonoBehaviour
{
	NavMeshAgent agent;
	GameObject destination;
	bool retreat = true;
	GameObject[] spawn_positions = new GameObject[5];

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		for (int i = 0; i < GameObject.Find ("Rabbit generator").transform.childCount; i++)
			spawn_positions [i] = GameObject.Find ("Rabbit generator").transform.GetChild (i).gameObject;
		}	

	void Update ()
	{
		GameObject[] carrots;

		carrots = GameObject.FindGameObjectsWithTag ("Carrot");
		if (carrots.Length != 0) {
			destination = get_nearest (carrots);
			agent.SetDestination (destination.transform.position);
			retreat = false;
		}

		if (destination == null && retreat == false) {
			agent.ResetPath();
			GameObject ret_dest = get_nearest (spawn_positions);
			agent.SetDestination (ret_dest.transform.position);
			/*agent.SetDestination (Vector3.zero);*/
			retreat = true;
		}

	}

	GameObject get_nearest(GameObject[] objects)
	{
		float distance = 1000;
		float tmp;
		float distx;
		float distz;
		GameObject nearest = null;

		foreach (GameObject obj in objects) {
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
		if (nearest == null) {
			nearest = new GameObject ();
			nearest.transform.position = Vector3.zero;
		}
		return (nearest);
	}
}

