using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroHover : MonoBehaviour
{
    private IntroCursor cursor;
    [SerializeField] private IntroController intro;

    [SerializeField] private int currentObj;

    private void Start()
    {
        cursor = FindObjectOfType<IntroCursor>();
    }

    private void OnMouseDown()
    {
        switch (currentObj)
        {
            case 0:
                cursor.ChangeCursor(4);
                break;
        }
    }

    private void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            switch (currentObj)
            {
                case 0:
                    cursor.ChangeCursor(2);
                    break;
                case 1:
                    if (intro.currentView == 0)
                        cursor.ChangeCursor(5);
                    else
                        cursor.ChangeCursor(1);
                    break;
                case 2:
                    cursor.ChangeCursor(3);
                    break;
            }
        }       
    }

    private void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            cursor.ChangeCursor(1);
    }
}
