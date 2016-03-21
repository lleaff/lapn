using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	
	public Text carrot;
	public Text time;
	public Text money;
	public Light dayint;
	private float m_time;
	private int sec;
	private float day;

	void Start () {
		m_time = 0;
		sec = 0;
		
	}
		
	void Update () {
		m_time += Time.deltaTime;
		if (m_time >= 1) {
			m_time = 0;
			sec += 1;
		}
		carrot.text = Globals.i.Carrots.ToString ();
		if (sec / 60 < 10)
			time.text = "0" + (sec / 60).ToString ();
		else
			time.text = (sec / 60).ToString ();
		time.text += ":";
		if (sec % 60 < 10)
			time.text += "0" + (sec % 60).ToString ();
		else
			time .text+= (sec % 60).ToString ();
		if ((sec/60)%24 < 12)
			day = 1.5F - (((sec/60)%12)/10F);
		else
			day = 0.5F + (((sec/60)%12)/10F);
		dayint.intensity = day;
		money.text = Globals.i.Money.ToString();
	}
}
