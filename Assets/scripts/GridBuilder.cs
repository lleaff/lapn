using UnityEngine;
using System.Collections;

public static class GridBuilder {

	public static GameObject[,] BuildGrid(
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

		GameObject gridObject = InstantiateGrid (gridName, gridWidth, gridHeight, cellDimensions);

		GameObject[,] grid = new GameObject[gridWidth, gridHeight];
		for (int z = 0; z < gridHeight; z++) {
			for (int x = 0; x < gridWidth; x++) {
				GameObject node = GameObject.Instantiate (
					nodeObject,
					new Vector3 (x * cellDimensions.x + offsetX, middle.y, z * cellDimensions.y + offsetZ),
					nodeObjectRotation) as GameObject;
				node.name = string.Format ("GridNode({0}-{1})", x, z);
				node.transform.parent = gridObject.transform;
				grid [x, z] = node;
			}
		}
		return grid;
	}


	static GameObject InstantiateGrid(string name, int width, int height, Vector2 cellDimensions)
	{
		GameObject grid = new GameObject (name);
		grid.AddComponent<Grid> ();
		Grid data = grid.GetComponent<Grid>();
		data.Width = width;
		data.Height = height;
		data.CellDimensions = cellDimensions;
		data.Lock();
		return grid;
	}

}