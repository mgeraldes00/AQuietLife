﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpDrawers closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    //public GameObject[] doors;
    public GameObject[] openDoor;
    public GameObject[] door2;
    //public GameObject[] objects;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderDrawers;
    //public GameObject interactionText;
    //public GameObject activityText;
    //public GameObject drawerRewindButton;
    //public GameObject drawerButtons;
    //public GameObject drawers;
    //public GameObject spoon;
    //public GameObject knife;
    //public GameObject knifeInteract;

    //public BoxCollider2D[] interactableColliders;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool[] isTrapped;
    [SerializeField] private bool lookingAtDrawer;

    public bool rewindApplied;
    public bool knifeTaken;
    public bool doorCenterOpen;
    public bool handlePlaced;
    private bool isPointing;

    [SerializeField] private string drawerName;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        isTrapped[0] = true;
        isTrapped[1] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom.currentView >= 1 && closeUp.isOnDrawer == true)
        {
            Vector3 mousePos2 =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

            RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

            if (lookingAtDrawer == false)
            {
                if (hit2.collider == null)
                {
                    gameMng.cursors.ChangeCursor("Inspect", 0);
                    if (isPointing)
                    {
                        gameMng.cursors.ChangeCursor("Point", 0);
                        isPointing = false;
                    }
                }
                else if (!hit2.collider.CompareTag("DrawerDoor1")
                    && !hit2.collider.CompareTag("DrawerDoor2")
                    && !hit2.collider.CompareTag("DrawerDoor3"))
                {
                    gameMng.cursors.ChangeCursor("Inspect", 0);
                    gameMng.cursors.ChangeCursor("OpenDoor", 0);
                    if (isPointing)
                    {
                        gameMng.cursors.ChangeCursor("Point", 0);
                        isPointing = false;
                    }
                }
                else if (hit2.collider.CompareTag("DrawerDoor1")
                    || hit2.collider.CompareTag("DrawerDoor3"))
                {
                    if (select.usingGlove || select.usingStoveCloth)
                    {
                        gameMng.cursors.ChangeCursor("Point", 1);
                        isPointing = true;
                    }
                    else
                        gameMng.cursors.ChangeCursor("OpenDoor", 3);
                }
                else if (hit2.collider.CompareTag("DrawerDoor2"))
                {
                    if (!handlePlaced)
                    {
                        if (select.usingHandle)
                        {
                            gameMng.cursors.ChangeCursor("Point", 1);
                            isPointing = true;
                        }
                        else
                            gameMng.cursors.ChangeCursor("Inspect", 1);
                    }
                    else
                    {
                        gameMng.cursors.ChangeCursor("OpenDoor", 3);
                    }
                }
            }        
        }    

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (zoom.currentView == 1 && isLocked == false && closeUp.isOnDrawer == true)
            {
                if (hit.collider == null)
                {
                    //Nothing
                }

                else if (
                    hit.collider.CompareTag("DrawerDoor1") && !gameMng.isLocked)
                {
                    if (isTrapped[0] == true)
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

                            //doors[0].SetActive(false);
                            //doors[1].SetActive(true);
                            //objects[0].SetActive(true);
                            FindObjectOfType<AudioCtrl>().Play("Disarm");
                            FindObjectOfType<Glove>().gloveUsed = true;
                            StartCoroutine(Untrap(0));
                            //zoom.InteractionTransition();
                        }

                        if (select.usingStoveCloth == true)
                        {
                            gameMng.cursors.ChangeCursor("Point", 0);
                            gameMng.cursors.ChangeCursor("OpenDoor", 3);

                            //doors[0].SetActive(false);
                            //doors[1].SetActive(true);
                            //objects[0].SetActive(true);
                            FindObjectOfType<AudioCtrl>().Play("Disarm");
                            FindObjectOfType<StoveCloth>().gloveUsed = true;
                            StartCoroutine(Untrap(0));
                            //zoom.InteractionTransition();
                        }
                    }
                    else if (isTrapped[0] == false)
                    {
                        gameMng.returnable = false;
                        LockAndUnlock();
                        //zoom.InteractionTransition();
                        closeUp.drawers[0].enabled = false;
                        StartCoroutine(ShowDoor(openDoor[0]));
                        zoom.currentView++;
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);
                        FindObjectOfType<AudioCtrl>().Play("OpenDrawer");
                    }
                }

                else if (hit.collider.CompareTag("DrawerDoor3") && !gameMng.isLocked)
                {
                    if (isTrapped[1] == true)
                    {
                        if (select.usingNothing == true)
                        {
                            Debug.Log("Game Over");
                            gameMng.Die();
                        }

                        if (select.usingGlove == true)
                        {
                            //doors[4].SetActive(false);
                            //doors[5].SetActive(true);
                            gameMng.cursors.ChangeCursor("Point", 0);
                            gameMng.cursors.ChangeCursor("OpenDoor", 3);
                            FindObjectOfType<AudioCtrl>().Play("Disarm");
                            FindObjectOfType<Glove>().gloveUsed = true;
                            StartCoroutine(Untrap(1));
                            //zoom.InteractionTransition();
                        }

                        if (select.usingStoveCloth == true)
                        {
                            gameMng.cursors.ChangeCursor("Point", 0);
                            gameMng.cursors.ChangeCursor("OpenDoor", 3);
                            //doors[4].SetActive(false);
                            //doors[5].SetActive(true);
                            FindObjectOfType<AudioCtrl>().Play("Disarm");
                            FindObjectOfType<StoveCloth>().gloveUsed = true;
                            StartCoroutine(Untrap(1));
                            //zoom.InteractionTransition();
                        }
                    }
                    else if (isTrapped[1] == false)
                    {
                        gameMng.returnable = false;
                        LockAndUnlock();
                        //zoom.InteractionTransition();
                        closeUp.drawers[2].enabled = false;
                        StartCoroutine(ShowDoor(openDoor[2]));
                        zoom.currentView++;
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);
                        FindObjectOfType<AudioCtrl>().Play("OpenDrawer");
                    }
                }

                else if (hit.collider.CompareTag("DrawerDoor2")
                    && gameMng.isLocked == false)
                {
                    if (handlePlaced == false)
                    {
                        if (select.usingHandle == true)
                        {
                            gameMng.cursors.ChangeCursor("Point", 0);
                            gameMng.cursors.ChangeCursor("OpenDoor", 3);
                            StartCoroutine(Restore());
                            FindObjectOfType<Handle>().handleUsed = true;
                            FindObjectOfType<AudioCtrl>().Play("InsertHandle");
                        }
                        else
                        {
                            thought.ShowThought();
                            thought.text = "The handle fell off... Now where is it?.";
                        }
                    }
                    else
                    {
                        doorCenterOpen = true;
                        drawerName = "ZoomDrawer2";

                        gameMng.returnable = false;
                        LockAndUnlock();
                        //zoom.InteractionTransition();
                        closeUp.drawers[1].enabled = false;
                        StartCoroutine(ShowDoor(openDoor[1]));
                        zoom.currentView++;
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);
                        FindObjectOfType<AudioCtrl>().Play("OpenDrawer");
                    }       
                }    
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && isLocked == false && !gameMng.isLocked
            && closeUp.isOnDrawer)
        {
            closeUp.Normalize();
            StartCoroutine(TimeToTransition());
        }
        else if (zoom.currentView == 2 && !isLocked && !gameMng.returnable
            && closeUp.isOnDrawer == true && !gameMng.isLocked)
        {
            LockAndUnlock();
            //zoom.InteractionTransition();
            //zoom.GetComponent<Animator>().SetTrigger(drawerName);
            StartCoroutine(HideDoor());
            StartCoroutine(BackZoom());
            FindObjectOfType<AudioCtrl>().Play("CloseDrawer");
        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        //returnArrow.SetActive(false);
        //rewindButton.SetActive(false);
    }

    public void LockAndUnlock()
    {
        if (!isLocked)
        {
            //returnArrow.SetActive(false);
            gameMng.isLocked = true;
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    public void LockAndUnlockLonger()
    {
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(UnlockLonger());
        }
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
        gameMng.isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator UnlockLonger()
    {
        yield return new WaitForSeconds(4);
        isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator TakeSpoon()
    {
        yield return new WaitForSeconds(2);
    }

    IEnumerator ShowDoor(GameObject door)
    {
        door.SetActive(true);
        StartCoroutine(ObjectFade.FadeIn(
            door.GetComponent<SpriteRenderer>()));
        foreach(Transform child in door.transform)
        {
            StartCoroutine(ObjectFade.FadeIn(
                child.gameObject.GetComponent<SpriteRenderer>()));
        }
        yield return new WaitForSeconds(1.0f);
        lookingAtDrawer = true;
        foreach(Transform child in door.transform)
        {
            if (child.gameObject.GetComponent<Collider2D>() != null)
            {
                child.gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    IEnumerator HideDoor()
    {
        int door = closeUp.currentDrawer;
        StartCoroutine(ObjectFade.FadeOut(
            openDoor[door], 0, 0));
        foreach(Transform child in openDoor[door].transform)
        {
            StartCoroutine(ObjectFade.FadeOut(
                child.gameObject, 0, 0));
            if (child.gameObject.GetComponent<Collider2D>() != null)
            {
                child.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
        yield return new WaitForSeconds(1.0f);
        openDoor[door].SetActive(false);
        closeUp.drawers[door].enabled = true;
        lookingAtDrawer = false;
    }

    IEnumerator BackZoom()
    {
        yield return new WaitForSeconds(0.1f);
        zoom.currentView--;
    }

    IEnumerator Untrap(int i)
    {
        yield return new WaitForEndOfFrame();
        isTrapped[i] = false;
    }

    IEnumerator Restore()
    {
        yield return new WaitForEndOfFrame();
        handlePlaced = true;
        door2[0].GetComponent<SpriteRenderer>().enabled = true;
        door2[1].GetComponent<SpriteRenderer>().enabled = false;
    }
}
