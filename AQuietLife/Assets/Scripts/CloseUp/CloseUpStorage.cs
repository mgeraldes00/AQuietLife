using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpStorage : MonoBehaviour
{
    public GameManager gameMng;
    public CameraCtrl zoom;

    [SerializeField] private BoxCollider2D[] doors;

    private void OnMouseDown()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0)
        {
            zoom.cameraAnim.SetTrigger("ZoomStorage");
            zoom.ObjectTransition();
            StartCoroutine(TimeToZoom());
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

    public void Normalize()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        /*for (int i = 0; i < doors.Length; i++)
            doors[i].enabled = false;*/
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < doors.Length; i++)
            doors[i].enabled = true;
        zoom.currentView++;
    }
}
