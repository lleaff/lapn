using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	IEnumerator OnTriggerEnter(Collider other) {
		gameObject.GetComponent<Animation> ().Play ("Up Down");
		other.gameObject.GetComponent<Animation> ().Play ("death");
		yield return new WaitForSeconds (1);
		Destroy (other.gameObject);
	}
}
