using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class FridgeManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpFridge closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject[] doors;
    public GameObject[] objects;
    public GameObject[] moreObjects;
    public GameObject[] objectsFreezer;

    public AudioSource alarm;

    public Animator returnArrow;
    public Animator returnArrowZoom;
    public GameObject arrow;
    public GameObject arrowZoom;
    //public GameObject fridgeRewindButton;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;
    [SerializeField] private bool lookingAtFridgeDoor;

    [SerializeField] private bool openingTopDoor;
    [SerializeField] private bool openingBottomDoor;
    [SerializeField] private bool closingTopDoor;
    [SerializeField] private bool closingBottomDoor;

    public bool rewindApplied;
    public bool hamTaken;
    public bool frozenBreadTaken;
    public bool doorLeftOpen;
    public bool doorRightOpen;
    public bool lookAtFridge;
    public bool onFridge;
    public bool isPointing;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        isTrapped = true;
        onFridge = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (zoom.currentView >= 1 && closeUp.isOnFridge == true)
            {
                Vector3 mousePos2 =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

                RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

                if (hit2.collider == null)
                {
                    gameMng.cursors.ChangeCursor("Point", 0);
                    isPointing = false;
                }
                else if (hit2.collider.CompareTag("FridgeDoor2") && !lookAtFridge
                    || hit2.collider.CompareTag("FridgeDoor1") && lookAtFridge)
                {
                    if (select.usingGlove || select.usingStoveCloth)
                    {
                        gameMng.cursors.ChangeCursor("Point", 1);
                        isPointing = true;
                    }
                    else
                    {
                        if (hit2.collider.CompareTag("FridgeDoor2") 
                            && doorRightOpen
                            || hit2.collider.CompareTag("FridgeDoor1")
                            && doorLeftOpen)
                                gameMng.cursors.ChangeCursor("OpenDoor", 1);
                        else
                            gameMng.cursors.ChangeCursor("OpenDoor", 2);
                    }
                }
                else if (hit2.collider.gameObject.name == "fridge_shelf")
                {

                }
            }

            if (Input.GetMouseButtonDown(0) && isLocked == false)
            {
                //Debug.Log("Mouse Clicked");
                Vector3 mousePos =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider == null)
                {
                    //Nothing
                }

                else if (hit.collider.CompareTag("FridgeDoor1") 
                    && !gameMng.isLocked && lookAtFridge && zoom.currentView == 1)
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
                            gameMng.cursors.ChangeCursor("OpenDoor", 2);
                            FindObjectOfType<AudioCtrl>().Play("Disarm");
                            FindObjectOfType<Glove>().gloveUsed = true;
                            StartCoroutine(Untrap());
                        }

                        if (select.usingStoveCloth == true)
                        {
                            gameMng.cursors.ChangeCursor("Point", 0);
                            gameMng.cursors.ChangeCursor("OpenDoor", 2);
                            FindObjectOfType<AudioCtrl>().Play("Disarm");
                            FindObjectOfType<StoveCloth>().gloveUsed = true;
                            StartCoroutine(Untrap());
                        }
                    }

                    if (isTrapped == false)
                    {
                        if (doorLeftOpen == false)
                        {
                            if (select.usingGlove || select.usingStoveCloth)
                            {
                                select.usingGlove = false;
                                select.usingStoveCloth = false;

                                gameMng.cursors.ChangeCursor("Point", 0);
                                gameMng.cursors.ChangeCursor("OpenDoor", 2);
                            }
                            else
                            {
                                gameMng.cursors.ChangeCursor("OpenDoor", 0);

                                StartCoroutine(UpdateDoors(0, 2, doors[2], doors[3]));
                                for (int i = 0; i < objects.Length; i++)
                                    objects[i].SetActive(true);
                                for (int i = 0; i < moreObjects.Length; i++)
                                    moreObjects[i].SetActive(true);
                                //zoom.InteractionTransition();
                                openingBottomDoor = true;
                                LockAndUnlock();
                                StartCoroutine(TimeToAlarm());
                                FindObjectOfType<AudioCtrl>().Play("OpenFridge");
                            }
                        }

                        if (doorLeftOpen == true)
                        {
                            gameMng.cursors.ChangeCursor("OpenDoor", 0);

                            StartCoroutine(UpdateDoors(1, 2, doors[2], doors[3]));
                            for (int i = 0; i < objects.Length; i++)
                                objects[i].GetComponent<Collider2D>().enabled = false;
                            for (int i = 0; i < moreObjects.Length; i++)
                                moreObjects[i].GetComponent<EdgeCollider2D>().enabled = false;
                            //zoom.InteractionTransition();
                            closingBottomDoor = true;
                            LockAndUnlockFromOpen();
                            StopCoroutine(TimeToAlarm());
                            alarm.Stop();
                            FindObjectOfType<AudioCtrl>().Play("CloseFridge");
                        }
                    }
                }

                else if (hit.collider.CompareTag("FridgeDoor2")
                    && gameMng.isLocked == false && lookAtFridge == false)
                {
                    if (doorRightOpen == false)
                    {
                        if (select.usingGlove || select.usingStoveCloth)
                        {
                            select.usingGlove = false;
                            select.usingStoveCloth = false;

                            gameMng.cursors.ChangeCursor("Point", 0);
                            gameMng.cursors.ChangeCursor("OpenDoor", 2);
                        }
                        else
                        {
                            gameMng.cursors.ChangeCursor("OpenDoor", 0);

                            StartCoroutine(UpdateDoors(0, 1, doors[0], doors[1]));
                            for (int i = 0; i < objectsFreezer.Length; i++)
                                objectsFreezer[i].SetActive(true);
                            //zoom.InteractionTransition();
                            openingTopDoor = true;
                            LockAndUnlock();
                            FindObjectOfType<AudioCtrl>().Play("OpenFridge");
                        }
                    }

                    if (doorRightOpen == true)
                    {
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);
                        for (int i = 0; i < objectsFreezer.Length; i++)
                            objectsFreezer[i].GetComponent<Collider2D>().enabled = false;
                        StartCoroutine(UpdateDoors(1, 1, doors[0], doors[1]));
                        //zoom.InteractionTransition();
                        closingTopDoor = true;
                        LockAndUnlockFromOpen();
                        FindObjectOfType<AudioCtrl>().Play("CloseFridge");
                    }
                }

                else if (hit.collider.gameObject.name == "fridge_shelf" && zoom.currentView == 1)
                {
                    //arrow.SetActive(false);
                    //arrowZoom.SetActive(true);
                    doors[8].GetComponent<BoxCollider2D>().enabled = false;
                    doors[3].GetComponent<Collider2D>().enabled = false;
                    for (int i = 0; i < objects.Length; i++)
                        objects[i].GetComponent<BoxCollider2D>().enabled = false;
                    for (int i = 0; i < moreObjects.Length; i++)
                        moreObjects[i].GetComponent<EdgeCollider2D>().enabled = true;
                    //closeUp.directionArrows[0].SetActive(false);
                    closeUp.dirArrows[0].SetTrigger("Hide");
                    zoom.cameraAnim.SetTrigger("ZoomFridge");
                    thought.KeepThought();
                    thought.text =
                        "Got this milk pack earlier. Hope no one drank from it yet....";
                    StartCoroutine(ZoomZoom());
                }
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1)
        {
            closeUp.Normalize();
            StartCoroutine(TimeToTransition());
        }
        else if (zoom.currentView == 2 && isLocked == false && closeUp.isOnFridge == true
            && gameMng.returnable == true)
        {
            //returnArrow.SetTrigger("Show");
            //arrowZoom.SetActive(false);
            doors[8].GetComponent<BoxCollider2D>().enabled = true;
            doors[3].GetComponent<Collider2D>().enabled = true;
            for (int i = 0; i < moreObjects.Length; i++)
                moreObjects[i].GetComponent<EdgeCollider2D>().enabled = false;
            for (int i = 0; i < objects.Length; i++)
                objects[i].GetComponent<BoxCollider2D>().enabled = true;
            //closeUp.directionArrows[0].SetActive(true);
            closeUp.dirArrows[0].SetTrigger("Show");
            thought.HideThought();
            //StartCoroutine(BackZoom());
        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        //returnArrow.SetActive(false);
        //rewindButton.SetActive(false);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < objectsFreezer.Length; i++)
            objectsFreezer[i].GetComponent<BoxCollider2D>().enabled = false;
        onFridge = false;
    }

    public void EnableObjs(int type)
    {
        if (type == 1)
        {
            for (int i = 0; i < objects.Length; i++)
                objects[i].GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (type == 2)
        {
            for (int i = 0; i < objectsFreezer.Length; i++)
                objectsFreezer[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    IEnumerator UpdateDoors(
        int state, int type, GameObject currDoor, GameObject oppDoor)
    {
        yield return new WaitForEndOfFrame();
        switch (state)
        {
            case 0:
                currDoor.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(
                    zoom.InteractionTransition(oppDoor, currDoor, 0, 0));
                if (type == 2)
                {
                    StartCoroutine(ObjectFade.FadeIn(
                        doors[8].GetComponent<SpriteRenderer>()));
                    StartCoroutine(ObjectFade.FadeIn(
                        doors[9].GetComponent<SpriteRenderer>()));
                    for (int i = 0; i < moreObjects.Length; i++)
                    {
                        StartCoroutine(ObjectFade.FadeIn(
                            moreObjects[i].GetComponent<SpriteRenderer>()));
                    }
                }
                yield return new WaitForSeconds(1.0f);
                oppDoor.GetComponent<Collider2D>().enabled = true;
                if (type == 1)
                {
                    for (int i = 0; i < objectsFreezer.Length; i++)
                        objectsFreezer[i].GetComponent<Collider2D>().enabled = true;
                }
                else if (type == 2)
                {
                    for (int i = 0; i < objects.Length; i++)
                        objects[i].GetComponent<Collider2D>().enabled = true;
                    doors[8].GetComponent<Collider2D>().enabled = true;
                }
                break;
            case 1:
                oppDoor.GetComponent<Collider2D>().enabled = false;
                if (type == 2)
                    doors[8].GetComponent<Collider2D>().enabled = false;
                StartCoroutine(
                    zoom.InteractionTransition(currDoor, oppDoor, 0, 0));
                if (type == 2)
                {
                    StartCoroutine(ObjectFade.FadeOut(doors[8], 0, 0));
                    StartCoroutine(ObjectFade.FadeOut(doors[9], 0, 0));
                    for (int i = 0; i < moreObjects.Length; i++)
                    {
                        StartCoroutine(ObjectFade.FadeOut(
                            moreObjects[i], 0, 0));
                    }
                }
                yield return new WaitForSeconds(1.0f);
                currDoor.GetComponent<Collider2D>().enabled = true;
                if (type == 1)
                {
                    for (int i = 0; i < objectsFreezer.Length; i++)
                        objectsFreezer[i].SetActive(false);
                }
                else if (type == 2)
                {
                    for (int i = 0; i < moreObjects.Length; i++)
                        moreObjects[i].SetActive(false);
                    for (int i = 0; i < objects.Length; i++)
                        objects[i].SetActive(false);
                }
                break;
        }
    }

    public void LockAndUnlock()
    {
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            lookingAtFridgeDoor = true;
            StartCoroutine(Unlock());
        }
    }

    public void LockAndUnlockFromOpen()
    {
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            lookingAtFridgeDoor = false;
            StartCoroutine(UnlockFromOpen());
        }
    }

    IEnumerator Untrap()
    {
        yield return new WaitForEndOfFrame();
        isTrapped = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(0.5f);
        if (openingTopDoor == true)
            doorRightOpen = true;
        if (openingBottomDoor == true)
            doorLeftOpen = true;
        isLocked = false;
        //returnArrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        openingTopDoor = false;
        openingBottomDoor = false;
    }

    IEnumerator UnlockFromOpen()
    {
        yield return new WaitForSeconds(0.5f);
        if (closingTopDoor == true)
            doorRightOpen = false;
        if (closingBottomDoor == true)
            doorLeftOpen = false;
        isLocked = false;
        //returnArrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        closingTopDoor = false;
        closingBottomDoor = false;
    }

    IEnumerator ShowArrow()
    {
        yield return new WaitForSeconds(1.0f);
        //returnArrow.SetActive(true);
    }

    IEnumerator ZoomZoom()
    {
        yield return new WaitForSeconds(0.1f);
        zoom.currentView++;
    }

    IEnumerator BackZoom()
    {
        yield return new WaitForSeconds(0.1f);
        zoom.currentView--;
    }

    IEnumerator TimeToAlarm()
    {
        yield return new WaitForSeconds(30.0f);
        alarm.Play();
    }
}
