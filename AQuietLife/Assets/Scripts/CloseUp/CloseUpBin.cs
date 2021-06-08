using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpBin : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public BinManager binMng;

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
                FindObjectOfType<PointerManager>().ChangeCursor(1);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            FindObjectOfType<PointerManager>().ChangeCursor(5);
        else if (zoom.currentView == 1 && gameMng.isLocked == false)
            FindObjectOfType<PointerManager>().ChangeCursor(2);
    }

    private void OnMouseExit()
    {
        FindObjectOfType<PointerManager>().ChangeCursor(1);
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        //binRewindButton.SetActive(true);
        zoom.currentView++;
        isOnTrash = true;
    }

    public void Normalize()
    {
        isOnTrash = false;
    }
}
