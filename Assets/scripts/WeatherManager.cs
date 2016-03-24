using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour {

	public static WeatherManager i = null; /* WeatherManager instance */

	int days = 0;

	public int WeatherUpdateSeconds = 3;

	public Light dayint;
	public Color LightColorDay;
	public Color LightColorNight;
	private float lightIntensity;

	public bool Raining = false;
	float RainingLightIntensityFactor = 1;
	public float heatWarmColorChangeMin = 23f;
	public float heatColdColorChangeMax = 10f;

	private float heat = 20f;
	public float Heat = 20f;
	public float DayHeatVariationScale = 1f;
	public float HeatMin = -10f;
	public float HeatMax = 35f;
	float HeatGoal;
	public int HeatGoalDayscale = 30;
	float HeatDailyGoalIncrease;

	private float humidity = 1f; /* 1 == neutral */
	public float Humidity = 20f;
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

		Heat = heat;
		Humidity = humidity;
		HeatGoal = Heat;
		HumidityGoal = Humidity;

		LightColorDay   = new Color (255f / 255f, 203f / 255f, 176f / 255f);
		LightColorNight = new Color (176f / 255f, 203f / 255f, 255f / 255f);
	}

	void Start() {
		StartCoroutine (WeatherUpdate());
	}

	//----------------------------------------------------------------------

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
		heat = heat + HeatDailyGoalIncrease;
		humidity = humidity + HumidityDailyGoalIncrease;
		Heat =  heat + Random.Range(-DayHeatVariationScale / 2f, DayHeatVariationScale / 2f);
		Humidity = humidity + Random.Range(-DayHumidityVariationScale / 2f, DayHumidityVariationScale / 2f);
	}


	IEnumerator WeatherUpdate() {
		while (true) {
			LightUpdate ();
			yield return new WaitForSeconds (WeatherUpdateSeconds);
		}
	}

	void LightUpdate() {
		lightIntensity = TimeManager.i.IsDay ?
			1.5F - (((TimeManager.i.Seconds / 60) % 12) / 10F) :
			0.3F + (((TimeManager.i.Seconds / 60) % 12) / 10F);
		if (Raining) {
			lightIntensity *= RainingLightIntensityFactor;
		}
		dayint.intensity = lightIntensity;

		Color color = TimeManager.i.IsDay ?	LightColorDay : LightColorNight;
		color = ApplyHeatColoration (color);
		dayint.color = color;
	}

	Color ApplyHeatColoration(Color color) {
		if (Heat > heatWarmColorChangeMin) {
			float heatAnomalyIntensity = ((Heat - heatWarmColorChangeMin) / HeatMax);
			float colorRedScale = 1f + heatAnomalyIntensity;
			float colorGreenScale = 1f + heatAnomalyIntensity * 0.7f;
			float colorBlueScale = 1f - (heatAnomalyIntensity * 0.2f);
			color.r *= colorRedScale;
			color.g *= colorGreenScale;
			color.b *= colorBlueScale;
		} else if (Heat < heatColdColorChangeMax) {
			float heatAnomalyIntensity = -((heatColdColorChangeMax - Heat) / HeatMin);
			float colorRedScale = 1f - (heatAnomalyIntensity * 0.2f);
			float colorGreenScale = 1f + heatAnomalyIntensity * 0.3f;
			float colorBlueScale = 1f + heatAnomalyIntensity * 1.2f;
			color.r *= colorRedScale;
			color.g *= colorGreenScale;
			color.b *= colorBlueScale;
		}
		return color;
	}
}
