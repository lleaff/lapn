using UnityEngine;
using System.Collections;



public class customcursor : MonoBehaviour
{
	public Texture2D cursorcarrot;
	public Texture2D cursorrake;
	public Texture2D cursorpelle;
	public Texture2D cursorremove;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
		//Invoke ("setcustomcursor", 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (globals.i.Button)
		{
		case 1:
			Cursor.SetCursor(cursorcarrot, hotSpot, cursorMode);
			break;
		case 3:
			Cursor.SetCursor(cursorrake, hotSpot, cursorMode);
			break;
		case 4:
			Cursor.SetCursor(cursorpelle, hotSpot, cursorMode);
			break;
		case 6:
			Cursor.SetCursor(cursorremove, hotSpot, cursorMode);
			break;
		default:
			Cursor.SetCursor (null, hotSpot, cursorMode);
			break;
		}
	}

	void setcustomcursor()
	{
		print("check cursor");
		print (globals.i.Button);
		switch (globals.i.Button)
		{
		case 1:
			Cursor.SetCursor(cursorcarrot, hotSpot, cursorMode);
			break;
		case 3:
			Cursor.SetCursor(cursorrake, hotSpot, cursorMode);
			break;
		case 4:
			Cursor.SetCursor(cursorpelle, hotSpot, cursorMode);
			break;
		case 6:
			Cursor.SetCursor(cursorremove, hotSpot, cursorMode);
			break;
		default:
			Cursor.SetCursor (null, hotSpot, cursorMode);
			break;
		}
	}
}

