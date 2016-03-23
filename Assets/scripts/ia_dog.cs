using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Bone = UnityEngine.GameObject;

public class ia_dog : MonoBehaviour
{
	NavMeshAgent agent;
	Animation anim;
	GameObject destination;

	// =Config
	//------------------------------------------------------------

	public float BoneReachedDistance = 1.5f;

	//------------------------------------------------------------

	//needed for AtEndOfPath
	public float pathEndThreshold = 0.1f;
	private bool hasPath = false;

	private List<GameObject> spottedRabbits;

	private Bone TargetBone = null;
	private List<Bone> Bones {
		get { return BonesManager.i.Bones; }
	}
	private List<Bone> VisitedBones;
	private Utils.DistanceComparer distComparer;

	private NavMeshPath path;

	//------------------------------------------------------------

	public enum DState { Idle, Boning, Chasing };
	public DState state = DState.Idle;

	//------------------------------------------------------------

	void Awake()
	{
		distComparer = new Utils.DistanceComparer (gameObject);
		VisitedBones = new List<GameObject> ();
	}

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		agent.autoBraking = false; /* Don't slow down when approaching destination */


		anim = GetComponent<Animation> ();
		spottedRabbits = new List<GameObject>();

		GotoNextBone ();
	}

	void Update ()
	{
		NavMeshPath path = new NavMeshPath();


		switch (state) {
		case DState.Boning:
			Boning ();
			break;
		case DState.Idle:
			Idle ();
			break;
		case DState.Chasing:
			Chasing ();
			break;
		}


		/*
		//stop anim if at destination
		if (AtEndOfPath () && !eat) {
			agent.ResetPath ();
			anim.Play ("Take 001");
		}

		//retreat if there's nothing to eat for rabbits
		if (carrots.Length == 0 && unattainable.Length <= 3 && !eat) {
			bunny_retreat ();
		}

		//if there are carrots and bunny isnt eating or destroying a fence look for a carrot to eat
		if (carrots.Length != 0 && !eat) {
			if (!agent.hasPath) {
				destination = get_nearest (carrots);
				agent.SetDestination (destination.transform.position);
				anim.Play ("hop");
				retreat = false;
			}
		} //else if there's no carrots but there's a hidden carrot, get aggressive to destroy fences
		else if (unattainable.Length >= 3 && carrots.Length == 0) {
			destination = get_nearest (unattainable);
			anim.Play ("hop");
			agent.SetDestination (destination.transform.position);
			aggressive = true;
		}

		if ((destination == null || destination.transform.CompareTag ("eaten")) && retreat == false && !eat && !aggressive) {
			bunny_retreat ();
		} 
		else if (destination != null && !destination.transform.CompareTag ("eaten") && !aggressive){
			agent.CalculatePath (destination.transform.position, path);
			if (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid) {
				agent.ResetPath ();
				destination.transform.tag = "unattainable";
				if (unattainable.Length < 2)
					anim.Play ("idle2");
				else
					bunny_retreat ();
			}
		}*/
	}

	//------------------------------------------------------------

	void Idle() {
		if (Bones.Count <= 1) {
			if (IsOnBone ()) {
				return;
			}
		}
		GotoNextBone ();
	}

	void Boning() {
		if (agent.remainingDistance < BoneReachedDistance) {
			VisitedBones.Add (TargetBone);
			GotoNextBone ();
		}
	}

	void Chasing() {

	}

	//------------------------------------------------------------

	void GotoNextBone() {
		if (Bones.Count == 0) {
			state = DState.Idle;
			return;
		} else if (Bones.Count == 1 && IsOnBone ()) {
			state = DState.Idle;
			agent.ResetPath ();
			return;
		}
		state = DState.Boning;

		Bones.Sort (distComparer);
		GameObject bone = GetNextBone ();
		TargetBone = bone;
		agent.SetDestination(bone.transform.position);
	}
	
	Bone GetNextBone() {
		GameObject bone = NearestNotVisitedBone ();
		if (bone == null) {
			Bone PreviousBone = VisitedBones.Last ();
			VisitedBones = new List<Bone>();
			VisitedBones.Add (PreviousBone);
			bone = NearestNotVisitedBone();
		}
		return bone;
	}

	Bone NearestNotVisitedBone() {
		GameObject bone = null;
		foreach (Bone b in Bones) {
			if (!VisitedBones.Contains (b)) {
				bone = b;
				break;
			}
		}
		return bone;
	}

	//------------------------------------------------------------


	void OnTriggerEnter(Collider other) {
		if (other.CompareTag (globals.rabbitTag)) {
			spottedRabbits.Add(other.gameObject);
		}
	}

	void OnCollisionEnter(Collision col) {
		switch (col.collider.tag) {
		case globals.rabbitTag:
			spottedRabbits.Add(col.gameObject);
			break;
		case globals.boneTag:
			
			break;
		default:
			if (!CanReachBone ()) {
				TargetBone = null;
				agent.ResetPath ();
			}
			break;
		}
	}

	//------------------------------------------------------------

	bool IsOnBone() {
		foreach (Bone bone in Bones) {
			if (gameObject.DistanceTo (bone) <= BoneReachedDistance) {
				return true;
			}
		}
		return false;
	}
	bool IsOnBone(Bone bone) {
		return (gameObject.DistanceTo (bone) <= BoneReachedDistance);
	}

	bool CanReachBone() {
		return CanReachBone (TargetBone);
	}
	bool CanReachBone(Bone bone) {
		NavMeshPath path = new NavMeshPath();;
		if (bone == null || !agent.CalculatePath (bone.transform.position, path))
			return false;
		return path.status == NavMeshPathStatus.PathComplete;
	}

	//------------------------------------------------------------

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

	GameObject get_nearest(GameObject[] objects)
	{
		float distance = gameObject.DistanceTo(objects[0]);
		float tmp;
		GameObject nearest = objects[0];

		foreach (GameObject obj in objects) {
			tmp = gameObject.DistanceTo(obj);
			if (tmp < distance) {
				distance = tmp;
				nearest = obj;
			}
		}
		return (nearest);
	}
}
