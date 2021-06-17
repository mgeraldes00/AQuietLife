using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpBin closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject openDoor;

    public GameObject[] bin;

    public GameObject returnArrow;
    //public GameObject binRewindButton;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;

    public bool isOpen;

    private void Start()
    {
        isLocked = false;
        isTrapped = true;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
            && zoom.currentView == 1 && isLocked == false && closeUp.isOnTrash == true)
        {
            if (isTrapped == true)
            {
                if (select.usingNothing == true)
                {
                    Debug.Log("Game Over");
                    gameMng.Die();
                }

                if (select.usingGlove == true)
                {
                    FindObjectOfType<AudioCtrl>().Play("Disarm");
                    FindObjectOfType<Glove>().gloveUsed = true;
                    StartCoroutine(Untrap());
                }

                if (select.usingStoveCloth == true)
                {
                    FindObjectOfType<AudioCtrl>().Play("Disarm");
                    FindObjectOfType<StoveCloth>().gloveUsed = true;
                    StartCoroutine(Untrap());
                }
            }
            else if (isTrapped == false)
            {
                isOpen = true;
                gameMng.returnable = false;
                LockAndUnlock();
                zoom.InteractionTransition();
                StartCoroutine(ShowDoor(openDoor));
                zoom.currentView++;
                FindObjectOfType<AudioCtrl>().Play("OpenCabinetDoor");
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && isLocked == false)
        {
            closeUp.Normalize();
        }
        else if (zoom.currentView == 2 && isLocked == false && gameMng.returnable == false
            && closeUp.isOnTrash == true)
        {
            LockAndUnlock();
            zoom.InteractionTransition();
            StartCoroutine(HideDoor());
            StartCoroutine(BackZoom());
        }
    }

    public void LockAndUnlock()
    {
        if (isLocked == false)
        {
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    IEnumerator ShowDoor(GameObject door)
    {

        yield return new WaitForSeconds(0.2f);
        door.SetActive(true);
        bin[0].GetComponent<SpriteRenderer>().enabled = false;
        bin[1].SetActive(true);
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1);
        isLocked = false;
    }

    IEnumerator HideDoor()
    {
        yield return new WaitForSeconds(0.2f);
        openDoor.SetActive(false);
    }

    IEnumerator BackZoom()
    {
        yield return new WaitForSeconds(0.1f);
        zoom.currentView--;
    }

    IEnumerator Untrap()
    {
        yield return new WaitForEndOfFrame();
        isTrapped = false;
    }
}
