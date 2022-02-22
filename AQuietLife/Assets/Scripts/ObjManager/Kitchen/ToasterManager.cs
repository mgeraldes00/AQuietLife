using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpToaster closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;
    [SerializeField] private BreadSlots slots;

    public GameObject[] objects;

    public GameObject returnArrow;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;
    public bool isHeating;
    public bool hasHeated;
    [SerializeField] private bool isPointing;

    public bool isPlaced;
    public bool isToaster;

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            gameMng.cursors.ChangeCursor("Inspect", 1);
        else if (zoom.currentView == 1 && !gameMng.isLocked)
        {
            if (select.usingGlove || select.usingStoveCloth)
            {
                gameMng.cursors.ChangeCursor("Point", 1);
                isPointing = true;
            }
            else
            {
                gameMng.cursors.ChangeCursor("OpenDoor", 3);
            }
        }
    }

    private void OnMouseExit()
    {
        gameMng.cursors.ChangeCursor("Inspect", 0);
        gameMng.cursors.ChangeCursor("Grab", 0);
        if (isPointing)
        {
            gameMng.cursors.ChangeCursor("Point", 0);
            isPointing = false;
        }
        gameMng.cursors.ChangeCursor("OpenDoor", 0);
    }

    private void Start()
    {
        isLocked = false;
        isTrapped = true;
    }

    public void CheckTrap()
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
                gameMng.cursors.ChangeCursor("OpenDoor", 3);
                FindObjectOfType<AudioCtrl>().Play("Disarm");
                FindObjectOfType<Glove>().gloveUsed = true;
                StartCoroutine(Untrap());
            }

            if (select.usingStoveCloth == true)
            {
                gameMng.cursors.ChangeCursor("Point", 0);
                gameMng.cursors.ChangeCursor("OpenDoor", 3);
                FindObjectOfType<AudioCtrl>().Play("Disarm");
                FindObjectOfType<StoveCloth>().gloveUsed = true;
                StartCoroutine(Untrap());
            }
        }
        else if (isLocked == false && isTrapped == false)
        {
            if (select.usingGlove || select.usingStoveCloth)
            {
                select.usingGlove = false;
                select.usingStoveCloth = false;

                gameMng.cursors.ChangeCursor("Point", 0);
                gameMng.cursors.ChangeCursor("OpenDoor", 3);
            }
            else if (hasHeated == false && isPlaced == true)
            {
                closeUp.toaster[0].enabled = false;
                closeUp.toaster[1].enabled = false;
                StartCoroutine(ObjectFade.FadeOut(objects[4], 0, 0));
                StartCoroutine(ObjectFade.FadeOut(objects[5], 0, 0));
                objects[1].GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(Toasting());
            }
        }
    }

    public void ToasterBehaviour()
    {
        if (isLocked == false)
        {
            if (select.usingFrozenBread == true)
            {
                //zoom.InteractionTransition();
                StartCoroutine(PlaceBread());
                LockAndUnlock();
                FindObjectOfType<FrozenBread>().frozenBreadUsed = true;
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && isToaster == true)
        {
            closeUp.Normalize();
            StartCoroutine(TimeToTransition());
        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForEndOfFrame();
        objects[0].GetComponent<BoxCollider2D>().enabled = false;
        objects[1].GetComponent<BoxCollider2D>().enabled = false;
    }

    public void LockAndUnlock()
    {
        if (isLocked == false)
        {
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    IEnumerator PlaceBread()
    {
        yield return new WaitForEndOfFrame();
        objects[1].SetActive(true);
        StartCoroutine(ObjectFade.FadeIn(
            objects[4].GetComponent<SpriteRenderer>()));
        StartCoroutine(ObjectFade.FadeIn(
            objects[5].GetComponent<SpriteRenderer>()));
        yield return new WaitForSeconds(1.0f);
        isPlaced = true;
    }

    public IEnumerator RemoveBread()
    {
        LockAndUnlock();
        yield return new WaitForEndOfFrame();
        GameObject.Find(
            "BreadFrozenInToaster").GetComponent<Pickup>().ExternalTrigger();
        StartCoroutine(ObjectFade.FadeOut(objects[4], 0, 0));
        StartCoroutine(ObjectFade.FadeOut(objects[5], 0, 0));
        yield return new WaitForSeconds(1.0f);
        objects[1].SetActive(false);
        isPlaced = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
    }

    IEnumerator Untrap()
    {
        yield return new WaitForEndOfFrame();
        isTrapped = false;
    }

    IEnumerator Toasting()
    {
        yield return new WaitForEndOfFrame();
        isHeating = true;
        isLocked = true;
        yield return new WaitForSeconds(1.0f);
        objects[1].SetActive(false);
        objects[0].SetActive(true);
        yield return new WaitForSeconds(9.0f);
        isHeating = false;
        isLocked = false;
        hasHeated = true;
        StartCoroutine(ObjectFade.FadeIn(
            objects[2].GetComponent<SpriteRenderer>()));
        StartCoroutine(ObjectFade.FadeIn(
            objects[3].GetComponent<SpriteRenderer>()));
        yield return new WaitForSeconds(1.0f);
        if (zoom.currentView == 1)
            objects[0].GetComponent<BoxCollider2D>().enabled = true;
    }
}
