using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

                if (lookingAtFridgeDoor == false)
                {
                    if (hit2.collider == null)
                        FindObjectOfType<PointerManager>().ChangeCursor(1);
                    else if (!hit2.collider.CompareTag("FridgeDoor1")
                        && !hit2.collider.CompareTag("FridgeDoor2"))
                        FindObjectOfType<PointerManager>().ChangeCursor(1);
                    else if (hit2.collider.CompareTag("FridgeDoor1")
                        || hit2.collider.CompareTag("FridgeDoor2")
                        || hit2.collider.gameObject.name == "fridge_shelf")
                    {
                        FindObjectOfType<PointerManager>().ChangeCursor(2);
                    }
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

                else if (hit.collider.CompareTag("FridgeDoor1") && gameMng.isLocked == false
                    && lookAtFridge == true && zoom.currentView == 1)
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

                    if (isTrapped == false)
                    {
                        if (doorLeftOpen == false)
                        {
                            doors[2].SetActive(false);
                            doors[3].SetActive(true);
                            for (int i = 0; i < objects.Length; i++)
                                objects[i].SetActive(true);
                            for (int i = 0; i < moreObjects.Length; i++)
                                moreObjects[i].SetActive(true);
                            zoom.InteractionTransition();
                            openingBottomDoor = true;
                            LockAndUnlock();
                        }

                        if (doorLeftOpen == true)
                        {
                            doors[2].SetActive(true);
                            doors[3].SetActive(false);
                            for (int i = 0; i < objects.Length; i++)
                                objects[i].SetActive(false);
                            for (int i = 0; i < moreObjects.Length; i++)
                                moreObjects[i].SetActive(false);
                            zoom.InteractionTransition();
                            closingBottomDoor = true;
                            LockAndUnlockFromOpen();
                        }
                    }
                }

                else if (hit.collider.CompareTag("FridgeDoor2")
                    && gameMng.isLocked == false && lookAtFridge == false)
                {
                    if (doorRightOpen == false)
                    {
                        doors[0].SetActive(false);
                        doors[1].SetActive(true);
                        for (int i = 0; i < objectsFreezer.Length; i++)
                            objectsFreezer[i].SetActive(true);
                        zoom.InteractionTransition();
                        openingTopDoor = true;
                        LockAndUnlock();
                    }

                    if (doorRightOpen == true)
                    {
                        doors[0].SetActive(true);
                        doors[1].SetActive(false);
                        for (int i = 0; i < objectsFreezer.Length; i++)
                            objectsFreezer[i].SetActive(false);
                        zoom.InteractionTransition();
                        closingTopDoor = true;
                        LockAndUnlockFromOpen();
                    }
                }

                else if (hit.collider.gameObject.name == "fridge_shelf" && zoom.currentView == 1)
                {
                    //arrow.SetActive(false);
                    //arrowZoom.SetActive(true);
                    doors[8].GetComponent<BoxCollider2D>().enabled = false;
                    for (int i = 0; i < moreObjects.Length; i++)
                        moreObjects[i].GetComponent<EdgeCollider2D>().enabled = true;
                    //closeUp.directionArrows[0].SetActive(false);
                    closeUp.dirArrows[0].SetTrigger("Hide");
                    zoom.cameraAnim.SetTrigger("ZoomFridge");
                    thought.KeepThought();
                    thought.text =
                        "Got this OJ pack earlier. Should be unopened, unless someone already drank from it....";
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
        else if (zoom.currentView == 2 && isLocked == false && gameMng.returnable == true)
        {
            //returnArrow.SetTrigger("Show");
            //arrowZoom.SetActive(false);
            doors[8].GetComponent<BoxCollider2D>().enabled = true;
            for (int i = 0; i < moreObjects.Length; i++)
                moreObjects[i].GetComponent<EdgeCollider2D>().enabled = false;
            //closeUp.directionArrows[0].SetActive(true);
            closeUp.dirArrows[0].SetTrigger("Show");
            thought.HideThought();
            StartCoroutine(BackZoom());
        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        //returnArrow.SetActive(false);
        //rewindButton.SetActive(false);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
        onFridge = false;
    }

    public void EnableObjs()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;
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
}
