using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	private GameObject globalValue;
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
		globalValue = GameObject.Find ("GlobalValue");
	}
		
	void Update () {
		m_time += Time.deltaTime;
		if (m_time >= 1) {
			m_time = 0;
			sec += 10;
		}
		carrot.text = globalValue.GetComponent<globalValue> ().Carrots.ToString ();
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
			day = 0.5F + (((sec/60)%12)/10F);
		else
			day = 1.7F - (((sec/60)%12)/10F);
		print (day);
		dayint.intensity = day;
		money.text = globalValue.GetComponent<globalValue> ().Money.ToString();
	}
}
