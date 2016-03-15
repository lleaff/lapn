using UnityEngine;
using System.Collections;


public class WorldGrid : MonoBehaviour {

	public string GridName = "Grid";
	public GameObject NodePrefab = null;
	public int GridHeight = 20;
	public int GridWidth = 20;
	public Vector2 CellDimensions;
	public Vector3 Middle = new Vector3(0, 0, 0);

	void Start () {
		GridBuilder.BuildGrid (GridName, NodePrefab, GridHeight, GridWidth, CellDimensions, Middle);
	}

	void Update () {
	
	}
}
