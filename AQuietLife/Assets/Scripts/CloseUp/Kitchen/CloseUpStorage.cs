using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpStorage : MonoBehaviour
{
    public GameManager gameMng;
    public StorageManager storage;
    public CameraCtrl zoom;

    public BoxCollider2D[] doors;
    public PolygonCollider2D lockedDoor;
    public BoxCollider2D keyHole;

    private void OnMouseUp()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0)
        {
            zoom.cameraAnim.SetTrigger("ZoomStorage");
            zoom.ObjectTransition();
            if (storage.leftDoorCol == true)
                StartCoroutine(storage.EnableObjs(0));
            if (storage.rightDoorCol == true)
                StartCoroutine(storage.EnableObjs(1));
            StartCoroutine(TimeToZoom());
        }
    }

    private void OnMouseEnter()
    {
        gameMng.cursors.ChangeCursor("Inspect", 1);
    }

    private void OnMouseExit()
    {
        gameMng.cursors.ChangeCursor("Inspect", 0);
    }

    public void Normalize()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        for (int i = 0; i < doors.Length; i++)
            doors[i].enabled = false;
        if (storage.isLocked == true)
            lockedDoor.enabled = false;
        storage.isOnDoor = false;
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().enabled = false;
        storage.isOnDoor = true;
        if (storage.isLocked == true)
        {
            lockedDoor.enabled = true;
            doors[1].enabled = true;
            keyHole.enabled = true;
        }
        else if (storage.isLocked == false)
            if (storage.leftDoorCol == true && storage.rightDoorCol == false)
                for (int i = 1; i < 3; i++)
                    doors[i].enabled = true;
            else if (storage.leftDoorCol == false && storage.rightDoorCol == true)
            {
                doors[0].enabled = true;
                doors[3].enabled = true;
            }
            else if (storage.leftDoorCol == false && storage.rightDoorCol == false)
                for (int i = 0; i < 2; i++)
                    doors[i].enabled = true;
            else if (storage.leftDoorCol == true && storage.rightDoorCol == true)
                for (int i = 2; i < doors.Length; i++)
                    doors[i].enabled = true;
        zoom.currentView++;
    }
}
