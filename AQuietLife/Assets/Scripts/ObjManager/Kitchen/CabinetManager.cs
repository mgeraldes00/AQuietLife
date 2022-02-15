using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CabinetManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public Clock wave;
    public MediaPlayer audioCtrl;
    public AudioSlider audioSlider;
    public Eyelids eyelids;
    public ObjectiveManager objective;
    public GameManager gameMng;
    public CloseUpCabinet closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    /*public GameObject cabinetGeneral;
    public GameObject cabinetDoor2Open;
    public GameObject cabinetDoor4Open;
    public GameObject cabinetDoor24Open;
    public GameObject cabinetDoor24OpenNoPlate; 
    public GameObject cabinetDoor4OpenNoPlate;
    public GameObject cabinetDoor2OpenGlove;
    public GameObject cabinetDoor24OpenGloveNoPlate;
    public GameObject cabinetDoor24OpenGlovePlate;*/

    public GameObject[] doors;
    public GameObject[] objects;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderCabinet;
    //public GameObject interactionText;
    //public GameObject activityText;
    public GameObject glove;
    public GameObject cabinetRewindButton;
    public GameObject dotAnim;
    //public GameObject cabinetButtons;    
    //public GameObject cabinet;
    //public GameObject plate;
    //public GameObject plateInteract;
    //public GameObject glass;

    public Animator pointer;
    public Animator rewindAnim;

    private Animator anim;

    public Image waveform;
    public Image waveformCabinet;

    public GameObject slider;

    public AudioSource rewindAudio;
    //public AudioClip rewind;
    public AudioSource rewindReverseAudio;
    public AudioSource doorSound;

    private Color red = new Color(1, 0, 0, 1);

    [SerializeField]
    private bool isLocked;
    [SerializeField]
    private bool rewindOnce;

    private bool isPointing;

    public bool rewindApplied;
    public bool plateTaken;
    public bool door2Open;
    public bool door4Open;
    public bool hasTime;

    private bool[] isTrapped;

    public bool isOnCabinet;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isLocked = false;
        hasTime = true;

        isTrapped = new bool[4];
        isTrapped[1] = true;
        isTrapped[3] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMng.isLocked != true)
            isLocked = false;

        Vector3 mousePos2 =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

        if (closeUp.isCabinet == true)
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
            }
            else
            {
                if (hit2.collider.CompareTag("CabinetDoor1")
                    || hit2.collider.CompareTag("CabinetDoor3"))
                {
                    if (select.usingGlove || select.usingStoveCloth)
                    {
                        gameMng.cursors.ChangeCursor("Point", 1);
                        isPointing = true;
                    }  
                    else
                        gameMng.cursors.ChangeCursor("OpenDoor", 1);
                }
                if (hit2.collider.CompareTag("CabinetDoor2")
                    || hit2.collider.CompareTag("CabinetDoor4"))
                {
                    if (select.usingGlove || select.usingStoveCloth)
                    {
                        gameMng.cursors.ChangeCursor("Point", 1);
                        isPointing = true;
                    }
                    else
                        gameMng.cursors.ChangeCursor("OpenDoor", 2);
                }
                if (hit2.collider.CompareTag("CabinetBreach"))
                {
                    gameMng.cursors.ChangeCursor("Inspect", 1);
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

            else if (hit.collider.CompareTag("CabinetDoor1") && gameMng.isLocked == false)
            {
                if (isTrapped[1])
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
                        FindObjectOfType<Glove>().gloveUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        StartCoroutine(Untrap(1));
                    }

                    if (select.usingStoveCloth == true)
                    {
                        gameMng.cursors.ChangeCursor("Point", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 1);
                        FindObjectOfType<StoveCloth>().gloveUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        StartCoroutine(Untrap(1));
                    }
                }
                else
                {
                    doors[2].GetComponent<BoxCollider2D>().enabled = false;
                    objects[1].SetActive(true);
                    zoom.InteractionTransition(doors[3], doors[2], 0);
                    FindObjectOfType<AudioCtrl>().Play("OpenCabinetDoor");
                }
            }

            else if (hit.collider.CompareTag("CabinetDoor3") && gameMng.isLocked == false)
            {
                if (isTrapped[3])
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
                        FindObjectOfType<Glove>().gloveUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        StartCoroutine(Untrap(3));
                    }

                    if (select.usingStoveCloth == true)
                    {
                        gameMng.cursors.ChangeCursor("Point", 0);
                        gameMng.cursors.ChangeCursor("OpenDoor", 1);
                        FindObjectOfType<StoveCloth>().gloveUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        StartCoroutine(Untrap(3));
                    }
                }
                else
                {
                    doors[4].GetComponent<BoxCollider2D>().enabled = false;
                    zoom.InteractionTransition(doors[5], doors[4], 0);
                    FindObjectOfType<AudioCtrl>().Play("OpenCabinetDoor");
                }
            }

            else if (hit.collider.CompareTag("CabinetDoor2") && gameMng.isLocked == false)
            {
                if (door2Open == false)
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
                        //door2Anim.SetBool("Door2Open", true);
                        doors[0].GetComponent<BoxCollider2D>().enabled = false;
                        door2Open = true;
                        //doorSound.Play();
                        //glove.SetActive(true);
                        LockAndUnlock();
                        zoom.InteractionTransition(doors[1], doors[0], 0);
                        FindObjectOfType<AudioCtrl>().Play("OpenCabinetDoor");
                    }
                }

                if (door2Open == true)
                {
                    //Do nothing
                }
            } 

            else if (hit.collider.CompareTag("CabinetDoor4") && door4Open == false 
                && gameMng.isLocked == false)
            {
                if (door4Open == false)
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
                        //door4Anim.SetBool("Door4Open", true);
                        doors[6].GetComponent<BoxCollider2D>().enabled = false;
                        objects[0].SetActive(true);
                        door4Open = true;
                        //plateInteract.SetActive(true);
                        //doorSound.Play();
                        //interactionText.SetActive(false);
                        LockAndUnlock();
                        zoom.InteractionTransition(doors[7], doors[6], 0);
                        FindObjectOfType<AudioCtrl>().Play("OpenCabinetDoor");
                    }
                }

                if (door4Open == true)
                {
                    // Do nothing
                }
            }

            else if (hit.collider.CompareTag("CabinetBreach") && gameMng.isLocked == false)
            {
                thought.ShowThought();
                thought.text = "Someone didn't close this properly...";
            }
        }
    }

    public void Rewind()
    {
        if (isLocked == false && gameMng.isLocked == false)
        {     
            //rewindAudio.Play();
            audioCtrl.rewindAudio = rewindAudio;
            audioSlider.rewindAudio = rewindAudio;
            eyelids.timerSmall = rewindAnim;
            eyelids.dots = dotAnim;
            StartCoroutine(TimeToOpen());
            //cabinetButtons.SetActive(true);
            //cabinetRewindButton.SetActive(false);
            //activityText.SetActive(true);
            //rewindApplied = true;
        }
    }

    public void ButtonBehaviour()
    {
        /*if (isLocked == false && hasTime == true && gameMng.isLocked == false)
        {
            switch (i)
            {
                case (0):
                default:
                    eyelids.Close();
                    //rewindAudio.Play();
                    StartCoroutine(TimeToOpen());
                    //cabinetButtons.SetActive(true);
                    //cabinetRewindButton.SetActive(false);
                    //activityText.SetActive(true);
                    //rewindApplied = true;
                    break;
                case (1):
                    glass.SetActive(true);
                    door1Anim.SetTrigger("Door1Part1");
                    doorSound.Play();
                    clock.Drain();
                    break;
                case (2):
                    glass.SetActive(true);
                    door1Anim.SetTrigger("Door1Part2");
                    glassAnim.SetTrigger("GlassTaken");
                    clock.Drain();
                    break;
                case (3):
                    glass.SetActive(false);
                    door1Anim.SetTrigger("Door1Part3");
                    clock.Drain();
                    break;
                case (4):
                    plate.SetActive(true);
                    door3Anim.SetTrigger("Door3Part1");
                    doorSound.Play();
                    clock.Drain();
                    break;
                case (5):
                    plate.SetActive(true);
                    door3Anim.SetTrigger("Door3Part2");
                    plateAnim.SetTrigger("PlateTaken");
                    clock.Drain();
                    break;
                case (6):
                    plate.SetActive(false);
                    door3Anim.SetTrigger("Door3Part3");
                    clock.Drain();
                    break;
            }
        }*/

        /*if (door2Open == true && door4Open == false && gloveMng.gloveTaken == true)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor2Open.SetActive(true);
        }
        if (door2Open == true && door4Open == false && gloveMng.gloveTaken == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor2OpenGlove.SetActive(true);
        }
        if (door4Open == true && door2Open == false && plateTaken == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor4Open.SetActive(true);
        }
        if (door2Open == true && door4Open == true && plateTaken == false && gloveMng.gloveTaken == true)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor24Open.SetActive(true);
        }
        if (door2Open == true && door4Open == true && plateTaken == false && gloveMng.gloveTaken == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor24OpenGlovePlate.SetActive(true);
        }
        if (door4Open == true && door2Open == false && plateTaken == true)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor4OpenNoPlate.SetActive(true);
        }
        if (door2Open == true && door4Open == true && plateTaken == true && gloveMng.gloveTaken == true)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor24OpenNoPlate.SetActive(true);
        }
        if (door2Open == true && door4Open == true && plateTaken == true && gloveMng.gloveTaken == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //cabinetDoor24OpenGloveNoPlate.SetActive(true);
        }
        if (door2Open == false && door4Open == false && plateTaken == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //abinetGeneral.SetActive(true);
        }*/

        if (zoom.currentView == 1 && isOnCabinet == true)
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
        //returnArrow.SetActive(false);
        cabinetRewindButton.GetComponent<Animator>().SetBool("Visible", false);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        cabinetRewindButton.SetActive(false);
        isOnCabinet = false;
    }

    public void EnableObjs()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void LockAndUnlock()
    {
        if (isLocked == false && hasTime == true && gameMng.isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            gameMng.isLocked = true;
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
            //StartCoroutine(UnlockRewind());
        }
    }

    public void NoMoreTime()
    {
        hasTime = false;
    }

    IEnumerator Untrap(int i)
    {
        yield return new WaitForEndOfFrame();
        isTrapped[i] = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        gameMng.isLocked = false;
        //returnArrow.SetActive(true);
        //plate.SetActive(false);
        //glass.SetActive(false);
    }

    IEnumerator UnlockRewind()
    {
        yield return new WaitForSeconds(16);
        isLocked = false;
        gameMng.isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator TimeToOpen()
    {
        /*yield return new WaitForSeconds(2);
        pointer.SetTrigger("CabinetRewind");
        rewindReverseAudio.Play();
        yield return new WaitForSeconds(2);
        rewindAudio.Play();
        slider.SetActive(true);
        waveformCabinet.enabled = true;
        //waveform.enabled = true;
        //Texture2D tex = wave.PaintWaveformSpectrum(rewind, 0.5f, 500, 200, red);
        //waveform.overrideSprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        yield return new WaitForSeconds(11);
        eyelids.Open();
        slider.SetActive(false);
        waveformCabinet.enabled = false;
        //waveform.enabled = false;*/
        yield return new WaitForSeconds(0.1f);
        eyelids.Close(3);
        if (rewindOnce != true)
        {
            yield return new WaitForSeconds(2.0f);
            rewindReverseAudio.Play();
            yield return new WaitForSeconds(1);
            eyelids.pointer.SetTrigger("CabinetRewind"); 
            eyelids.timer.SetTrigger("Pressed");
            waveformCabinet.enabled = true;
            eyelids.Uncover(1);
            rewindOnce = true;
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            /*for (int i = 0; i < eyelids.ctrlButtons.Length; i++)
                eyelids.ctrlButtons[i].SetActive(true);*/
            eyelids.mediaFunction = true;
            //audioCtrl.audioButtons[2].SetBool("Play", true);
            audioCtrl.pressedButtons[2].SetActive(true);
            slider.SetActive(true);
            //waveformCabinet.enabled = true;
            rewindAudio.Play();
            eyelids.dots.SetActive(true);
        }
        else if (rewindOnce == true)
        {
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            /*for (int i = 0; i < eyelids.ctrlButtons.Length; i++)
                eyelids.ctrlButtons[i].SetActive(true);*/
            eyelids.mediaFunction = true;
            //audioCtrl.audioButtons[2].SetBool("Play", true);
            audioCtrl.pressedButtons[2].SetActive(true);
            audioCtrl.MoreRewind();
            slider.SetActive(true);
            waveformCabinet.enabled = true;
            rewindAudio.Play();
            eyelids.dots.SetActive(true);
        }
    }
}
