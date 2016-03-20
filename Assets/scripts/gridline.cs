using UnityEngine;
using System.Collections;

public class gridline : MonoBehaviour {

	public int gridSizeX;
	public int gridSizeZ;

	public float startX;
	public float startZ;

	private Material lineMaterial;

	private Color mainColor = new Color(1f,1f,1f,1f);
		
	void CreateLineMaterial() 
	{

		if( !lineMaterial ) {
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
				"SubShader { Pass { " +
				"    Blend SrcAlpha OneMinusSrcAlpha " +
				"    ZWrite Off Cull Off Fog { Mode Off } " +
				"    BindChannels {" +
				"      Bind \"vertex\", vertex Bind \"color\", color }" +
				"} } }" );
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	void OnPostRender() 
	{        
		CreateLineMaterial();
		// set the current material
		lineMaterial.SetPass( 0 );

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
