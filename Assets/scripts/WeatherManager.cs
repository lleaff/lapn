using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour {

	public static WeatherManager i = null; /* WeatherManager instance */

	int days = 0;

	public int WeatherUpdateSeconds = 3;
	public float LightTransitionTime = 2f;

	public Light LightSource;
	public Color LightColorDay;
	public Color LightColorNight;
	private Color LightColor;
	private float lightIntensity;

	public bool Raining = false;
	float RainingLightIntensityFactor = 1;
	public float heatWarmColorChangeMin = 23f;
	public float heatColdColorChangeMax = 10f;

	private float heat = 20f;
	public float Heat = 20f;
	public float DayHeatVariationScale = 10f;
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

	public GameObject NightLights;

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
		LightColor = LightColorDay;
	}

	void Start() {
		LightTransitionTime = Mathf.Min(LightTransitionTime, TimeManager.i.DaySeconds / 3);

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

	private bool wasDay = true;

	void LightUpdate() {

		float startIntensity = lightIntensity;
		lightIntensity = TimeManager.i.IsDay ?
			1.5F - (((TimeManager.i.Seconds / 60) % 12) / 10F) :
			0.8F + (((TimeManager.i.Seconds / 60) % 12) / 10F);
		if (Raining) {
			lightIntensity *= RainingLightIntensityFactor;
		}

		if (lightIntensity != startIntensity) {
			StartCoroutine (ApplyIntensityOverTime(LightSource, lightIntensity, LightTransitionTime));
		}

		//------------------------------

		Color startColor = LightColor;
		Color color = TimeManager.i.IsDay ?	LightColorDay : LightColorNight;
		color = ApplyHeatColoration (color);

		if (color != startColor) {
			LightColor = color;
			StartCoroutine (ApplyColorOverTime (LightSource, color, LightTransitionTime));
		}
			
		//------------------------------

		bool isDay = TimeManager.i.IsDay;
		if (wasDay && !isDay) {
			StartCoroutine (SwitchNightLights (NightLights));
		}
	}

	IEnumerator SwitchNightLights(GameObject obj) {
		float switchOnDelay = LightTransitionTime + LightTransitionTime * 0.5f;
		yield return new WaitForSeconds (switchOnDelay);
		obj.SetActive (true);
		yield return new WaitForSeconds ((TimeManager.i.NightDuration - switchOnDelay) * 0.5f);
		obj.SetActive (false);
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

	private float timeStep = 0.15f;

	IEnumerator ApplyColorOverTime(Light light, Color color, float time) {
		int steps = (int)(time / timeStep);
		Color startColor = light.color;
		for (int step = 1; step < steps; step++) {
			float scale = (float)step / (float)steps;
			light.color = Color.Lerp (startColor, color, scale);
			yield return new WaitForSeconds (timeStep);
		}
		light.color = color;
	}

	IEnumerator ApplyIntensityOverTime(Light light, float intensity, float time) {
		int steps = (int)(time / timeStep);
		float startIntensity = light.intensity;
		for (int step = 1; step < steps; step++) {
			float scale = (float)step / (float)steps;
			light.intensity = Mathf.Lerp (startIntensity, intensity, scale);
			yield return new WaitForSeconds (timeStep);
		}
		light.intensity = intensity;
	}
}
