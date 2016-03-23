using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	private int time;
	private int old;

	public int cooldown = 10;

	void Awake () {
		old = TimeManager.i.Seconds;
	}

	void Update() {
		time = TimeManager.i.Seconds;
		if ((time - old) >= cooldown)
			time = cooldown + old;
	}

	IEnumerator OnTriggerEnter(Collider other) {
		if ((time - old) >= cooldown) {
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
