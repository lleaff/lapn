using UnityEngine;
using System.Collections;

public class RabbitAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (string.Format("x: {0}, y: {1}", SM.i.CellDimensions.x, SM.i.CellDimensions.y));
	}
}
