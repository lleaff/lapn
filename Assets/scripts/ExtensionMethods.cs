using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

	public static void Fill<T>(this T[] array, T fillValue) {
		for (int i = array.Length - 1; i >= 0; i--) {
			array [i] = fillValue;
		}
	}


}