using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MicrowaveManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public ThoughtManager thought;
    public GameManager gameMng;
    //public ObjectiveManager objective;
    public CloseUpMicrowave closeUp;
    public ObjectSelection select;

    public GameObject[] door;
    public GameObject[] objects;
    [SerializeField] private GameObject[] slices;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral; //Activate on final
    //public GameObject noTextColliderMicrowave;
    //public GameObject interactionText;
    //public GameObject activityText;
    //public GameObject microwaveRewindButton;
    //public GameObject microwaveButtons;
    //public GameObject breadObj;

    //public BoxCollider2D[] interactableColliders;
    //public BoxCollider2D structure;
    //public BoxCollider2D bread;

    //public Animator doorAnim; //Activate on final

    public AudioSource microwaveWork;

    [SerializeField]
    private bool isLocked;
    private bool breadPlaced;
    [SerializeField]
    private bool worked;

    [SerializeField] private bool isPointing;

    public bool rewindApplied;
    public bool BreadTaken;
    public bool doorOpen;
    public bool working;
    public bool hasTime;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        hasTime = true;       
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
            && zoom.currentView == 1 && closeUp.isMicrowave == true)
        {
            Vector3 mousePos2 =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

            RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

            if (hit2.collider == null)
            {
                gameMng.cursors.ChangeCursor("OpenDoor", 0);
                gameMng.cursors.ChangeCursor("Inspect", 0);
                if (isPointing)
                {
                    gameMng.cursors.ChangeCursor("Point", 0);
                    isPointing = false;
                }
                gameMng.cursors.ChangeCursor("Grab", 0);
                gameMng.cursors.ChangeCursor("Click", 0);
            }
            else if (hit2.collider.CompareTag("Microwave"))
            {
                if (select.usingGlove || select.usingStoveCloth)
                {
                    gameMng.cursors.ChangeCursor("Point", 1);
                    isPointing = true;
                }
                else
                    gameMng.cursors.ChangeCursor("Click", 1);
            }
            else if (hit2.collider.CompareTag("MicrowaveDoor"))
            {
                if (select.usingGlove || select.usingStoveCloth)
                {
                    gameMng.cursors.ChangeCursor("Point", 1);
                    isPointing = true;
                }
                else
                {
                    if (!doorOpen)
                    {
                        gameMng.cursors.ChangeCursor("Click", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 1);
                    }
                    else
                    {
                        gameMng.cursors.ChangeCursor("Point", 0);
                        gameMng.cursors.ChangeCursor("Grab", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 2);
                    }
                }
            }
            else if (hit2.collider.CompareTag("Nothing"))
            {
                if (select.usingPlateWFrozenBread)
                    gameMng.cursors.ChangeCursor("Point", 1);
                else
                {
                    gameMng.cursors.ChangeCursor("OpenDoor", 0);
                }
            }
        }

        if (!EventSystem.current.IsPointerOverGameObject()
            && Input.GetMouseButtonDown(0) && isLocked == false)
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

            else if (hit.collider.CompareTag("MicrowaveDoor") 
                && gameMng.isLocked == false)
            {
                if (select.usingGlove || select.usingStoveCloth)
                {
                    select.usingGlove = false;
                    select.usingStoveCloth = false;

                    gameMng.cursors.ChangeCursor("Point", 0);
                    gameMng.cursors.ChangeCursor("OpenDoor", 3);
                }
                else
                {
                    if (doorOpen == false && working == false)
                    {
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);

                        OpenAndUnlock();
                        //zoom.InteractionTransition();
                        FindObjectOfType<AudioCtrl>().Play("OpenMWave");

                        if (worked == false)
                        {
                            //if (worked == true)
                            //bread.enabled = true;
                            //doorAnim.SetTrigger("Open");
                            //doorOpen = true;
                            //structure.enabled = true;
                            //doorOpen = true;
                            StartCoroutine(
                                zoom.InteractionTransition(door[1], door[0], 0, 0));
                            door[0].GetComponent<BoxCollider2D>().enabled = false;
                            door[1].GetComponent<BoxCollider2D>().enabled = true;
                            objects[1].SetActive(true);
                        }

                        if (worked == true)
                        {         
                            StartCoroutine(
                                zoom.InteractionTransition(door[1], door[0], 0, 0));
                            door[0].GetComponent<BoxCollider2D>().enabled = false;
                            door[1].GetComponent<BoxCollider2D>().enabled = true;
                            objects[0].GetComponent<BoxCollider2D>().enabled = true;
                        }                    
                    }

                    if (doorOpen == true)
                    {
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);

                        //if (worked == true)
                        //bread.enabled = false;
                        //doorAnim.SetTrigger("Close");
                        //doorAnim.SetBool("PrevOpen", false);
                        //doorOpen = false;
                        CloseAndUnlock();
                        //doorOpen = false;
                        StartCoroutine(
                            zoom.InteractionTransition(door[0], door[1], 0, 0));
                        door[1].GetComponent<BoxCollider2D>().enabled = false;
                        door[0].GetComponent<BoxCollider2D>().enabled = true;
                        objects[1].SetActive(false);
                        objects[2].SetActive(true);
                        //zoom.InteractionTransition();
                        FindObjectOfType<AudioCtrl>().Play("CloseMWave");
                    }
                }
            }

            else if (hit.collider.CompareTag("Nothing") 
                && gameMng.isLocked == false)
            {
                gameMng.cursors.ChangeCursor("Point", 0);

                if (doorOpen == true && select.usingPlateWFrozenBread == true)
                {
                    objects[0].SetActive(true);
                    objects[1].SetActive(false);
                    StartCoroutine(ObjectFade.FadeIn(
                        objects[0].GetComponent<SpriteRenderer>()));
                    StartCoroutine(ObjectFade.FadeIn(
                        slices[0].GetComponent<SpriteRenderer>()));
                    StartCoroutine(ObjectFade.FadeIn(
                        slices[1].GetComponent<SpriteRenderer>()));
                    breadPlaced = true;
                    FindObjectOfType<PlateWFrozenBread>().frozenBreadWPlateUsed = true;
                    FindObjectOfType<AudioCtrl>().Play("PlacePlate");
                }  

                if (doorOpen == true && select.usingFrozenBread == true)
                {
                    thought.ShowThought();
                    thought.text = "Better place this in a plate first.";
                }
            }

            else if (hit.collider.CompareTag("Microwave") && !gameMng.isLocked)
            {
                if (select.usingGlove || select.usingStoveCloth)
                {
                    select.usingGlove = false;
                    select.usingStoveCloth = false;

                    gameMng.cursors.ChangeCursor("Point", 0);
                    gameMng.cursors.ChangeCursor("OpenDoor", 3);
                }
                else if (!doorOpen && breadPlaced && !worked && !working)
                {
                    gameMng.cursors.ChangeCursor("Click", 0);

                    //Start audio
                    //doorAnim.SetBool("Working", true);
                    objects[2].GetComponent<BoxCollider2D>().enabled = false;
                    door[0].SetActive(false);
                    door[2].SetActive(true);
                    working = true;
                    microwaveWork.Play();
                    //StartCoroutine(Unfreeze());
                    gameMng.StartCoroutine(Unfreeze());
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
        else
        {

        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        //returnArrow.SetActive(false);
        //rewindButton.SetActive(false);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableObjs()
    {
        for (int i = 1; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OpenAndUnlock()
    {
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(OpenUnlock());
        }
    }

    public void CloseAndUnlock()
    {
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(CloseUnlock());
        }
    }

    IEnumerator OpenUnlock()
    {
        yield return new WaitForEndOfFrame();
        doorOpen = true;
        yield return new WaitForSeconds(1);
        isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator CloseUnlock()
    {
        yield return new WaitForEndOfFrame();
        doorOpen = false;
        yield return new WaitForSeconds(1);
        isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(30.0f);
        //doorAnim.SetBool("Working", false);
        working = false;
        worked = true;
        door[2].SetActive(false);
        door[0].SetActive(true);
    }
}
