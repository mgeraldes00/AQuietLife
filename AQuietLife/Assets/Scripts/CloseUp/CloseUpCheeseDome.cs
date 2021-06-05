using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpCheeseDome : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CheeseDomeManager cheeseMng;

    public GameObject returnArrow;
    //public GameObject rewindButton;

    public BoxCollider2D cheeseDome;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (gameMng.isLocked == false && zoom.currentView == 0)
            {
                zoom.cameraAnim.SetTrigger("ZoomCheeseDome");
                StartCoroutine(TimeToZoom());
                zoom.ObjectTransition();
                cheeseMng.isCheese = true;
            }
            else if (gameMng.isLocked == false && zoom.currentView == 1)
            {
                cheeseMng.CheeseBehaviour();
                cheeseMng.CheckTrap();
            }
        }
    }

    private void OnMouseEnter()
    {
        if (gameMng.isLocked == false && zoom.currentView == 0)
        {
            Cursor.SetCursor
            (gameMng.pointer.examineTexture, gameMng.pointer.hotSpot, gameMng.pointer.curMode);
        }
        else if (gameMng.isLocked == false && zoom.currentView == 1)
        {

        }
    }

    private void OnMouseExit()
    {
        if (gameMng.isLocked == false)
        {
            Cursor.SetCursor
            (gameMng.pointer.defaultTexture, gameMng.pointer.hotSpot, gameMng.pointer.curMode);
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        cheeseDome.enabled = false;
        zoom.currentView++;
        yield return new WaitForSeconds(0.5f);
        cheeseDome.enabled = true;
        if (cheeseMng.isOpen == true)
        {
            cheeseDome.offset = new Vector2(1.68f, -1.01f);
            cheeseDome.size = new Vector2(4.87f, 3.95f);
        }
        cheeseMng.objects[2].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Normalize()
    {
        cheeseDome.offset = new Vector2(0, 0);
        cheeseDome.size = new Vector2(10, 8);
        cheeseMng.isCheese = false;
    }
}
