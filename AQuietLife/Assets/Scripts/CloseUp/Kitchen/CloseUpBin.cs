using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpBin : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public BinManager binMng;
    public ObjectSelection obSel;

    public GameObject returnArrow;
    //public GameObject binRewindButton;

    public BoxCollider2D bin;

    public bool isOnTrash;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (gameMng.isLocked == false && zoom.currentView == 0)
            {
                zoom.cameraAnim.SetTrigger("ZoomTrashBin");
                StartCoroutine(TimeToZoom());
                zoom.ObjectTransition();
                gameMng.cursors.ChangeCursor("Inspect", 0);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0)
            gameMng.cursors.ChangeCursor("Inspect", 1);
        else if (zoom.currentView == 1)
            if (!binMng.isOpen)
                if (obSel.usingGlove || obSel.usingStoveCloth)
                    gameMng.cursors.ChangeCursor("Point", 1);
                else
                    gameMng.cursors.ChangeCursor("OpenDoor", 1);
            else
                gameMng.cursors.ChangeCursor("Inspect", 1);
    }

    private void OnMouseExit()
    {
        gameMng.cursors.ChangeCursor("Inspect", 0);
        gameMng.cursors.ChangeCursor("OpenDoor", 0);
        gameMng.cursors.ChangeCursor("Point", 0);
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        //binRewindButton.SetActive(true);
        if (!binMng.isOpen)
        {
            bin.offset = new Vector2(0.53f, -0.1f);
            bin.size = new Vector2(0.3f, 0.6f);
        }
        zoom.currentView++;
        isOnTrash = true;
    }

    public void Normalize()
    {
        isOnTrash = false;
        bin.offset = new Vector2(0, -0.04f);
        bin.size = new Vector2(1.49f, 1.99f);
    }
}
