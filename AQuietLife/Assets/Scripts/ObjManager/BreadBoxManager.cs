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

    public bool rewindApplied;
    public bool bread1Taken;
    public bool doorOpen;
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
        Vector3 mousePos2 =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

        if (closeUp.isBreadBox == true)
        {
            if (hit2.collider == null)
                FindObjectOfType<PointerManager>().ChangeCursor(1);
            else if (hit2.collider.CompareTag("BreadBoxDoor") || hit2.collider.CompareTag("Bread1"))
            {
                FindObjectOfType<PointerManager>().ChangeCursor(2);
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
                if (select.usingNothing == true)
                {
                    Debug.Log("Game Over");
                    gameMng.Die();
                }

                if (select.usingGlove == true)
                {
                    door[0].SetActive(false);
                    door[1].SetActive(true);
                    objects[0].SetActive(true);
                    objects[1].SetActive(true);
                    FindObjectOfType<Glove>().gloveUsed = true;
                    zoom.InteractionTransition();
                }

                if (select.usingStoveCloth == true)
                {
                    door[0].SetActive(false);
                    door[1].SetActive(true);
                    objects[0].SetActive(true);
                    objects[1].SetActive(true);
                    FindObjectOfType<StoveCloth>().gloveUsed = true;
                    zoom.InteractionTransition();
                }
            }

            else if (hit.collider.CompareTag("Bread1") && bread1Taken == false
                && gameMng.isLocked == false && select.usingKnife == true)
            {
                bread1Taken = true;
                objects[3].SetActive(true);
                objects[2].SetActive(true);
                objects[1].GetComponent<BoxCollider2D>().enabled = false;
                FindObjectOfType<Knife>().knifeUsed = true;
                FindObjectOfType<InventorySimple>().knifeInPossession = false;
                zoom.InteractionTransition();
            }
        }
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
        yield return new WaitForEndOfFrame();
        breadBoxRewindButton.GetComponent<Animator>().SetBool("Visible", false);
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

    IEnumerator TimeToOpen()
    {
        yield return new WaitForSeconds(0.1f);
        eyelids.Close();
        if (rewindOnce != true)
        {
            yield return new WaitForSeconds(3);
            eyelids.pointer.SetTrigger("CabinetRewind");
            rewindReverseAudio.Play();
            eyelids.timer.SetTrigger("Pressed");
            waveformBreadBox.enabled = true;
            eyelids.Uncover();
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
