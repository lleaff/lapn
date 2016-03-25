using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	public Text carrot;
	public Text time;
	public Text money;
	public Text temp;
	public Text humidity;

	void Update () {
		carrot.text = globals.i.Carrots.ToString ();
		time.text = TimeManager.i.FormattedTime;
		money.text = globals.i.Money.ToString();
		temp.text = WeatherManager.i.Heat.ToString("0.0") + "˚C";
		humidity.text = (WeatherManager.i.Humidity * 100).ToString ("0");
	}
}
