using UnityEngine;
using System.Collections;

public class GridBuilder : MonoBehaviour {

	public Transform node;
	public int GridHeight = 20;
	public int GridWidth = 20;
	public Vector3 Middle = new Vector3(0, 0, 0);


	void Start () {
		float offsetX = -((GridWidth  / 2) + Middle.x);
		float offsetZ = -((GridHeight / 2) + Middle.z);

		for (int z = 0; z < GridHeight; z++) {
			for (int x = 0; x < GridWidth; x++) {
				Instantiate (node, new Vector3 (x + offsetX, Middle.y, z + offsetZ), Quaternion.identity);
			}
		}
	}
	

	void Update () {
	
	}
}
