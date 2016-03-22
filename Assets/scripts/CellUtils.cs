using UnityEngine;
using System.Collections;

public static class CellUtils {
	
	public static bool HasObjectWithTag(GameObject cell, string tagName) {
		foreach (Transform child in cell.transform) {
			if (child.CompareTag (tagName)) {
				return true;
			}
		}
		return false;
	}

	public static GameObject FindObjectWithTag(GameObject cell, string tagName) {
		foreach (Transform child in cell.transform) {
			if (child.CompareTag (tagName)) {
				return child.gameObject;
			}
		}
		return null;
	}
}