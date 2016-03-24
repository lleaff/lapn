using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utils {

	public static List<T> ToList<T>(T[] array) {
		List<T> list = new List<T>();
		foreach (T val in array) {
			list.Add(val);
		}
		return list;
	}

	public class DistanceComparer : IComparer<GameObject> {
		GameObject Obj;
		public DistanceComparer(GameObject obj) {
			Obj = obj;
		}

		public int Compare(GameObject a, GameObject b) {
			float diff = Obj.DistanceTo(a) - Obj.DistanceTo(b);
			if (diff > 0) {
				return 1;
			} else if (diff == 0) {
				return 0;
			} else {
				return -1;
			}
		}
	}

	public static bool isDestroyed (GameObject obj) {
		return (obj == null || obj.Equals (null));
	}
}
