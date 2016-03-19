using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	public Vector2 Position;


	public void Init(Vector2 position) {
		Position = position;
	}

	public GameObject InstantiateIn(GameObject obj) {
		GameObject newObj = Instantiate (obj);
		newObj.transform.parent = transform;
		newObj.transform.localRotation = Quaternion.identity;
		newObj.transform.localPosition = Vector3.zero;
		newObj.transform.localScale = Vector3.one;
		return newObj;
	}
}
