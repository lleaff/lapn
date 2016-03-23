using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonesManager : MonoBehaviour {

	public static BonesManager i = null; /* Bones manager instance */

	public List<GameObject> Bones;

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		UpdateBones ();
	}

	public void UpdateBones() {
		Bones = Utils.ToList<GameObject>(GameObject.FindGameObjectsWithTag ("Bone"));
	}

	public void Add(GameObject bone) {
		Bones.Add (bone);
	}
	public void Remove(GameObject bone) {
		Bones.Remove (bone);
	}
}
