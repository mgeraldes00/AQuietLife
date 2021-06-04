using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpToaster : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public ToasterManager toasterMng;

    public GameObject returnArrow;
    //public GameObject rewindButton;

    public BoxCollider2D toaster;
    public BoxCollider2D[] zoomableObjs;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (gameMng.isLocked == false && zoom.currentView == 0)
            {
                zoom.cameraAnim.SetTrigger("ZoomToaster");
                StartCoroutine(TimeToZoom());
                zoom.ObjectTransition();
            }
        }
    }

    private void OnMouseEnter()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0)
        {
            Cursor.SetCursor
            (gameMng.pointer.examineTexture, gameMng.pointer.hotSpot, gameMng.pointer.curMode);
        }
    }

    private void OnMouseExit()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0)
        {
            Cursor.SetCursor
            (gameMng.pointer.defaultTexture, gameMng.pointer.hotSpot, gameMng.pointer.curMode);
        }   
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        toaster.enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;
        //returnArrow.SetActive(true);
        toasterMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        toaster.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
    }
}
