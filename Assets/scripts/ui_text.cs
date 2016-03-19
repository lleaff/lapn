using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	private GameObject globalValue;
	public Text carrot;
	public Text time;
	public Text money;

	void Start () {
		globalValue = GameObject.Find ("GlobalValue");
	}
		
	void Update () {
		carrot.text = "Bonjour";
	}
}
