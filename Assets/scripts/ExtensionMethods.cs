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
}