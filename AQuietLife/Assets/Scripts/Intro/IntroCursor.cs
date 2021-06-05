using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCursor : MonoBehaviour
{
    public IntroController gameMng;

    public Texture2D defaultTexture;
    public Texture2D interactTexture;
    public Texture2D pickUpTexture;
    public Texture2D examineTexture;
    public Texture2D doorTexture;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }
}
