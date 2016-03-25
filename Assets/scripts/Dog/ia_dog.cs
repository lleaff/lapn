using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Bone = UnityEngine.GameObject;
using Rabbit = UnityEngine.GameObject;

public class ia_dog : MonoBehaviour
{
	NavMeshAgent agent;
	Animation anim;
	GameObject destination;

	// =Config
	//------------------------------------------------------------

	public float BoneReachedDistance = 1.5f;
	public int RabbitSpotPersistenceTimeSeconds = 3;

	public float ChasingSpeed = 5f;
	public float ChasingAcceleration = 15f;
	public float SpawnedSpeed = 3.5f;
	public float IdlingSpeed = 0.8f;
	public float IdlingAcceleration = 5f;

	public float DetectionRadius = 3.15f;

	public float RestingTime = 6f;

	//------------------------------------------------------------

	//needed for AtEndOfPath
	public float pathEndThreshold = 0.1f;
	private bool hasPath = false;

	private List<Rabbit> spottedRabbits;
	private Rabbit TargetRabbit = null;

	private Bone TargetBone = null;
	private List<Bone> Bones {
		get { return BonesManager.i.Bones; }
	}
	private List<Bone> VisitedBones;
	private Utils.DistanceComparer distComparer;

	private NavMeshPath path;

	//------------------------------------------------------------

	public enum DState { Idle, Spawned, Boning, Chasing, Resting };
	public DState state = DState.Spawned;

	//------------------------------------------------------------

	void Awake()
	{
		distComparer = new Utils.DistanceComparer (gameObject);
		VisitedBones = new List<GameObject> ();
		agent = GetComponent<NavMeshAgent> ();
	}

	void Start ()
	{
		GetComponent<SphereCollider> ().radius = DetectionRadius;

		agent.autoBraking = true; /* Don't slow down when approaching destination */
		agent.speed = IdlingSpeed;
		agent.acceleration = IdlingAcceleration;


		anim = GetComponent<Animation> ();
		spottedRabbits = new List<GameObject>();

		GotoNextBone ();
		Spawned ();
	}

	void Update ()
	{
		if (spottedRabbits.Count != 0) {
			Chasing ();
		}

		switch (state) {
		case DState.Boning:
			BoningUpdate ();
			break;
		case DState.Idle:
			IdleUpdate ();
			break;
		case DState.Resting:
			RestingUpdate ();
			break;
		case DState.Chasing:
			ChasingUpdate ();
			break;
		case DState.Spawned:
			SpawnedUpdate ();
			break;
		}
	}

	void LateUpdate() {
		CleanRabbits ();

		switch (state) {
		case DState.Chasing:
			ChasingLate ();
			break;
		}
	}

	//------------------------------------------------------------

	void Idle() {
		anim.Play ("idle_04");
		agent.speed = IdlingSpeed;
		agent.acceleration = IdlingAcceleration;
		state = DState.Idle;
	}
	void IdleUpdate() {
		if (Bones.Count <= 1) {
			if (IsOnBone ()) {
				return;
			}
		}
		GotoNextBone ();
	}

	void Spawned() {
		agent.speed = SpawnedSpeed;
		if (Bones.Count >= 1) {
			GoToBone (Bones [0]);
		}
	}
	void SpawnedUpdate() {
		if (agent.remainingDistance < BoneReachedDistance) {
			VisitedBones.AddUnique (TargetBone);
			Idle ();
		}
	}

	void Boning() {
		anim.Play ("run");
		agent.speed = IdlingSpeed;
		agent.acceleration = IdlingAcceleration;
		state = DState.Boning;
	}
	void BoningUpdate() {
		if (agent.remainingDistance < BoneReachedDistance) {
			VisitedBones.AddUnique (TargetBone);
			GotoNextBone ();
		}
	}

	void Chasing() {
		anim.Play ("run_fast");
		agent.speed = ChasingSpeed;
		agent.acceleration = ChasingAcceleration;
		state = DState.Chasing;
	}
	void ChasingUpdate() {
		if (spottedRabbits.Count == 0) {
			Idle ();
		}
		if (NoTargetRabbit ()) {
			foreach (Rabbit rabbit in spottedRabbits) {
				if (CanReach (rabbit)) {
					TargetRabbit = rabbit;
					break;
				}
			}
		} else {
			ChaseRabbit (TargetRabbit);
		}
	}
	void ChasingLate() {
	}

	void Resting() {
		state = DState.Resting;
		if (!IsOnBone ()) {
			GoToBone ();
		}
		StartCoroutine (SetToIdleAfter(RestingTime));
	}

	void RestingUpdate() {
		if (IsOnBone ()) {
			anim.Play ("idle_05");
		}
	}

	//------------------------------------------------------------

	void GotoNextBone() {
		if (Bones.Count == 0) {
			Idle ();
			return;
		} else if (Bones.Count == 1 && IsOnBone ()) {
			Idle ();
			agent.ResetPath ();
			return;
		}
		Boning ();
		List<Bone> bones = GetSortedByDistance (Bones);
		bones.RemoveAll (b => !CanReach(b));
		if (bones.Count == 0) {
			Idle ();
			return;
		}
		Bone bone = GetNextBone (bones);
		GoToBone (bone);
	}

	bool GoToBone() {
		return GoToBone (GetNextBone (GetSortedByDistance (Bones)));
	}
	bool GoToBone(Bone bone) {
		TargetBone = bone;
		agent.SetDestination(bone.transform.position);
		return true;
	}
	
	Bone GetNextBone(List<Bone> bones) {
		if (bones.Count == 1) {
			return bones[0];
		}
		Bone bone = NearestNotVisitedBone (bones);
		if (bone == null) {
			Bone PreviousBone = VisitedBones.Last ();
			VisitedBones = new List<Bone>();
			VisitedBones.AddUnique (PreviousBone);
			bone = NearestNotVisitedBone(bones);
		}
		return bone;
	}

	Bone NearestNotVisitedBone(List<Bone> bones) {
		Bone bone = null;
		foreach (Bone b in bones) {
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
			spottedRabbits.AddUnique(other.gameObject);
			if (spottedRabbits.Count == 1) {
				Chasing ();
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag (globals.rabbitTag)) {
			StartCoroutine (ForgetRabbit(other.gameObject));
		}
	}

	public void EatableRabbitEnter(Collider other) {
		// Eat rabbit
		EatRabbit (other.gameObject);
	}

	bool EatRabbit(Rabbit rabbit) {
		StartCoroutine (_EatRabbit (rabbit));
		Resting ();
		return true;
	}
	IEnumerator _EatRabbit(Rabbit rabbit) {
		anim.Play ("attack_05");
		yield return new WaitForSeconds (1.11f);
		God.i.KillRabbit (rabbit, true);
		spottedRabbits.Remove (rabbit);
	}

	//------------------------------------------------------------

	void OnCollisionEnter(Collision col) {
		switch (col.collider.tag) {
		case globals.rabbitTag:
			spottedRabbits.AddUnique(col.gameObject);
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

	bool RabbitIsValidFood(Rabbit rabbit) {
		return (!Utils.isDestroyed (rabbit) && !rabbit.GetComponent<ia>().Eating);
	}

	void CleanRabbits() {
		spottedRabbits.RemoveAll (r => !RabbitIsValidFood(r));
	}

	IEnumerator ForgetRabbit(Rabbit rabbit) {
		yield return new WaitForSeconds (RabbitSpotPersistenceTimeSeconds);
		UnspotRabbit (rabbit);
	}
	void UnspotRabbit(Rabbit rabbit) {
		spottedRabbits.Remove (rabbit);
	}

	bool ChaseRabbit(Rabbit rabbit) {
		agent.SetDestination (rabbit.transform.position);
		TargetRabbit = rabbit;
		return true;
	}

	bool NoTargetRabbit() {
		return !(RabbitIsValidFood (TargetRabbit) && CanReach (TargetRabbit));
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
		return CanReach (bone);
	}
	bool CanReach(GameObject obj) {
		NavMeshPath path = new NavMeshPath();
		if (obj == null || !agent.CalculatePath (obj.transform.position, path))
			return false;
		return path.status == NavMeshPathStatus.PathComplete;
	}

	//------------------------------------------------------------

	List<GameObject> GetSortedByDistance (List<GameObject> objects) {
		List<GameObject> copied = new List<GameObject> (objects);
		copied.Sort (distComparer);
		return copied;
	}

	//------------------------------------------------------------

	IEnumerator SetToIdleAfter(float time) {
		yield return new WaitForSeconds (time);
		Idle ();
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




	public static bool IsDogBody(Collider col) {
		if (!col.CompareTag (globals.dogTag))
			return false;
		if (col.isTrigger)
			return false;
		return true;
	}
}
