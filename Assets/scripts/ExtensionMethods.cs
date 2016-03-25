using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public static T Pop<T>(this List<T> list) {
		if (list.Count == 0) {
			throw new UnityException ("Can't pop from empty list");
		}
		T el = list [list.Count - 1];
		list.Remove (el);
		return el;
	}
	public static T Last<T>(this List<T> list) {
		if (list.Count == 0) {
			throw new UnityException ("Can't get last element from empty list");
		}
		return list [list.Count - 1];
	}

	public static bool AddUnique<T>(this List<T> list, T el) {
		if (list.Contains (el)) {
			return false;
		} else {
			list.Add (el);
			return true;
		}
	}
}