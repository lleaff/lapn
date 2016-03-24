﻿using UnityEngine;
using System.Collections;

public class button_active : MonoBehaviour {

	public GameObject carrot;
	public GameObject dog;
	public GameObject trap;
	public GameObject fence;
	public GameObject Sell;

	void Update () {
		if (globals.i.Money < 10)
			carrot.SetActive (false);
		else
			carrot.SetActive (true);
		if (globals.i.Money < 20)
			fence.SetActive (false);
		else
			fence.SetActive (true);
		if (globals.i.Money < 100)
			dog.SetActive (false);
		else
			dog.SetActive (true);
		if (globals.i.Money < 110)
			dog.SetActive (false);
		if (globals.i.Carrots < 1)
			Sell.SetActive (false);
		else
			Sell.SetActive (true);
	}
}
