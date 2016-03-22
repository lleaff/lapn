using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public static TimeManager i = null; /* Time manager instance */

	private int seconds = 0;
	public int Seconds {
		get { return seconds; }
	}
	private bool isDay;
	public bool IsDay {
		get { return isDay; }
	}
	private float m_time = 0;


	void Awake () {
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy(gameObject);
		}
	}


	void Update () {
		CalcSeconds ();

		isDay = (seconds / 60) % 24 < 12;
	}


	//------------------------------------------------------------

	void CalcSeconds() {
		m_time += Time.deltaTime;
		int whole = (int)System.Math.Floor (m_time);
		m_time -= whole;
		seconds += whole;
	}


	public string FormatTime(int sec) {
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

	public string FormattedTime {
		get {
			return FormatTime(seconds);
		}
	}
}
