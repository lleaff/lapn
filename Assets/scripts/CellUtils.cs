using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public static bool IsFieldNode(Transform n) {
		return n.name.BeginsWith (globals.fieldNodeName);
	}
	public static bool IsFieldNode(GameObject n) {
		return IsFieldNode(n.transform);
	}
	public static bool IsField(Transform n) {
		return n.name.BeginsWith (globals.fieldName);
	}
	public static bool IsField(GameObject n) {
		return IsFieldNode(n.transform);
	}
	public static bool IsCarrot(Transform n) {
		return n.name.BeginsWith (globals.carrotName);
	}
	public static bool IsCarrot(GameObject n) {
		return IsCarrot(n.transform);
	}

	public static GameObject FindObjectWithNameBeginsWith(GameObject cell, string name) {
		foreach (Transform child in cell.transform) {
			if (child.name.BeginsWith (name)) {
				return child.gameObject;
			}
		}
		return null;
	}

	public static GameObject GetCarrotObj(Transform field) {
		return GetCarrotObj (field.gameObject);
	}
	public static GameObject GetCarrotObj(GameObject field) {
		foreach (Transform child in field.transform) {
			if (IsField (child)) {
				return (child.gameObject);
			}
		}
		return null;
	}

	public static List<GameObject> GetCarrots(GameObject field) {
		List<GameObject> carrots = new List<GameObject>();
		foreach (Transform child in field.transform) {
			if (IsCarrot(child)) {
				carrots.Add (child.gameObject);
			}
		}
		return carrots;
	}

	public static int CountCarrots(GameObject field) {
		int count = 0;
		foreach (Transform child in field.transform) {
			if (IsCarrot(child)) {
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
		if (!carrot || !IsCarrot(carrot)) {
			return false;
		}
		MonoBehaviour.Destroy (carrot);
		return true;
	}
}