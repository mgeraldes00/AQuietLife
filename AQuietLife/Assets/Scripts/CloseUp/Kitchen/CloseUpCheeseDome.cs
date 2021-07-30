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
                cheeseMng.CheeseBehaviour();
                cheeseMng.CheckTrap();
            }
        }
    }

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            FindObjectOfType<PointerManager>().ChangeCursor(5);
        else if (zoom.currentView == 1 && gameMng.isLocked == false && isOnDome == true)
            FindObjectOfType<PointerManager>().ChangeCursor(2);
    }

    private void OnMouseExit()
    {
        FindObjectOfType<PointerManager>().ChangeCursor(1);
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        rewindButton.SetActive(true);
        rewindButton.GetComponent<Animator>().SetBool("Visible", true);
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
        isOnDome = false;
    }
}
