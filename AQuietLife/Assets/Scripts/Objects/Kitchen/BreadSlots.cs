using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadSlots : MonoBehaviour
{
    [SerializeField] private ThoughtManager thought;
    [SerializeField] private GameManager gameMng;
    [SerializeField] private ToasterManager toasterMng;

    [SerializeField] private string inspectionText;

    private void OnMouseDown()
    {
        if (!toasterMng.select.usingFrozenBread && !toasterMng.isPlaced)
        {
            thought.ShowThought();
            thought.text = inspectionText;
        }
        else if (toasterMng.select.usingFrozenBread)
        {
            toasterMng.ToasterBehaviour();
        }
        else if (toasterMng.isPlaced && !toasterMng.hasHeated)
        {
            StartCoroutine(toasterMng.RemoveBread());
        }
    }

    private void OnMouseEnter()
    {
        if (!toasterMng.select.usingFrozenBread && !toasterMng.isPlaced)
            gameMng.cursors.ChangeCursor("Inspect", 1);
        else if (toasterMng.select.usingFrozenBread)
            gameMng.cursors.ChangeCursor("Point", 1);
        else if (toasterMng.isPlaced)
            gameMng.cursors.ChangeCursor("Grab", 1);
    }

    private void OnMouseExit()
    {
        gameMng.cursors.ChangeCursor("Inspect", 0);
        gameMng.cursors.ChangeCursor("Point", 0);
        gameMng.cursors.ChangeCursor("Grab", 0);
    }
}
