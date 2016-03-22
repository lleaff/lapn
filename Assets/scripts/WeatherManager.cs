using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour {

	public static WeatherManager i = null; /* WeatherManager instance */

	public float Heat = 20;
	public float Humidity = 1; /* 1 == neutral */

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
}
