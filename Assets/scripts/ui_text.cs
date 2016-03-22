using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	
	public Text carrot;
	public Text time;
	public Text money;
	public Light dayint;
	private float lightIntensity;

	void Start () {
	}
		
	void Update () {
		carrot.text = globals.i.Carrots.ToString ();
		time.text = TimeManager.i.FormattedTime;
		money.text = globals.i.Money.ToString();



		lightIntensity = TimeManager.i.IsDay ?
			1.5F - (((TimeManager.i.Seconds/60)%12)/10F) :
			0.5F + (((TimeManager.i.Seconds/60)%12)/10F);
		dayint.intensity = lightIntensity;

	}



}
