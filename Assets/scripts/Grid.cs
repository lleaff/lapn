using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour  {
	private bool locked = false;

	public void Lock() {
		locked = true;
	}

	private string lockedAccessWriteMsg = "Grid: Can't modify locked object.";

	private int width;
	public int Width {
		get { return width; }
		set { if (!locked) width = value; else throw new System.Exception(lockedAccessWriteMsg); }
	}

	private int height;
	public int Height {
		get { return height; }
		set { if (!locked) height = value; else throw new System.Exception(lockedAccessWriteMsg); }
	}

	private Vector2 cellDimensions;
	public Vector2 CellDimensions {
		get { return cellDimensions; }
		set { if (!locked) cellDimensions = value; else throw new System.Exception(lockedAccessWriteMsg); }
	}
}
