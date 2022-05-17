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

    public GameObject[] openDoor;

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
            && zoom.currentView == 1 && !isLocked && closeUp.isOnTrash)
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
                    gameMng.cursors.ChangeCursor("Point", 0);
                    gameMng.cursors.ChangeCursor("OpenDoor", 1);
                    FindObjectOfType<AudioCtrl>().Play("Disarm");
                    FindObjectOfType<Glove>().gloveUsed = true;
                    StartCoroutine(Untrap());
                }

                if (select.usingStoveCloth == true)
                {
                    gameMng.cursors.ChangeCursor("Point", 0);
                    gameMng.cursors.ChangeCursor("OpenDoor", 1);
                    FindObjectOfType<AudioCtrl>().Play("Disarm");
                    FindObjectOfType<StoveCloth>().gloveUsed = true;
                    StartCoroutine(Untrap());
                }
            }
            else if (isTrapped == false)
            {
                if (select.usingGlove || select.usingStoveCloth)
                {
                    select.usingGlove = false;
                    select.usingStoveCloth = false;

                    gameMng.cursors.ChangeCursor("Point", 0);
                    gameMng.cursors.ChangeCursor("OpenDoor", 1);
                }
                else
                {
                    if (!isOpen)
                    {
                        FindObjectOfType<AudioCtrl>().Play("OpenCabinetDoor");
                        isOpen = true;

                        gameMng.cursors.ChangeCursor("Inspect", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);

                        closeUp.bin.offset = new Vector2(0, -0.04f);
                        closeUp.bin.size = new Vector2(1.49f, 1.99f);

                        LockAndUnlock();
                        StartCoroutine(ObjectFade.FadeIn(
                            bin[1].GetComponent<SpriteRenderer>()));
                        StartCoroutine(ObjectFade.FadeIn(
                            bin[2].GetComponent<SpriteRenderer>()));
                        StartCoroutine(ObjectFade.FadeOut(
                            bin[0], 0, 0));
                    }
                    else
                    {
                        gameMng.cursors.ChangeCursor("Inspect", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);

                        LockAndUnlock();
                        gameMng.returnable = false;
                        StartCoroutine(ShowDoor(openDoor));
                        zoom.currentView++;
                    }
                }
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && !isLocked && !gameMng.isLocked
            && closeUp.isOnTrash)
        {
            closeUp.Normalize();
        }
        else if (zoom.currentView == 2 && !isLocked && !gameMng.returnable
            && closeUp.isOnTrash && !gameMng.isLocked)
        {
            LockAndUnlock();
            StartCoroutine(HideDoor());
            StartCoroutine(BackZoom());
        }
    }

    public void LockAndUnlock()
    {
        if (!isLocked)
        {
            gameMng.isLocked = true;
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    IEnumerator ShowDoor(GameObject[] door)
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < door.Length; i++)
        {
            StartCoroutine(ObjectFade.FadeIn(
                door[i].GetComponent<SpriteRenderer>()));
        }
        yield return new WaitForSeconds(1.0f);
        door[1].GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
        gameMng.isLocked = false;
    }

    IEnumerator HideDoor()
    {
        if (openDoor[1] != null)
        {
            openDoor[1].GetComponent<BoxCollider2D>().enabled = false;
        }
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < openDoor.Length; i++)
        {
            StartCoroutine(ObjectFade.FadeOut(
                openDoor[i], 0, 0));
        }
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
