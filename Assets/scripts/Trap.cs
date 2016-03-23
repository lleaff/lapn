using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	private int time;
	private int old;


	void Awake () {
		old = TimeManager.i.Seconds;
	}

	void Update() {
		time = TimeManager.i.Seconds;
		/*print (old);
		print (time);
		print (time - old);
		print ("-------------");*/
	}
	IEnumerator OnTriggerEnter(Collider other) {
		if ((time - old) >= 10) {
			gameObject.GetComponent<Animation> ().Play ("Up Down");
			other.gameObject.GetComponent<Animation> ().Play ("death");
			other.gameObject.GetComponent<ia> ().enabled = false;
			other.gameObject.GetComponent<NavMeshAgent> ().ResetPath ();
			yield return new WaitForSeconds (1);
			if (other != null)
				Destroy (other.gameObject);
			old = TimeManager.i.Seconds;
		}
	}
}
