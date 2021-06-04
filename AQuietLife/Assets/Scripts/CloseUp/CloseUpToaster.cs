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

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (gameMng.isLocked == false && zoom.currentView == 0)
            {
                zoom.cameraAnim.SetTrigger("ZoomToaster");
                StartCoroutine(TimeToZoom());
                zoom.ObjectTransition();
                toasterMng.isToaster = true;
            }
            else if (gameMng.isLocked == false && zoom.currentView == 1)
            {
                toasterMng.ToasterBehaviour();
                toasterMng.CheckTrap();
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
        else if (gameMng.isLocked == false && zoom.currentView == 1)
        {

        }
    }

    private void OnMouseExit()
    {
        if (gameMng.isLocked == false)
        {
            Cursor.SetCursor
            (gameMng.pointer.defaultTexture, gameMng.pointer.hotSpot, gameMng.pointer.curMode);
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        toaster.enabled = false;
        zoom.currentView++;
        yield return new WaitForSeconds(0.5f);
        toaster.enabled = true;
        if (toasterMng.isPlaced == true)
        {
            toaster.size = new Vector2(0.32f, 0.8f);
            toaster.offset = new Vector2(-0.48f, 0);
        }
        toasterMng.objects[0].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Normalize()
    {
        toaster.size = new Vector2(2.02f, 1.82f);
        toaster.offset = new Vector2(0.05f, 0.19f);
        toasterMng.isToaster = false;
    }
}
