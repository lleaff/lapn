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
	public bool Eating {
		get { return eat; }
	}
	public GameObject gridnode;
	//used for destroying fences
	public bool aggressive = false;
	public bool destroying = false;
	//needed for Atendofpath
	public float pathEndThreshold = 0.1f;
	private bool hasPath = false;
	//
	//mesh array for fences
	public Mesh[] meshs;

	private GameObject[] carrots;
	private GameObject[] unattainable;
	public int p_max_hidden_carrots;
	private int max_hidden_carrots;

	void Awake ()
	{
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animation> ();
		for (int i = 0; i < GameObject.Find ("Rabbit generator").transform.childCount; i++)
			spawn_positions [i] = GameObject.Find ("Rabbit generator").transform.GetChild (i).gameObject;
	}

	void Start ()
	{
		agent.speed = globals.i.RabbitSpeedRegular;
		max_hidden_carrots = p_max_hidden_carrots;
	}	

	void Update ()
	{
		NavMeshPath path = new NavMeshPath();
		carrots = GameObject.FindGameObjectsWithTag ("Carrot");
		unattainable = GameObject.FindGameObjectsWithTag ("unattainable");

		/*************BONUS*************/
		//Daughter's && son's bonus: max_hidden_carrot += 1 if she is alive
		if (globals.i.Family [3] > 0 && globals.i.Family [2] > 0)
			max_hidden_carrots = p_max_hidden_carrots + 3;
		else if (globals.i.Family [2] > 0 || globals.i.Family [3] > 0)
			max_hidden_carrots = p_max_hidden_carrots + 1;
		else
			max_hidden_carrots = p_max_hidden_carrots;
		/*****************************/

		//Check if unattainable carrots became attainable again (if other bunnys have destroyed fences around them without eating them)
		foreach(GameObject crt in unattainable)
		{
			agent.CalculatePath (crt.transform.position, path);
			if (path.status == NavMeshPathStatus.PathComplete) {
				crt.transform.tag = "Carrot";
			}
		}
			
		if (AtEndOfPath () && aggressive && carrots.Length == 0 && unattainable.Length == 0 && !destroying) {
			bunny_retreat ();
		}

		//if there are carrots and bunny isnt eating or destroying a fence look for a carrot to eat
		if (carrots.Length != 0 && !eat && !destroying) {
			eat_nearest_carrot ();
		} //else if there's no carrots but there's more than 2 hidden carrots, get aggressive to destroy fences
		else if (unattainable.Length >= max_hidden_carrots && carrots.Length == 0 && !aggressive) {
			focus_unattainable ();
		}

		//retreat if there's nothing to eat for rabbits || carrot has been eaten or destroyed 
		if (carrots.Length == 0 && unattainable.Length < max_hidden_carrots && !eat && retreat == false && !aggressive) {
			bunny_retreat ();
		} 
		//See if destination carrot is attainable and set it unattainable if not so
		if (destination != null && !destination.transform.CompareTag ("eaten") && !aggressive){
			agent.CalculatePath (destination.transform.position, path);
			if (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid) {
				agent.ResetPath ();
				destination.transform.tag = "unattainable";
				carrots = GameObject.FindGameObjectsWithTag ("Carrot");
				if (carrots.Length == 0 && unattainable.Length < max_hidden_carrots && !aggressive) {
					bunny_retreat ();
				} else if (carrots.Length > 0) {
					eat_nearest_carrot ();
				} else {
					focus_unattainable ();
				}
			}
		}
	}

	void focus_unattainable()
	{
		print ("Bunny got aggressive");
		print (max_hidden_carrots);
		aggressive = true;
		anim.Play ("hop");
		if (unattainable.Length > 0) {
			destination = get_nearest (unattainable);
			agent.SetDestination (destination.transform.position);
		}
	}

	void eat_nearest_carrot()
	{
		if (carrots.Length > 0) {
			destination = get_nearest (carrots);
			agent.SetDestination (destination.transform.position);
		}
		anim.Play ("hop");
		retreat = false;
	}

	void bunny_retreat()
	{
		anim.Play ("hop");
		agent.ResetPath ();
		GameObject ret_dest = get_nearest (spawn_positions);
		agent.SetDestination (ret_dest.transform.position);
		retreat = true; 
	}

	bool AtEndOfPath()
	{
		hasPath |= agent.hasPath;
		if (hasPath && agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold )
		{
			// Arrived
			hasPath = false;
			return true;
		}

		return false;
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

	GameObject get_nearest(GameObject[] objects)
	{
		float distance = get_distance(objects[0]);
		float tmp;
		GameObject nearest = objects[0];

		foreach (GameObject obj in objects) {
			tmp = get_distance(obj);
			if (tmp < distance) {
				distance = tmp;
				nearest = obj;
			}
		}
		return (nearest);
	}
		

	IEnumerator OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.name == "field" && col.collider.gameObject.CompareTag("Carrot"))
		{
			aggressive = false;
			col.collider.gameObject.GetComponent <ia_carrots> ().Eaten ();
			agent.ResetPath();	
			eat = true;
			anim.Play ("idle1");
			StartCoroutine(removeCarrots(col.collider.gameObject));
			GameObject parent = col.collider.transform.parent.gameObject;
			yield return new WaitForSeconds(6);
			Destroy(col.collider.gameObject);
			removefield (parent);
			Destroy (this.gameObject);
			eat = false;
		}
		else if (aggressive && col.collider.gameObject.CompareTag ("noedit")) {
			destroying = true;
			col.collider.gameObject.tag = "noedit_destroy";
			agent.ResetPath ();
			anim.Play ("idle2");
			StartCoroutine(destroy_fence(col.collider.gameObject));
			yield return new WaitForSeconds (3);
			Destroy(col.collider.gameObject);
			destroying = false;
			aggressive = false;
			retreat = false;
		}
		if (col.collider.gameObject.name == "field" && col.collider.gameObject.CompareTag ("unattainable")) {
			col.collider.gameObject.tag = "Carrot";
		}
	}

	IEnumerator destroy_fence (GameObject	fence)
	{
		
		for (int i = 0; i < 3; i++) {
			if (fence != null) {
				yield return new WaitForSeconds (0.5f);
				fence.GetComponent<MeshFilter> ().mesh = meshs [i];
			}
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

	/*GameObject get_next_nearest(GameObject[] tab, GameObject current)
	{
		float tmp_dist;
		GameObject next;
		float next_dist = 2000;

		next = null;
		foreach (GameObject obj in tab) {
			tmp_dist = get_distance (obj);
			if (tmp_dist < next_dist && obj.GetInstanceID() != current.GetInstanceID()) {
				next_dist = tmp_dist;
				next = obj;
			}
		}
		if (next == null) {
			next = new GameObject ();
			next.transform.position = Vector3.zero;
		}
		return (next);
	}*/
}

