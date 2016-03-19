using UnityEngine;
using System.Collections;

public class sampleagentscript : MonoBehaviour {

	NavMeshAgent agent;
	private GameObject nearest;
	private GameObject[] carrot;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	void Update () {
		carrot = GameObject.FindGameObjectsWithTag ("Carrot");
		if (carrot.Length != 0) {
			float distance = 1000;
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
			agent.SetDestination (nearest.transform.position);
			NavMeshPath path = new NavMeshPath ();
			bool hasFoundPath = agent.CalculatePath (nearest.transform.position, path);
			/*destroy fence*/
			if (path.status == NavMeshPathStatus.PathPartial)
				agent.SetDestination (Vector3.zero);
			else if (path.status == NavMeshPathStatus.PathInvalid) {
				print ("The agent cannot reach the destination");
				print ("hasFoundPath will be false");
			}
		}


	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.name == "field")
		{
			Destroy(col.collider.gameObject);
			Destroy (this.gameObject);
		}
	}
}
