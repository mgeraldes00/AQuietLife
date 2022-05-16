using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadBoxManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public MediaPlayer audioCtrl;
    public AudioSlider audioSlider;
    public Eyelids eyelids;
    public CloseUpBreadBox closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject[] door;
    public GameObject[] objects;
    [SerializeField] private GameObject[] slices;

    public GameObject returnArrow;
    public GameObject breadBoxRewindButton;

    public Image waveformBreadBox;

    public GameObject slider;

    public AudioSource rewindAudio;
    public AudioSource rewindReverseAudio;

    public Animator rewindAnim;

    [SerializeField]
    private bool isLocked;
    [SerializeField]
    private bool rewindOnce;

    private bool isPointing;
    private bool isTrapped;

    public bool rewindApplied;
    public bool bread1Taken;
    public bool doorOpen;
    public bool hasTime;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        hasTime = true;
        isTrapped = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2 =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

        if (closeUp.isBreadBox == true)
        {
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
            }
            else if (hit2.collider.CompareTag("BreadBoxDoor"))
            {
                if (select.usingGlove || select.usingStoveCloth)
                {
                    gameMng.cursors.ChangeCursor("Point", 1);
                    isPointing = true;
                }  
                else
                    gameMng.cursors.ChangeCursor("OpenDoor", 4);
            }
            else if (hit2.collider.CompareTag("Bread1"))
            {
                if (select.usingKnife)
                {
                    gameMng.cursors.ChangeCursor("Inspect", 0);
                    gameMng.cursors.ChangeCursor("Point", 1);

                }
                else
                {
                    gameMng.cursors.ChangeCursor("Inspect", 0);
                    gameMng.cursors.ChangeCursor("Grab", 1);
                }   
            }
            else if (hit2.collider.CompareTag("Freezer"))
            {
                gameMng.cursors.ChangeCursor("Point", 0);
                gameMng.cursors.ChangeCursor("Grab", 0);
                gameMng.cursors.ChangeCursor("Inspect", 1);
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

            else if (hit.collider.CompareTag("BreadBoxDoor") && doorOpen == false
                && gameMng.isLocked == false)
            {
                if (isTrapped)
                {
                    if (select.usingNothing == true)
                    {
                        Debug.Log("Game Over");
                        gameMng.Die();
                    }

                    if (select.usingGlove == true)
                    {
                        gameMng.cursors.ChangeCursor("Point", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 4);

                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        FindObjectOfType<Glove>().gloveUsed = true;
                        //zoom.InteractionTransition();
                        StartCoroutine(Untrap());
                    }

                    if (select.usingStoveCloth == true)
                    {
                        gameMng.cursors.ChangeCursor("Point", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 4);

                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        FindObjectOfType<StoveCloth>().gloveUsed = true;
                        //zoom.InteractionTransition();
                        StartCoroutine(Untrap());
                    }
                }
                else if (!isTrapped)
                {
                    if (select.usingGlove || select.usingStoveCloth)
                    {
                        select.usingGlove = false;
                        select.usingStoveCloth = false;

                        gameMng.cursors.ChangeCursor("Point", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 4);
                    }
                    else
                    {
                        gameMng.cursors.ChangeCursor("OpenDoor", 0);

                        doorOpen = true;
                        //zoom.InteractionTransition(door[1], door[0], 0, 0);
                        door[0].GetComponent<BoxCollider2D>().enabled = false;
                        StartCoroutine(ObjectFade.FadeOut(door[0], 0, 0));
                        objects[0].SetActive(true);
                        objects[1].SetActive(true);
                    }
                }
            }

            else if (hit.collider.CompareTag("Bread1") && bread1Taken == false
                && gameMng.isLocked == false)
            {
                if (!select.usingKnife)
                {
                    thought.ShowThought();
                    thought.text = "Need something to cut a few slices..";
                }
                else
                {   
                    gameMng.cursors.ChangeCursor("Point", 0);

                    bread1Taken = true;
                    objects[2].SetActive(true);
                    objects[3].SetActive(true);
                    StartCoroutine(CutBread());
                    objects[0].GetComponent<BoxCollider2D>().enabled = false;
                    objects[1].GetComponent<BoxCollider2D>().enabled = false;
                    FindObjectOfType<Knife>().knifeUsed = true;
                    FindObjectOfType<InventorySimple>().knifeInPossession = false;
                    //zoom.InteractionTransition();
                }
            }
        }
    }

    IEnumerator CutBread()
    {
        StartCoroutine(ObjectFade.FadeIn(
            slices[0].GetComponent<SpriteRenderer>()));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ObjectFade.FadeIn(
            slices[1].GetComponent<SpriteRenderer>()));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ObjectFade.FadeIn(
            objects[3].GetComponent<SpriteRenderer>()));
    }

    public void Rewind()
    {
        if (isLocked == false && gameMng.isLocked == false)
        {
            audioCtrl.rewindAudio = rewindAudio;
            audioSlider.rewindAudio = rewindAudio;
            eyelids.timerSmall = rewindAnim;
            StartCoroutine(TimeToOpen());
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && closeUp.isBreadBox == true)
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
        yield return new WaitForEndOfFrame();
        //breadBoxRewindButton.GetComponent<Animator>().SetBool("Visible", false);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        breadBoxRewindButton.SetActive(false);
    }

    public void EnableObjs()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void LockAndUnlock()
    {
        if (isLocked == false && hasTime == true)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    public void RewindLock()
    {
        if (isLocked == false && gameMng.isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            gameMng.isLocked = true;
        }
    }

    public void RewindUnlock()
    {
        isLocked = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator Untrap()
    {
        yield return new WaitForEndOfFrame();
        isTrapped = false;
    }

    IEnumerator TimeToOpen()
    {
        yield return new WaitForSeconds(0.1f);
        eyelids.Close(1);
        if (rewindOnce != true)
        {
            yield return new WaitForSeconds(3);
            eyelids.pointer.SetTrigger("CabinetRewind");
            rewindReverseAudio.Play();
            eyelids.timer.SetTrigger("Pressed");
            waveformBreadBox.enabled = true;
            eyelids.Uncover(1);
            rewindOnce = true;
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            eyelids.mediaFunction = true;
            audioCtrl.pressedButtons[2].SetActive(true);
            slider.SetActive(true);
            rewindAudio.Play();
        }
        else if (rewindOnce == true)
        {
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            eyelids.mediaFunction = true;
            audioCtrl.pressedButtons[2].SetActive(true);
            audioCtrl.MoreRewind();
            slider.SetActive(true);
            waveformBreadBox.enabled = true;
            rewindAudio.Play();
        }
    }
}
