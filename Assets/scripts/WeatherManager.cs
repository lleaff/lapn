using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour {

	public static WeatherManager i = null; /* WeatherManager instance */

	int days = 0;

	private float heat = 20f;
	public float Heat;
	public float DayHeatVariationScale = 1f;
	public float HeatMin = -10f;
	public float HeatMax = 35f;
	float HeatGoal;
	public int HeatGoalDayscale = 30;
	float HeatDailyGoalIncrease;

	private float humidity = 1f; /* 1 == neutral */
	public float Humidity;
	public float DayHumidityVariationScale = 0.5f;
	public float HumidityMin = 0f;
	public float HumidityMax = 5f;
	float HumidityGoal;
	public int HumidityGoalDayscale = 10;
	float HumidityDailyGoalIncrease;


	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		heat = Heat;
		Humidity = Humidity;
		HeatGoal = Heat;
		HumidityGoal = Humidity;
	}

	void SetGoals() {
		if (days % HumidityGoalDayscale == 0) {
			HumidityGoal = Random.Range (HumidityMin, HumidityMax);
			HumidityDailyGoalIncrease = (HumidityGoal - humidity) / HumidityGoalDayscale;
		}
		if (days % HeatGoalDayscale == 0) {
			HeatGoal = Random.Range(HeatMin, HeatMax);
			HeatDailyGoalIncrease = (HeatGoal - heat) / HeatGoalDayscale;
		}
	}

	public void NewDay() {
		days++;
		SetGoals ();
		Heat = heat + HeatDailyGoalIncrease;
		Humidity = humidity + HumidityDailyGoalIncrease;
		Heat += Random.Range(-DayHeatVariationScale / 2f, DayHeatVariationScale / 2f);
		Humidity += Random.Range(-DayHumidityVariationScale / 2f, DayHumidityVariationScale / 2f);
	}
}
