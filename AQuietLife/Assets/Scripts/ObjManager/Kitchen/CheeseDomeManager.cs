using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheeseDomeManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public MediaPlayer audioCtrl;
    public AudioSlider audioSlider;
    public Eyelids eyelids;
    public CloseUpCheeseDome closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject[] objects;

    public GameObject returnArrow;
    public GameObject rewindButton;

    public Animator pointer;
    public Animator rewindAnim;

    public GameObject dotAnim;

    public Image waveform;

    public GameObject slider;

    public AudioSource rewindAudio;
    public AudioSource rewindReverseAudio;

    [SerializeField] private bool rewindOnce;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;
    public bool isTaken;
    public bool isPointing;
    public bool isCheese;
    public bool isOpen;
    public bool rewindApplied;

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
                gameMng.cursors.ChangeCursor("OpenDoor", 4);
                FindObjectOfType<AudioCtrl>().Play("Disarm");
                FindObjectOfType<Glove>().gloveUsed = true;
                StartCoroutine(Untrap());
            }

            if (select.usingStoveCloth == true)
            {
                gameMng.cursors.ChangeCursor("Point", 0);
                gameMng.cursors.ChangeCursor("OpenDoor", 4);
                FindObjectOfType<AudioCtrl>().Play("Disarm");
                FindObjectOfType<StoveCloth>().gloveUsed = true;
                StartCoroutine(Untrap());
            }
        }
        else if (isLocked == false && isTrapped == false && isOpen == false)
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
                //zoom.InteractionTransition();
                StartCoroutine(OpenDome());
            }
        }
    }

    public void CheeseBehaviour()
    {
        if (isLocked == false)
        {
            if (select.usingKnife == false && isOpen == true && isTaken == false)
            {
                thought.ShowThought();
                thought.text = "Gonna need a knife for this....";
            }
            else if (select.usingKnife == true && isOpen == true && isTaken == false)
            {
                //zoom.InteractionTransition();
                LockAndUnlock();
                FindObjectOfType<Knife>().knifeUsed = true;
                FindObjectOfType<InventorySimple>().knifeInPossession = false;
                StartCoroutine(TakeCheese());
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
            eyelids.dots = dotAnim;
            StartCoroutine(TimeToOpen());
        }
    }

    public void RewindLock()
    {
        if (isLocked == false && gameMng.isLocked == false)
        {
            isLocked = true;
            gameMng.isLocked = true;
        }
    }

    public void RewindUnlock()
    {
        isLocked = false;
    }

    IEnumerator TimeToOpen()
    {
        yield return new WaitForSeconds(0.1f);
        eyelids.Close(1);
        if (rewindOnce != true)
        {
            yield return new WaitForSeconds(2.0f);
            rewindReverseAudio.Play();
            yield return new WaitForSeconds(1);
            eyelids.pointer.SetTrigger("CabinetRewind");
            eyelids.timer.SetTrigger("Pressed");
            waveform.enabled = true;
            eyelids.Uncover(1);
            rewindOnce = true;
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            eyelids.mediaFunction = true;
            audioCtrl.pressedButtons[2].SetActive(true);
            slider.SetActive(true);
            rewindAudio.Play();
            eyelids.dots.SetActive(true);
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
            waveform.enabled = true;
            eyelids.Uncover(2);
            rewindAudio.Play();
            eyelids.dots.SetActive(true);
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && isCheese == true)
        {
            closeUp.Normalize();
            StartCoroutine(TimeToTransition());
        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForEndOfFrame();
        rewindAnim.GetComponent<Animator>().SetBool("Visible", false);
        objects[2].GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        rewindButton.SetActive(false);
    }

    public void LockAndUnlock()
    {
        if (isLocked == false)
        {
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    IEnumerator OpenDome()
    {
        closeUp.cheeseDome.enabled = false;
        yield return new WaitForEndOfFrame();
        isOpen = true;
        objects[1].SetActive(true);
        FindObjectOfType<AudioCtrl>().Play("Glassdome");
        StartCoroutine(
            zoom.InteractionTransition(objects[1], objects[0], 0, 0));
        yield return new WaitForSeconds(1.0f);
        objects[0].SetActive(false);
        closeUp.cheeseDome.offset = new Vector2(1.68f, -1.01f);
        closeUp.cheeseDome.size = new Vector2(4.87f, 3.95f);
    }

    IEnumerator TakeCheese()
    {
        closeUp.cheeseDome.enabled = false;
        yield return new WaitForEndOfFrame();
        isTaken = true;
        FindObjectOfType<AudioCtrl>().Play("CutCheese");
        objects[2].SetActive(true);
        StartCoroutine(
            ObjectFade.FadeIn(objects[2].GetComponent<SpriteRenderer>()));
        FindObjectOfType<PickupCheese>().PickCheese();
        yield return new WaitForSeconds(1.0f);
        objects[2].GetComponent<BoxCollider2D>().enabled = true;
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
}
