using System.Collections;
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

    public bool rewindApplied;
    public bool knifeTaken;
    public bool doorCenterOpen;

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
        if (zoom.currentView == 1)
        {
            Vector3 mousePos2 =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

            RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

            if (hit2.collider == null)
                FindObjectOfType<PointerManager>().ChangeCursor(1);
            else if (hit2.collider.CompareTag("DrawerDoor1")
                || hit2.collider.CompareTag("DrawerDoor2")
                || hit2.collider.CompareTag("DrawerDoor3"))
            {
                FindObjectOfType<PointerManager>().ChangeCursor(2);
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

                else if (hit.collider.CompareTag("DrawerDoor1") && gameMng.isLocked == false)
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
                        zoom.InteractionTransition();
                        StartCoroutine(ShowDoor(openDoor[0]));
                        zoom.currentView++;
                        FindObjectOfType<PointerManager>().ChangeCursor(1);
                    }
                }

                else if (hit.collider.CompareTag("DrawerDoor3") && gameMng.isLocked == false)
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
                            FindObjectOfType<AudioCtrl>().Play("Disarm");
                            FindObjectOfType<Glove>().gloveUsed = true;
                            StartCoroutine(Untrap(1));
                            //zoom.InteractionTransition();
                        }

                        if (select.usingStoveCloth == true)
                        {
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
                        zoom.InteractionTransition();
                        StartCoroutine(ShowDoor(openDoor[2]));
                        zoom.currentView++;
                        FindObjectOfType<PointerManager>().ChangeCursor(1);
                    }
                }

                else if (hit.collider.CompareTag("DrawerDoor2")
                    && gameMng.isLocked == false)
                {
                    //doors[2].SetActive(false);
                    //doors[3].SetActive(true);
                    //objects[1].SetActive(true);
                    doorCenterOpen = true;
                    drawerName = "ZoomDrawer2";
                    gameMng.returnable = false;
                    LockAndUnlock();
                    zoom.InteractionTransition();
                    //zoom.GetComponent<Animator>().SetTrigger("Return2");
                    StartCoroutine(ShowDoor(openDoor[1]));
                    zoom.currentView++;
                    FindObjectOfType<PointerManager>().ChangeCursor(1);
                }    
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && isLocked == false)
        {
            closeUp.Normalize();
            StartCoroutine(TimeToTransition());
        }
        else if (zoom.currentView == 2 && isLocked == false && gameMng.returnable == false
            && closeUp.isOnDrawer == true)
        {
            LockAndUnlock();
            zoom.InteractionTransition();
            //zoom.GetComponent<Animator>().SetTrigger(drawerName);
            StartCoroutine(HideDoor());
            StartCoroutine(BackZoom());
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
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
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
        yield return new WaitForSeconds(1);
        isLocked = false;
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

        yield return new WaitForSeconds(0.2f);
        door.SetActive(true);
    }

    IEnumerator HideDoor()
    {
        int door = closeUp.currentDrawer;
        yield return new WaitForSeconds(0.2f);
        openDoor[door].SetActive(false);
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
}
