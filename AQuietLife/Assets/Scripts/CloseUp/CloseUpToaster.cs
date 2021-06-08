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

    [SerializeField] private bool isOnToaster;

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
                isOnToaster = true;
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
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            FindObjectOfType<PointerManager>().ChangeCursor(5);
        else if (zoom.currentView == 1 && gameMng.isLocked == false && isOnToaster == true)
            FindObjectOfType<PointerManager>().ChangeCursor(2);
    }

    private void OnMouseExit()
    {
        FindObjectOfType<PointerManager>().ChangeCursor(1);
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
        isOnToaster = false;
    }
}
