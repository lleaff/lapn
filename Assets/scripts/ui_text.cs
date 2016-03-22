using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	
	public Text carrot;
	public Text time;
	public Text money;
	public Light dayint;
	private int sec;
	private bool day;
	private float lightIntensity;
	private float m_time = 0;

	void Start () {
		sec = 0;
		
	}
		
	void Update () {
		CalcSeconds ();

		carrot.text = globals.i.Carrots.ToString ();
		time.text = FormatTime (sec);
		money.text = globals.i.Money.ToString();


		day = (sec / 60) % 24 < 12;

		lightIntensity = day ?
			1.5F - (((sec/60)%12)/10F) :
			0.5F + (((sec/60)%12)/10F);
		dayint.intensity = lightIntensity;

	}

	void CalcSeconds() {
		m_time += Time.deltaTime;
		int whole = (int)System.Math.Floor (m_time);
		m_time -= whole;
		sec += whole;
	}


	string FormatTime(int sec) {
		string text = "";
		if (sec / 60 < 10)
			text += "0" + (sec / 60).ToString ();
		else
			text += (sec / 60).ToString ();
		text += ":";
		if (sec % 60 < 10)
			text += "0" + (sec % 60).ToString ();
		else
			text+= (sec % 60).ToString ();
		return text;
	}

}
