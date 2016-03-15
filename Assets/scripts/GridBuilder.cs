﻿using UnityEngine;
using System.Collections;


public class GridBuilder : MonoBehaviour {

	public string GridName = "Grid";
	public GameObject NodePrefab = null;
	public int GridHeight = 20;
	public int GridWidth = 20;
	public float CellWidth = 5f;
	public float CellHeight = 5f;
	public Vector3 Middle = new Vector3(0, 0, 0);


	void Start () {
		if (NodePrefab == null) {
			NodePrefab = new GameObject ();
		}
		Quaternion nodePrefabRotation = NodePrefab.gameObject.transform.rotation;
		float offsetX = -((GridWidth * CellWidth  / 2) + Middle.x);
		float offsetZ = -((GridHeight * CellHeight / 2) + Middle.z);
		GameObject grid = new GameObject(GridName);

		for (int z = 0; z < GridHeight; z++) {
			for (int x = 0; x < GridWidth; x++) {
				GameObject node = Instantiate (NodePrefab, new Vector3 (x * CellWidth + offsetX, Middle.y, z * CellHeight + offsetZ), nodePrefabRotation) as GameObject;
				node.name = string.Format ("GridNode({0}-{1})", x, z);
				node.transform.parent = grid.transform;
			}
		}
	}
	

	void Update () {
	
	}
}
