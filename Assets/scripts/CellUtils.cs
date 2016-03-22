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

	public static GameObject FindObjectWithNameBeginsWith(GameObject cell, string name) {
		foreach (Transform child in cell.transform) {
			if (child.name.BeginsWith (name)) {
				return child.gameObject;
			}
		}
		return null;
	}


	public static int CountCarrots(GameObject field) {
		int count = 0;
		foreach (Transform child in field.transform) {
			if (child.name.BeginsWith(globals.carrotName)) {
				count++;
			}
		}
		return count;
	}

	public static bool RemoveCarrot(GameObject field) {
		if (!field.name.BeginsWith (globals.fieldName)) {
			throw new UnityException ("RemoveCarrot: Object is not a field.");
		}
		GameObject carrot = field.transform.GetChild (0).gameObject;
		if (!carrot || !carrot.name.BeginsWith (globals.carrotName)) {
			return false;
		}
		MonoBehaviour.Destroy (carrot);
		return true;
	}
}