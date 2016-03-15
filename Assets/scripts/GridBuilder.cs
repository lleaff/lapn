using UnityEngine;
using System.Collections;


public class GridBuilder : MonoBehaviour {

	public string GridName = "Grid";
	public GameObject NodePrefab = null;
	public int GridHeight = 20;
	public int GridWidth = 20;
	public Vector2 CellDimensions = new Vector2 (5f, 5f);
	public Vector3 Middle = new Vector3(0, 0, 0);


	void Start () {
		BuildGrid (GridName, NodePrefab, GridHeight, GridWidth, CellDimensions, Middle);
	}

	void BuildGrid(
		string gridName = "Grid",
		GameObject nodeObject = null,
		int gridWidth = 20,
		int gridHeight = 20,
		Vector2 cellDimensions = default(Vector2),
		Vector3 middle = default(Vector3))
	{
		if (nodeObject == null) {
			nodeObject = new GameObject ();
		}
		Quaternion nodeObjectRotation = nodeObject.gameObject.transform.rotation;
		float offsetX = -((gridWidth * cellDimensions.x  / 2) + middle.x);
		float offsetZ = -((gridHeight * cellDimensions.y / 2) + middle.z);
		GameObject grid = new GameObject(gridName);

		for (int z = 0; z < gridHeight; z++) {
			for (int x = 0; x < gridWidth; x++) {
				GameObject node = Instantiate (
					nodeObject,
					new Vector3 (x * cellDimensions.x + offsetX, Middle.y, z * cellDimensions.y + offsetZ),
					nodeObjectRotation) as GameObject;
				node.name = string.Format ("GridNode({0}-{1})", x, z);
				node.transform.parent = grid.transform;
			}
		}
	}


	void Update () {
	
	}
}
