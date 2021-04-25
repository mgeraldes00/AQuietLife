using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetManager : MonoBehaviour
{
    //public ClockManager clock;
    public CameraCtrl zoom;
    public Clock wave;
    public MediaPlayer audioCtrl;
    public AudioSlider audioSlider;
    public InventoryManager inventory;
    public Eyelids eyelids;
    public GloveManager gloveMng;
    public ObjectiveManager objective;
    public GameManager gameMng;
    public CloseUpCabinet closeUp;
    public ThoughtManager thought;

    public Text thoughtText;

    /*public GameObject cabinetGeneral;
    public GameObject cabinetDoor2Open;
    public GameObject cabinetDoor4Open;
    public GameObject cabinetDoor24Open;
    public GameObject cabinetDoor24OpenNoPlate; 
    public GameObject cabinetDoor4OpenNoPlate;
    public GameObject cabinetDoor2OpenGlove;
    public GameObject cabinetDoor24OpenGloveNoPlate;
    public GameObject cabinetDoor24OpenGlovePlate;*/

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderCabinet;
    //public GameObject interactionText;
    //public GameObject activityText;
    public GameObject glove;
    public GameObject cabinetRewindButton;
    //public GameObject cabinetButtons;    
    public GameObject cabinet;
    public GameObject plate;
    public GameObject plateInteract;
    public GameObject glass;

    public Animator door1Anim;
    public Animator door2Anim;
    public Animator door3Anim;
    public Animator door4Anim;
    public Animator glassAnim;
    public Animator plateAnim;

    public Animator pointer;
    public Animator rewindText;

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

    public bool rewindApplied;
    public bool plateTaken;
    public bool door2Open;
    public bool door4Open;
    public bool hasTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isLocked = false;
        hasTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMng.isLocked != true)
            isLocked = false;

        if (Input.GetMouseButtonDown(0) && isLocked == false)
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos = 
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            /*if (hit.collider.CompareTag("Nothing"))
            {
                if (door2Open == true && door4Open == false && gloveMng.gloveTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor2Open.SetActive(true);
                }
                if (door2Open == true && door4Open == false && gloveMng.gloveTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor2OpenGlove.SetActive(true);
                }
                if (door4Open == true && door2Open == false && plateTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor4Open.SetActive(true);
                }
                if (door2Open == true && door4Open == true && plateTaken == false && gloveMng.gloveTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor24Open.SetActive(true);
                }
                if (door2Open == true && door4Open == true && plateTaken == false && gloveMng.gloveTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor24OpenGlovePlate.SetActive(true);
                }
                if (door4Open == true && door2Open == false && plateTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor4OpenNoPlate.SetActive(true);
                }
                if (door2Open == true && door4Open == true && plateTaken == true && gloveMng.gloveTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor24OpenNoPlate.SetActive(true);
                }
                if (door2Open == true && door4Open == true && plateTaken == true && gloveMng.gloveTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor24OpenGloveNoPlate.SetActive(true);
                }
                if (door2Open == false && door4Open == false && plateTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetGeneral.SetActive(true);
                } 
                
                cabinet.SetActive(false);
                returnArrow.SetActive(false);
                noTextCollidersGeneral.SetActive(true);
                noTextColliderCabinet.SetActive(false);
                activityText.SetActive(false);
                cabinetRewindButton.SetActive(false);
                cabinetButtons.SetActive(false);
            }*/

            /*if (hit.collider.CompareTag("Cabinet"))
            {
                //door1Anim.SetTrigger("Door1OpenFull");
                //plate.SetActive(true);
                //glass.SetActive(true);
                //StartCoroutine(CabinetRewind());
                cabinetButtons.SetActive(true);
            }*/

            if (hit.collider.CompareTag("CabinetDoor1") && gameMng.isLocked == false 
                || hit.collider.CompareTag("CabinetDoor3") && gameMng.isLocked == false)
            {
                Debug.Log("Game Over");
                gameMng.Die();
            }

            if (hit.collider.CompareTag("CabinetDoor2") && door2Open == false 
                && gameMng.isLocked == false)
            {
                door2Anim.SetBool("Door2Open", true);
                door2Open = true;
                doorSound.Play();
                glove.SetActive(true);
                //interactionText.SetActive(false);
                LockAndUnlock();
                zoom.InteractionTransition();
            }

            if (hit.collider.CompareTag("CabinetDoor2") && door2Open == true)
            {
                //Do nothing
            }

            if (hit.collider.CompareTag("CabinetDoor4") && door4Open == false 
                && gameMng.isLocked == false)
            {
                door4Anim.SetBool("Door4Open", true);
                door4Open = true;
                plateInteract.SetActive(true);
                doorSound.Play();
                //interactionText.SetActive(false);
                LockAndUnlock();
            }

            if (hit.collider.CompareTag("CabinetDoor4") && door4Open == true)
            {
                //Do nothing
            }

            /*if (hit.collider.CompareTag("Plate") && inventory.hasObject != true 
                && gameMng.isLocked == false)
            {
                plateInteract.SetActive(false);
                inventory.PlateInInventory();
                plateTaken = true;
                objective.hasPlate = true;
                gameMng.pickUpText.SetActive(false);
            }*/

            if (hit.collider.CompareTag("CabinetBreach") && gameMng.isLocked == false)
            {
                thought.ShowThought();
                thoughtText.text = "Someone didn't close this properly...";
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("Base Layer.cabinetOpen", 0, 0.25f);
            }
        }*/
    }

    IEnumerator CabinetRewind()
    {
        yield return new WaitForSeconds(2);
        glassAnim.SetTrigger("GlassTaken");

        yield return new WaitForSeconds(2);
        glass.SetActive(false);

        yield return new WaitForSeconds(2);
        door3Anim.SetTrigger("Door3OpenFull");

        yield return new WaitForSeconds(2);
        plateAnim.SetTrigger("PlateTaken");

        yield return new WaitForSeconds(2);
        plate.SetActive(false);
    }

    public void Rewind()
    {
        if (isLocked == false && hasTime == true && gameMng.isLocked == false)
        {
            eyelids.Close();
            //rewindAudio.Play();
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

        if (door2Open == true && door4Open == false && gloveMng.gloveTaken == true)
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
        }

        StartCoroutine(TimeToTransition());
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        cabinet.SetActive(false);
        returnArrow.SetActive(false);
        //noTextCollidersGeneral.SetActive(true);
        //noTextColliderCabinet.SetActive(false);
        //activityText.SetActive(false);
        cabinetRewindButton.SetActive(false);
        //cabinetButtons.SetActive(false);
    }

    public void LockAndUnlock()
    {
        if (isLocked == false && hasTime == true && gameMng.isLocked == false)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            gameMng.isLocked = true;
            StartCoroutine(Unlock());
        }        
    }

    public void RewindLock()
    {
        if (isLocked == false && hasTime == true && gameMng.isLocked == false)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            gameMng.isLocked = true;
            //StartCoroutine(UnlockRewind());
        }
    }

    public void NoMoreTime()
    {
        hasTime = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        gameMng.isLocked = false;
        returnArrow.SetActive(true);
        plate.SetActive(false);
        glass.SetActive(false);
    }

    IEnumerator UnlockRewind()
    {
        yield return new WaitForSeconds(16);
        isLocked = false;
        gameMng.isLocked = false;
        returnArrow.SetActive(true);
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
        if (rewindOnce != true)
        {
            yield return new WaitForSeconds(3);
            eyelids.pointer.SetTrigger("CabinetRewind");
            rewindReverseAudio.Play();
            eyelids.timer.SetTrigger("Pressed");
            waveformCabinet.enabled = true;
            eyelids.Uncover();
            rewindOnce = true;
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            rewindText.SetBool("Working", true);
            /*for (int i = 0; i < eyelids.ctrlButtons.Length; i++)
                eyelids.ctrlButtons[i].SetActive(true);*/
            eyelids.mediaFunction = true;
            //audioCtrl.audioButtons[2].SetBool("Play", true);
            audioCtrl.pressedButtons[2].SetActive(true);
            slider.SetActive(true);
            //waveformCabinet.enabled = true;
            rewindAudio.Play();
        }
        else if (rewindOnce == true)
        {
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            rewindText.SetBool("Working", true);
            /*for (int i = 0; i < eyelids.ctrlButtons.Length; i++)
                eyelids.ctrlButtons[i].SetActive(true);*/
            eyelids.mediaFunction = true;
            //audioCtrl.audioButtons[2].SetBool("Play", true);
            audioCtrl.pressedButtons[2].SetActive(true);
            slider.SetActive(true);
            waveformCabinet.enabled = true;
            rewindAudio.Play();
        }
    }
}
