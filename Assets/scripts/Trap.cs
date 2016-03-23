using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	private int time;
	private int old;
	private static int killed = 0;

	public int cooldown = 10;
	public int max = 5;
	public Material blood;
	public Material clean;

	void Awake () {
		old = TimeManager.i.Seconds;
	}

	void Update() {
		time = TimeManager.i.Seconds;
		if ((time - old) >= cooldown) {
			time = cooldown + old;
			gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = clean;
		}
	}

	IEnumerator OnTriggerEnter(Collider other) {
		if ((time - old) >= cooldown) {
			killed++;
			gameObject.GetComponent<Animation> ().Play ("Up Down");
			old = TimeManager.i.Seconds;
			gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = blood;
			other.gameObject.GetComponent<Animation> ().Play ("death");
			other.gameObject.GetComponent<ia> ().enabled = false;
			other.gameObject.GetComponent<NavMeshAgent> ().ResetPath ();
			yield return new WaitForSeconds (1);
			if (other != null)
				Destroy (other.gameObject);
			if (killed == max)
				Destroy (gameObject);
		}
	}
}
