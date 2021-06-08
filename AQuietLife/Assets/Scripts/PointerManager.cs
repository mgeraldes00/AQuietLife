using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public Texture2D defaultTexture;
    public Texture2D interactTexture;
    public Texture2D pickUpTexture;
    public Texture2D examineTexture;
    public Texture2D clickTexture;
    public Texture2D doorTexture;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    [SerializeField] private int currentCursor;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }

    public void ChangeCursor(int i)
    {
        currentCursor = i;

        switch (currentCursor)
        {
            case 1:
                Cursor.SetCursor(defaultTexture, hotSpot, curMode);
                break;
            case 2:
                Cursor.SetCursor(interactTexture, hotSpot, curMode);
                break;
            case 3:
                Cursor.SetCursor(pickUpTexture, hotSpot, curMode);
                break;
            case 4:
                Cursor.SetCursor(clickTexture, hotSpot, curMode);
                break;
            case 5:
                Cursor.SetCursor(examineTexture, hotSpot, curMode);
                break;
        }
    }
}
