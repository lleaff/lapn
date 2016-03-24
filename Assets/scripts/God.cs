using UnityEngine;
using System.Collections;

public class God : MonoBehaviour {

	public static God i;

	public GameObject BloodSplatterObject;
	public int BloodPersistenceTime = 5;

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy (gameObject);
		}
	}

	public bool Kill(GameObject ent) {
		if (ent.CompareTag (globals.rabbitTag)) {
			return KillRabbit (ent);
		} else if (ent.CompareTag (globals.dogTag)) {
			return KillDog (ent);
		}
		return false;
	}

	public bool KillRabbit(GameObject rabbit, bool blood = false) {
		if (rabbit == null)
			return false;
		rabbit.GetComponent<ia> ().enabled = false;
		rabbit.GetComponent<Collider> ().enabled = false;
		rabbit.GetComponent<Animation> ().Play ("death");
		rabbit.GetComponent<NavMeshAgent> ().ResetPath ();
		StartCoroutine(WaitAndDestroyObj (rabbit, 1));
		StartCoroutine(WaitAndSpawnBlood (rabbit.transform.position, 1));
		return true;
	}

	public bool KillDog(GameObject dog) {
		if (dog == null)
			return false;
		dog.GetComponent<ia_dog> ().enabled = false;
		dog.GetComponent<Collider> ().enabled = false;
		dog.GetComponent<Animation> ().Play ("death");
		dog.GetComponent<NavMeshAgent> ().ResetPath ();
		StartCoroutine(WaitAndDestroyObj (dog, 1));
		return true;
	}

	public IEnumerator WaitAndDestroyObj (GameObject obj, int time = 1) {
		yield return new WaitForSeconds (time);
		Destroy (obj);
	}

	public IEnumerator WaitAndSpawnBlood (Vector3 position, int time = 1) {
		yield return new WaitForSeconds (time);
		SpawnBlood (position);
	}

	void SpawnBlood(Vector3 position, int disappearTime = 3) {
		Vector3 bloodPosition = position;
		bloodPosition.y = 0.1f;
		GameObject blood = Instantiate (BloodSplatterObject, bloodPosition, Quaternion.identity) as GameObject;
		StartCoroutine (WaitAndDestroyObj (blood, BloodPersistenceTime));
	}
}
