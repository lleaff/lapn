using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

	public static void Fill<T>(this T[] array, T fillValue) {
		for (int i = array.Length - 1; i >= 0; i--) {
			array [i] = fillValue;
		}
	}

	public static bool BeginsWith(this string str, string substr) {
		return (str.Substring (0, substr.Length) == substr);
	}


	public static void DeactivateAllComponentsExcept(this GameObject obj, string[] componentNames) {
		MonoBehaviour[] components = obj.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour c in components) {
			c.enabled = false;
		}
		foreach (string cn in componentNames) {
			MonoBehaviour ci = obj.GetComponent(cn) as MonoBehaviour;
			if (ci) {
				ci.enabled = true;
			}
		}
	}

	public static float DistanceTo(this GameObject self, GameObject other) {
		float tmp;
		float distx;
		float distz;

		tmp = 0;
		distx = self.transform.position.x - other.transform.position.x;
		if (distx < 0)
			distx *= -1;
		distz = self.transform.position.z - other.transform.position.z;
		if (distz < 0)
			distz *= -1;
		tmp = distx + distz;
		return (tmp);
	}
}