using UnityEngine;
using System.Collections;

public class EatTrigger : MonoBehaviour {

	public ia_dog DogIA;

	void Awake () {
		DogIA = GetComponentInParent<ia_dog> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == globals.rabbitTag) {
			DogIA.EatableRabbitEnter (other);
		}
	}
}
