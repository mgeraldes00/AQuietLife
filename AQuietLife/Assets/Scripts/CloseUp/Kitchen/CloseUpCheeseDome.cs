using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpCheeseDome : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CheeseDomeManager cheeseMng;
    [SerializeField] private ObjectSelection select;

    public GameObject returnArrow;
    public GameObject rewindButton;

    public BoxCollider2D cheeseDome;

    [SerializeField] private bool isOnDome;

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
                isOnDome = true;
            }
            else if (gameMng.isLocked == false && zoom.currentView == 1)
            {
                if (isOnDome)
                {
                    cheeseMng.CheeseBehaviour();
                    cheeseMng.CheckTrap();
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            gameMng.cursors.ChangeCursor("Inspect", 1);
        else if (
            zoom.currentView == 1 && !gameMng.isLocked && isOnDome)
        {
            if (select.usingGlove || select.usingStoveCloth)
            {
                gameMng.cursors.ChangeCursor("Point", 1);
                cheeseMng.isPointing = true;
            }
            else
            {
                if (!cheeseMng.isOpen)
                    gameMng.cursors.ChangeCursor("OpenDoor", 4);
                else
                    if (!select.usingKnife)
                        gameMng.cursors.ChangeCursor("Inspect", 1);
                    else
                    {
                        gameMng.cursors.ChangeCursor("Point", 1);
                        cheeseMng.isPointing = true;
                    }
            }
        }
    }

    private void OnMouseExit()
    {
        gameMng.cursors.ChangeCursor("Inspect", 0);
        gameMng.cursors.ChangeCursor("Grab", 0);
        if (cheeseMng.isPointing)
        {
            gameMng.cursors.ChangeCursor("Point", 0);
            cheeseMng.isPointing = false;
        }
        gameMng.cursors.ChangeCursor("OpenDoor", 0);
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        rewindButton.SetActive(true);
        rewindButton.GetComponent<Animator>().SetBool("Visible", true);
        cheeseDome.enabled = false;
        zoom.currentView++;
        yield return new WaitForSeconds(0.5f);
        if (!cheeseMng.isTaken)
            cheeseDome.enabled = true;
        if (!cheeseMng.isOpen)
        {
            cheeseDome.offset = new Vector2(0, 3);
            cheeseDome.size = new Vector2(2.25f, 2.4f);
        }
        else
        {
            cheeseDome.offset = new Vector2(1.68f, -1.01f);
            cheeseDome.size = new Vector2(4.87f, 3.95f);
        }
        cheeseMng.objects[2].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Normalize()
    {
        cheeseDome.enabled = true;
        cheeseDome.offset = new Vector2(0, 0);
        cheeseDome.size = new Vector2(10, 8);
        cheeseMng.isCheese = false;
        isOnDome = false;
    }
}
