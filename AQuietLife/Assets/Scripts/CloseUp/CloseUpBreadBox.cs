﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpBreadBox : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public BreadBoxManager breadBoxMng;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderBreadBox;
    //public GameObject inspectionTextGeneral;
    //public GameObject[] objsToZoom;
    //public GameObject breadBox;
    //public GameObject breadBoxButtons;
    //public GameObject activityText;
    //public GameObject breadBoxRewindButton;

    public BoxCollider2D breadBox;
    public BoxCollider2D[] zoomableObjs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Mouse Clicked");
                Vector3 mousePos =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider == null)
                {
                    //Nothing
                }

                else if (hit.collider.CompareTag("BreadBox") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomBreadbox");
                    StartCoroutine(TimeToZoom());
                }         
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //breadBoxRewindButton.SetActive(true);
        breadBox.enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;       
        returnArrow.SetActive(true);
        breadBoxMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        breadBox.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
    }
}
