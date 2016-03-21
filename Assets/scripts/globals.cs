﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class globals : MonoBehaviour {

	public static globals i = null; /* globals instance */

	private int carrots = 10;
	private int money = 0;
	private List<int>list_value	= new  List<int>();
	private bool canAdd = true;
	private bool canLoseLife = true;
	private int[] family;
	private int button_click;

	public int Money {
		get {
			return (money);
		}
		set {
			money = value;
		}
	}

	public int Carrots {
		get {
			return (carrots);
		}
		set {
			carrots = value;
		}
	}

	public List<int> List {
		get {
			return (list_value);
		}
	}

	public int Button{
		get {
			return (button_click);
		}
		set {
			button_click = value;
		}
	}

	public int get_life(int n)
	{
		return (family [n]);
	}
		
	public void add_carrots(int n)
	{
		if (n > 0)
			carrots += n;
	}

	public void add_money(int n)
	{
		if (n > 0)
			money += n;
	}

	public void add_life(int n, int i)
	{
		if (n > 0)
			family [i] += n;
		if (family [i] > 100)
			family [i] = 100;
	}

	public void remove_carrots(int n)
	{
		if (n > 0 && n <= carrots)
			carrots -= n;
	}

	public void remove_life(int n, int i)
	{
		if (n > 0)
			family [i] -= n;
		if (family [i] < 0)
			family [i] = 0;
	}

	private IEnumerator add_value()
	{
		yield return new WaitForSeconds (120);
		if (list_value[list_value.Count - 1] - 10 > 0)
			list_value.Add (Random.Range (list_value[list_value.Count - 1] - 10, list_value[list_value.Count - 1] + 10));
		else
			list_value.Add (Random.Range (5, 20));
		canAdd = true;
	}

	private IEnumerator lose_life()
	{
		yield return new WaitForSeconds (6);
		for (int i = 0; i < 4; i++)
			remove_life (1, i);
		canLoseLife = true;
	}

	// Monobehaviour
	//------------------------------------------------------------

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		list_value.Add (Random.Range (10, 60));
		for (int j = 0; j < 20; j++) {
			if (list_value[list_value.Count - 1] - 10 > 0)
				list_value.Add (Random.Range (list_value[list_value.Count - 1] - 10, list_value[list_value.Count - 1] + 10));
			else
				list_value.Add (Random.Range (5, 20));	
		}
		family = new int[4];
		family.Fill (100);
	}

	void Update () {
		if (canAdd) {
			canAdd = false;
			StartCoroutine (add_value ());
		}
		if (canLoseLife) {
			canLoseLife = false;
			StartCoroutine (lose_life ());
		}
	}
}