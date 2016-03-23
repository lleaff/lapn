	using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	private int time;
	private int old;
	private int killed = 0;

	public int cooldown = 10;
	public int maxUses = 5;
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
		if (!other.CompareTag (globals.rabbitTag) && !other.CompareTag(globals.dogTag)) {
			return null; /* Kill only rabbits and dogs */
		}
		if ((time - old) >= cooldown) {
			old = TimeManager.i.Seconds;
			killed++;
			gameObject.GetComponent<Animation> ().Play ("Up Down");
			gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = blood;

			God.i.Kill(other.gameObject);

			if (killed == maxUses)
				Destroy (gameObject);
		}
		return null;
	}
}
