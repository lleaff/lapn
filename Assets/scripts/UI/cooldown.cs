using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cooldown : MonoBehaviour {

	private string[] words = new string[3];
	private int time;
	public Text go_time;
	public GameObject coolDown;

	void Start () {
		
	}

	private static int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}

	void Update () {
		words = go_time.text.Split (' ');
		time = IntParseFast(words[1]);
		coolDown.transform.localScale = new Vector3(1, 1 - ((5 - time) * 0.2F) ,1);
	}
}
