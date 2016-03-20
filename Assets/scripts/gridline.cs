using UnityEngine;
using System.Collections;

public class gridline : MonoBehaviour {

	public int gridSizeX;
	public int gridSizeZ;

	public float startX;
	public float startZ;

	private Material lineMaterial;

	private Color mainColor = new Color(0f,0f,0f,1f);

	void OnPostRender() 
	{        
		GL.Begin( GL.LINES );
		GL.Color(mainColor);

		for(float i = 0; i <= gridSizeZ; i++)
		{
			GL.Vertex3( startX, 0.1F, startZ + i);
			GL.Vertex3( startX + gridSizeX, 0.1F, startZ + i);
		}

		//Z axis lines
		for(float i = 0; i <= gridSizeX; i++)
		{
			GL.Vertex3( startX + i, 0.1F, startZ);
			GL.Vertex3( startX + i, 0.1F, startZ + gridSizeZ);
		}
		GL.End();
	}
}
