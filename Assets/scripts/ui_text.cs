using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	private GameObject globalValue;
	public Text carrot;
	public Text time;
	public Text money;
	private float m_time;
	private int sec;

	void Start () {
		m_time = 0;
		sec = 0;
		globalValue = GameObject.Find ("GlobalValue");
	}
		
	void Update () {
		m_time += Time.deltaTime;
		if (m_time >= 1) {
			m_time = 0;
			sec += 1;
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

		money.text = globalValue.GetComponent<globalValue> ().Money.ToString();
	}
}
