﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpFridge : MonoBehaviour
{
    public GameManager gameMng;
    public FridgeManager fridgeMng;
    public CameraCtrl zoom;

    public GameObject[] directionArrows;

    public Animator[] dirArrows;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderFridge;
    //public GameObject inspectionTextGeneral;
    //public GameObject[] objsToZoom;
    //public GameObject fridge;
    //public GameObject fridgeButtons;
    //public GameObject activityText;
    
    //public GameObject fridgeRewindButton;

    public BoxCollider2D[] fridge;
    public BoxCollider2D[] zoomableObjs;
    public EdgeCollider2D bottomDoor;

    public bool isOnFridge;
    public bool isFridge;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2 =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

        if (hit2.collider == null)
        {
            if (isFridge == true)
            {
                gameMng.cursors.ChangeCursor("Inspect", 0);
                isFridge = false;
            }
            gameMng.cursors.ChangeCursor("OpenDoor", 0);
        }
        else
        {
            if (hit2.collider.CompareTag("Fridge")
                || hit2.collider.CompareTag("Freezer"))
            {
                if (zoom.currentView == 0 && !fridgeMng.onFridge)
                {
                    gameMng.cursors.ChangeCursor("Inspect", 1);
                    isFridge = true;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {     
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                gameMng.cursors.ChangeCursor("Inspect", 0);

                Debug.Log("Mouse Clicked");
                Vector3 mousePos =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider == null)
                {
                    //Nothing
                }

                else if (hit.collider.CompareTag("Fridge") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    fridgeMng.onFridge = true;
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomFridge");
                    //directionArrows[0].SetActive(true);
                    dirArrows[0].SetTrigger("Show");
                    fridgeMng.lookAtFridge = true;
                    StartCoroutine(TimeToZoom());
                }

                else if (hit.collider.CompareTag("Freezer") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    fridgeMng.onFridge = true;
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomFreezer");
                    //directionArrows[1].SetActive(true);
                    dirArrows[1].SetTrigger("Show");
                    fridgeMng.lookAtFridge = false;
                    StartCoroutine(TimeToZoom());
                }
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        fridge[0].enabled = false;
        fridge[1].enabled = false;
        yield return new WaitForSeconds(0.1f);
        //fridgeRewindButton.SetActive(true);
        if (fridgeMng.doorLeftOpen)
        {
            bottomDoor.enabled = true;
            fridgeMng.EnableObjs(1);
        }
        else if (!fridgeMng.doorLeftOpen)
            zoomableObjs[2].enabled = true;
        if (fridgeMng.doorRightOpen)
        {
            zoomableObjs[1].enabled = true;
            fridgeMng.EnableObjs(2);
        }
        else if (!fridgeMng.doorRightOpen)
        {
            zoomableObjs[0].enabled = true;
        }
        //returnArrow.SetActive(true);
        zoom.currentView++;
        isOnFridge = true;
    }

    public void Normalize()
    {
        if (fridgeMng.onFridge != true)
        {
            //Nothing
        }
        else if (fridgeMng.onFridge == true)
        {
            fridge[0].enabled = true;
            fridge[1].enabled = true;
            if (fridgeMng.lookAtFridge == true)
                dirArrows[0].SetTrigger("Hide");
            if (fridgeMng.lookAtFridge == false)
                dirArrows[1].SetTrigger("Hide");
            for (int i = 0; i < zoomableObjs.Length; i++)
                zoomableObjs[i].enabled = false;
            bottomDoor.enabled = false;
            isOnFridge = false;
        }
    }
}
