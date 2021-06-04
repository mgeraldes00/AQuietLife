﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpDishWasher : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public DishWasherManager dishwasherMng;
    public ThoughtManager thought;

    public Animator returnArrow;

    public BoxCollider2D dishWasher;
    public BoxCollider2D[] zoomableObjs;

    [SerializeField] private string zoomTrigger;

    private void OnMouseDown()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0
            && !EventSystem.current.IsPointerOverGameObject())
        {
            if (dishwasherMng.isWorking == false)
            {
                zoom.ObjectTransition();
                zoom.cameraAnim.SetTrigger(zoomTrigger);
                StartCoroutine(TimeToZoom());
            }
            else if (dishwasherMng.isWorking == true)
            {
                thought.ShowThought();
                thought.text = "Should be done soon..";
            }
        }
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor
            (gameMng.pointer.examineTexture, gameMng.pointer.hotSpot, gameMng.pointer.curMode);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor
            (gameMng.pointer.defaultTexture, gameMng.pointer.hotSpot, gameMng.pointer.curMode);
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //dishWasher.enabled = false;
        /*for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;*/
        //returnArrow.SetTrigger("Show");
        //dishwasherMng.EnableObjs();
        dishwasherMng.isActive = true;
        zoom.currentView++;
    }

    public void Normalize()
    {
        dishWasher.enabled = true;
        /*for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;*/
        dishwasherMng.isActive = false;
    }
}
