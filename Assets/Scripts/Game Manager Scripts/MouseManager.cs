using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Texture2D crosshair;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    // Start is called before the first frame update
    public void OnMouseEnter()
    {
        Cursor.SetCursor(crosshair, hotSpot, cursorMode);
    }


    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
