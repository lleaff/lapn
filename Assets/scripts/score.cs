using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class score : MonoBehaviour {

	public static bool Lost = false;
	private GameObject score_board;
	private int carrots;
	private int money;
	private GameObject[] carrot;
	private GameObject[] unattainable;
	private GameObject[] seeds;

	// Use this for initialization
	void Start () {
		carrots = globals.i.Carrots;
		money = globals.i.Money;
		StartCoroutine (Slow_Update (1));
	}

	bool carrots_planted()
	{
		carrot = GameObject.FindGameObjectsWithTag ("Carrot");
		unattainable  = GameObject.FindGameObjectsWithTag ("unattainable");
		seeds = GameObject.FindGameObjectsWithTag ("seed");
		if (carrot.Length > 0 || unattainable.Length > 0 || seeds.Length > 0)
			return true;
		return false;
	}

	bool family_alive()
	{
		int i = 0;

		foreach (int hp in globals.i.Family) {
			if (hp <= 0)
				i++;
		}
		if (i == 4)
			return false;
		return true;
	}

	// Update is called once per frame
	IEnumerator Slow_Update (int time) {
		while (Lost == false) {
			carrots = globals.i.Carrots;
			money = globals.i.Money;
			yield return new WaitForSeconds (time);
			if ((money < 10 && carrots <= 0 && !carrots_planted ()) || !family_alive ()) {
				Lost = true;
				SceneManager.LoadScene("Menu");
			}
		}
	}
}
