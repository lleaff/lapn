	using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	
	private int killed = 0;

	public float CooldownTimeSeconds = 10;
	public int maxUses = 7;

	public Material blood;
	public Material clean;

	public bool OnCooldown = false;

	void OnTriggerEnter(Collider other) {
		if (!other.CompareTag (globals.rabbitTag) &&
			!ia_dog.IsDogBody(other)) {
			return; /* Kill only rabbits and dogs */
		}
		if (!OnCooldown) {
			killed++;
			gameObject.GetComponent<Animation> ().Play ("Up Down");
			gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = blood;

			God.i.Kill(other.gameObject);

			if (killed == maxUses)
				Destroy (gameObject);
			BeginCooldown ();
		}
	}


	IEnumerator BeginCooldown() {
		OnCooldown = true;
		yield return new WaitForSeconds (CooldownTimeSeconds);
		gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = clean;
		OnCooldown = false;
	}
}
