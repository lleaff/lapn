using UnityEngine;
using System.Collections;

public class ia : MonoBehaviour
{
	NavMeshAgent agent;
	Animation anim;
	GameObject destination;
	bool retreat = true;
	GameObject[] spawn_positions = new GameObject[5];
	bool eat = false;
	public GameObject gridnode;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animation> ();
		for (int i = 0; i < GameObject.Find ("Rabbit generator").transform.childCount; i++)
			spawn_positions [i] = GameObject.Find ("Rabbit generator").transform.GetChild (i).gameObject;
	}	

	void Update ()
	{
		GameObject[] carrots;
		NavMeshPath path = new NavMeshPath();

		carrots = GameObject.FindGameObjectsWithTag ("Carrot");
		if (carrots.Length != 0 && !eat) {
			destination = get_nearest (carrots);
			agent.SetDestination (destination.transform.position);
			anim.Play ("hop");
			retreat = false;
			bool hasFoundPath = agent.CalculatePath(destination.transform.position, path);
		}

		if ((destination == null || destination.transform.CompareTag("eated")) && retreat == false && !eat) {
			agent.ResetPath();
			GameObject ret_dest = get_nearest (spawn_positions);
			agent.SetDestination (ret_dest.transform.position);
			retreat = true;
		}

		if(path.status == NavMeshPathStatus.PathComplete)
		{
			print("The agent can reach the destionation");
		}
		else if(path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
		{
			print("||||||||The agent can't reach the destionation");
			agent.ResetPath();
			anim.Play ("idle2");
			retreat = false;
		}
	}

	float get_distance(GameObject obj)
	{
		float tmp;
		float distx;
		float distz;

		tmp = 0;
		distx = this.transform.position.x - obj.transform.position.x;
		if (distx < 0)
			distx *= -1;
		distz = this.transform.position.z - obj.transform.position.z;
		if (distz < 0)
			distz *= -1;
		tmp = distx + distz;
		return (tmp);
	}

	GameObject get_nearest(GameObject[] objects, int index)
	{
		GameObject nearest = null;
		bool sorted = false;
		int size = objects.Length;
			
		while (!sorted)
		{
			sorted = true;
			for (int i = 0; i < objects.Length; i++)
			{
				if (get_distance (objects [i]) > get_distance (objects [i + 1])) {
					swap (objects [i], objects [i + 1]);
					sorted = false;
				}
			}
		}
		if (nearest == null) {
			nearest = new GameObject ();
			nearest.transform.position = Vector3.zero;
		}
		return (nearest);
	}

	GameObject get_next_nearest(GameObject[] tab, GameObject current)
	{
		float current_dist;
		float tmp_dist;

		tmp_dist = get_distance (tab [0]);
		foreach (GameObject obj in tab)
			if (tmp_dist > get_distance(obj))
				get_distance(obj)

	}

	IEnumerator OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.name == "field" && col.collider.gameObject.CompareTag("Carrot"))
		{
			col.collider.gameObject.tag = "eated";
			agent.ResetPath();	
			eat = true;
			anim.Play ("idle1");
			StartCoroutine(removeCarrots(col.collider.gameObject));
			yield return new WaitForSeconds(6);
			eat = false;
			GameObject parent = col.collider.transform.parent.gameObject;
			Destroy(col.collider.gameObject);
			Destroy (this.gameObject);
			removefield (parent);
		}
	}


	IEnumerator removeCarrots (GameObject fieldnode)
	{
		int count = fieldnode.transform.childCount;
		for (int i = 0; i < count; i++)
		{
			yield return new WaitForSeconds(0.5f);
			Destroy (fieldnode.transform.GetChild(0).gameObject);
		}
	}

	void removefield(GameObject fieldnode){
		int count = fieldnode.transform.childCount;
		GameObject new_tile = Instantiate (gridnode);
		new_tile.transform.parent = fieldnode.transform.parent;
		new_tile.transform.localRotation = fieldnode.transform.localRotation;
		new_tile.transform.localPosition = fieldnode.transform.localPosition;
		new_tile.transform.localScale = fieldnode.transform.localScale;
		new_tile.name = "GridNode" + fieldnode.name.Substring (9);
		new_tile.layer = fieldnode.layer;
		for (int i = 0; i < count; i++)
		{
			fieldnode.transform.GetChild(0).parent = new_tile.transform;
		}
		Destroy (fieldnode);
	}
}

