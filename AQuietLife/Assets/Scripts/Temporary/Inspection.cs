using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection : MonoBehaviour
{
    [SerializeField] private ThoughtManager thought;
    [SerializeField] private GameManager gameMng;

    [SerializeField] private string inspectionText;
    [SerializeField] private string cursorType;

    private void OnMouseDown()
    {
        thought.ShowThought();
        thought.text = inspectionText;
    }

    private void OnMouseEnter()
    {
        if (cursorType != null)
        {
            gameMng.cursors.ChangeCursor(cursorType, 1);
        }
    }

    private void OnMouseExit()
    {
        if (cursorType != null)
        {
            gameMng.cursors.ChangeCursor(cursorType, 0);
        }
    }
}
