﻿using System.Collections;
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
            }
        }
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