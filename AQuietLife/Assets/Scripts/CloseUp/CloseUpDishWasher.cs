using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpDishWasher : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public DishWasherManager dishwasherMng;

    public GameObject returnArrow;

    public BoxCollider2D dishWasher;
    public BoxCollider2D[] zoomableObjs;

    [SerializeField] private string zoomTrigger;

    private void OnMouseDown()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0)
        {
            zoom.ObjectTransition();
            zoom.cameraAnim.SetTrigger(zoomTrigger);
            StartCoroutine(TimeToZoom());
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //dishWasher.enabled = false;
        /*for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;*/
        returnArrow.SetActive(true);
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
