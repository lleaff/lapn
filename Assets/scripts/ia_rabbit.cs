using UnityEngine;
using System.Collections;

public class ia_rabbit : MonoBehaviour
{
	NavMeshAgent agent;
	Animation anim;
	GameObject destination;
	GameObject[] spawn_positions = new GameObject[5];
	public GameObject gridnode;


	public enum RState { idle, retreat, eating };
	public RState state = RState.idle;


	void ExecBehaviour() {
		switch (state) {
		case RState.idle:
			Idle ();
			break;
		case RState.retreat:
			Retreat ();
			break;
		case RState.eating:
			Eating ();
			break;
		}
	}
	

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animation> ();
		for (int i = 0; i < GameObject.Find ("Rabbit generator").transform.childCount; i++)
			spawn_positions [i] = GameObject.Find ("Rabbit generator").transform.GetChild (i).gameObject;
	}

	void Update ()
	{
		ExecBehaviour ();
	}

	//------------------------------------------------------------

	void Idle() {
		GameObject[] carrots;

		carrots = GameObject.FindGameObjectsWithTag ("Carrot");
		if (carrots.Length != 0 && state != RState.eating) {
			destination = get_nearest (carrots);
			agent.SetDestination (destination.transform.position);
			anim.Play ("hop");
			state = RState.idle;
		}

		if ((destination == null || destination.transform.CompareTag("eated")) && state != RState.retreat && state != RState.eating) {
			agent.ResetPath();
			GameObject ret_dest = get_nearest (spawn_positions);
			agent.SetDestination (ret_dest.transform.position);
			state = RState.retreat;
		}
	}

	void Eating() {

	}

	void Retreat() {

	}



	//------------------------------------------------------------

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

	IEnumerator OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.name == "field" && col.collider.gameObject.CompareTag("Carrot"))
		{
			col.collider.gameObject.tag = "eated";
			agent.ResetPath();	
			state = RState.eating;
			anim.Play ("idle1");
			StartCoroutine(removeCarrots(col.collider.gameObject));
			yield return new WaitForSeconds(6);
			state = RState.idle;
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

