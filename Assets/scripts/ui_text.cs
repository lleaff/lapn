using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	
	public Text carrot;
	public Text time;
	public Text money;

	void Start () {
	}
		
	void Update () {
		carrot.text = globals.i.Carrots.ToString ();
		time.text = TimeManager.i.FormattedTime;
		money.text = globals.i.Money.ToString();
	}



}
