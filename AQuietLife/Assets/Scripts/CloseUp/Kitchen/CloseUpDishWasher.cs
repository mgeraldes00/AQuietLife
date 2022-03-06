using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpDishWasher : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public DishWasherManager dishwasherMng;
    public ThoughtManager thought;
    [SerializeField] private ObjectSelection select;

    public Animator returnArrow;

    public BoxCollider2D dishWasher;
    public BoxCollider2D[] zoomableObjs;

    [SerializeField] private string zoomTrigger;

    [SerializeField] private bool isOnWasher;

    private void OnMouseDown()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0)
        {
            if (dishwasherMng.isWorking == false)
            {
                zoom.ObjectTransition();
                zoom.cameraAnim.SetTrigger(zoomTrigger);
                StartCoroutine(TimeToZoom());
                dishWasher.size = new Vector2(3.11f, 0.52f);
                dishWasher.offset = new Vector2(0, 0.81f);
                isOnWasher = true;
                gameMng.cursors.ChangeCursor("Inspect", 0);
                if (dishwasherMng.isOpen == true)
                {
                    dishWasher.size = new Vector2(3.11f, 0.4f);
                    dishWasher.offset = new Vector2(0, -0.88f);
                    for (int i = 0; i < dishwasherMng.objects.Length; i++)
                        dishwasherMng.objects[i].
                            GetComponent<BoxCollider2D>().enabled = true;
                }
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
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            gameMng.cursors.ChangeCursor("Inspect", 1);
        else if (
            zoom.currentView == 1 && !gameMng.isLocked && isOnWasher)
        {
            if (select.usingGlove || select.usingStoveCloth)
            {
                gameMng.cursors.ChangeCursor("Point", 1);
                dishwasherMng.isPointing = true;
            }
            else
            {
                if (!dishwasherMng.isOpen)
                    gameMng.cursors.ChangeCursor("OpenDoor", 3);
                else
                    gameMng.cursors.ChangeCursor("OpenDoor", 4);
            }
        }
    }

    private void OnMouseExit()
    {
        gameMng.cursors.ChangeCursor("Inspect", 0);
        gameMng.cursors.ChangeCursor("Grab", 0);
        gameMng.cursors.ChangeCursor("OpenDoor", 0);
        if (dishwasherMng.isPointing)
        {
            gameMng.cursors.ChangeCursor("Point", 0);
            dishwasherMng.isPointing = false;
        }
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
        isOnWasher = false;
        dishWasher.size = new Vector2(3.11f, 2.13f);
        dishWasher.offset = new Vector2(0, 0);
    }
}
