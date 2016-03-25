using UnityEngine;
using System.Collections;

public class generate_bunny : MonoBehaviour {

	private bool canGen = true;

	private int start_time = 15;
	private int spawn_delay = 0; 
	private int nb_rabbit = 0;
	private int nb_trap = 0;
	private float time = 0;
	private int dog = 0;

	public GameObject rabbits;
	public GameObject[] pos;
	public int minSec;
	public int maxSec;

	private int get_nbr_trap()
	{
		int i = 0;
		foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			if (go.name == "trap")
				i++;
		}
		return (i);
	}

	IEnumerator GenerateBunny()
	{
		if (spawn_delay - (nb_rabbit * 0.5F + nb_trap * 0.5F + time * 0.01F + dog * 5F) < 5)/*If time is smaller than 5s*/
			yield return new WaitForSeconds(5);
		else
			yield return new WaitForSeconds(spawn_delay - (nb_rabbit * 0.5F + nb_trap * 0.5F + time * 0.01F + dog * 5F));
		int selectPos = Random.Range (0, pos.Length);
		Instantiate (rabbits, pos[selectPos].transform.position, rabbits.transform.localRotation);
		canGen = true;
	}

	// Monobehaviour
	//------------------------------------------------------------

	void Update () {
		if (globals.i.Family[0] > 0)  /*Father's bonus: if he is alive, bunny spawn 10s later*/
			spawn_delay = start_time + 10;
		else
			spawn_delay = start_time;

		if (GameObject.Find ("Dog"))
			dog = 1;
		else
			dog = 0;
		
		nb_rabbit = GameObject.FindGameObjectsWithTag ("Bunny").Length;
		nb_trap = get_nbr_trap ();
		time = TimeManager.i.Seconds;
		if (canGen && nb_rabbit < 30) {
			canGen = false;
			StartCoroutine (GenerateBunny ());
		}
	}
}
