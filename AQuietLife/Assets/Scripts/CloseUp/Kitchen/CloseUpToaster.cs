using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpToaster : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public ToasterManager toasterMng;

    [SerializeField] private ObjectSelection select;

    public GameObject returnArrow;
    //public GameObject rewindButton;

    public BoxCollider2D[] toaster;

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
                if (isOnToaster)
                {
                    toasterMng.CheckTrap();
                }
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        toaster[0].enabled = false;
        zoom.currentView++;
        yield return new WaitForSeconds(0.5f);
        if (!toasterMng.isHeating && !toasterMng.hasHeated)
        {
            toaster[0].enabled = true;
            toaster[1].enabled = true;
        }
        else if (toasterMng.hasHeated)
            toasterMng.objects[0].GetComponent<BoxCollider2D>().enabled = true;
        toaster[0].size = new Vector2(0.32f, 0.8f);
        toaster[0].offset = new Vector2(-0.48f, 0);
    }

    public void Normalize()
    {
        toaster[0].enabled = true;
        toaster[1].enabled = false;
        toaster[0].size = new Vector2(2.02f, 1.82f);
        toaster[0].offset = new Vector2(0.05f, 0.19f);
        toasterMng.isToaster = false;
        isOnToaster = false;
    }
}
