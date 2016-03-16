﻿using UnityEngine;
using System.Collections;

public class generate_bunny : MonoBehaviour {

	private bool canGen = true;
	public GameObject rabbits;
	public GameObject[] pos;

	// Use this for initialization
	void Start () {

	}

	IEnumerator GenerateBunny()
	{
		yield return new WaitForSeconds(Random.Range(0.5f,2.5f));
		int selectPos = Random.Range (0, pos.Length);
		Instantiate (rabbits, pos[selectPos].transform.position, rabbits.transform.localRotation);
		canGen = true;
	}

	void Update () {
		if (canGen) {
			canGen = false;
			StartCoroutine (GenerateBunny ());
		}
	}
}
