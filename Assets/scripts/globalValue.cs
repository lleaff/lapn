using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class globalValue : MonoBehaviour {

	private int carrots = 10;
	private int money = 0;
	private List<int>list_value	= new  List<int>();
	private bool canAdd = true;

	public int get_carrots()
	{
		return (carrots);
	}

	public int get_money()
	{
		return (money);
	}

	public List<int> get_list()
	{
		return (list_value);
	}

	public void set_carrots(int n)
	{
		carrots = n;
	}

	public void set_money(int n)
	{
		money = n;
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
		print (money);
	}

	public void remove_carrots(int n)
	{
		if (n > 0 && n <= carrots)
			carrots -= n;
	}

	private IEnumerator add_value()
	{
		yield return new WaitForSeconds (120);
		list_value.Add (Random.Range (10, 50));
		canAdd = true;
	}

	// Use this for initialization
	void Start () {
		list_value.Add (Random.Range (10, 50));
		list_value.Add (Random.Range (10, 50));
		list_value.Add (Random.Range (10, 50));
		list_value.Add (42);
		print (list_value[list_value.Count - 1]);
	}
	
	// Update is called once per frame
	void Update () {
		if (canAdd) {
			canAdd = false;
			StartCoroutine (add_value ());
		}
	}
}
