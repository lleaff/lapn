using UnityEngine;
using System.Collections;

public class ia : MonoBehaviour
{
	NavMeshAgent agent;
	GameObject destination;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update ()
	{
		GameObject[] carrots;

		carrots = GameObject.FindGameObjectsWithTag ("Carrot");
		if (carrots.Length != 0) {
			destination = get_nearest (carrots);
			agent.SetDestination (destination.transform.position);
		}
		if (destination == null) {
			agent.ResetPath();	
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

