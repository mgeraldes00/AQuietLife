using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpMisc : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public MiscManager miscMng;

    public GameObject returnArrow;

    public BoxCollider2D currentObj;
    public BoxCollider2D[] zoomableObjs;

    [SerializeField] private string zoomTrigger;

    [SerializeField] private bool isOnObj;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (gameMng.isLocked == false && zoom.currentView == 0)
            {
                zoom.ObjectTransition();
                zoom.cameraAnim.SetTrigger(zoomTrigger);
                StartCoroutine(TimeToZoom());
                isOnObj = true;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            FindObjectOfType<PointerManager>().ChangeCursor(5);
        else if (zoom.currentView == 1 && gameMng.isLocked == false && isOnObj == true)
            FindObjectOfType<PointerManager>().ChangeCursor(2);
    }

    private void OnMouseExit()
    {
        FindObjectOfType<PointerManager>().ChangeCursor(1);
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        currentObj.enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;
        //returnArrow.SetActive(true);
        miscMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        currentObj.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
        isOnObj = false;
    }
}
