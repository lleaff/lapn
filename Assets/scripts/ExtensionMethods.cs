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
}