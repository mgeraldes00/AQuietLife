using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHover : MonoBehaviour
{
    private IntroCursor cursor;

    [SerializeField] private int currentObj;

    private void Start()
    {
        cursor = FindObjectOfType<IntroCursor>();
    }

    private void OnMouseEnter()
    {
        switch (currentObj)
        {
            case 0:
                Cursor.SetCursor(cursor.interactTexture, cursor.hotSpot, cursor.curMode);
                break;
            case 1:
                Cursor.SetCursor(cursor.examineTexture, cursor.hotSpot, cursor.curMode);
                break;
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursor.defaultTexture, cursor.hotSpot, cursor.curMode);
    }
}
