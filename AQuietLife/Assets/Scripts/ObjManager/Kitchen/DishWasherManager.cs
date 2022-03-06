using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DishWasherManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpDishWasher closeUp;
    public ThoughtManager thought;
    [SerializeField] private ObjectSelection select;

    public GameObject[] doors;
    public GameObject[] objects;

    public Animator returnArrow;

    [SerializeField] public AudioSource[] workingAudio;

    public bool isWorking;
    public bool isActive;
    public bool isPointing;

    [SerializeField]private bool isLocked;
    public bool isOpen;

    [SerializeField] float startTime;
    float timer = 0f;

    private void Start()
    {
        isWorking = true;
        isLocked = false;
        closeUp.dishWasher = closeUp.dishWasher.GetComponent<BoxCollider2D>();

        workingAudio[0].Play();

        StartCoroutine(TimeToFinish());
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isActive == true
            && isLocked == false)
        {
            if (select.usingGlove || select.usingStoveCloth)
            {
                select.usingGlove = false;
                select.usingStoveCloth = false;

                gameMng.cursors.ChangeCursor("Point", 0);
                if (isOpen)
                    gameMng.cursors.ChangeCursor("OpenDoor", 4);
                else
                    gameMng.cursors.ChangeCursor("OpenDoor", 3);
            }
            else
            {
                if (isOpen != true)
                {
                    StartCoroutine(OpenDoor());
                }
                else if (isOpen == true)
                {
                    StartCoroutine(CloseDoor());
                }
                LockAndUnlock();
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
        returnArrow.SetTrigger("Hide");
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.9f);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
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

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1);
        isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator OpenDoor()
    {
        closeUp.dishWasher.enabled = false;
        yield return new WaitForEndOfFrame();
        closeUp.dishWasher.size = new Vector2(3.11f, 0.4f);
        closeUp.dishWasher.offset = new Vector2(0, -0.88f);
        StartCoroutine(ObjectFade.FadeOut(doors[0], 0, 0));
        FindObjectOfType<AudioCtrl>().Play("OpenDishWasher");
        yield return new WaitForSeconds(1.0f);
        closeUp.dishWasher.enabled = true;
        for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
            closeUp.zoomableObjs[i].enabled = true;
        isOpen = true;
    }

    IEnumerator CloseDoor()
    {
        closeUp.dishWasher.enabled = false;
        for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
            closeUp.zoomableObjs[i].enabled = false;
        yield return new WaitForEndOfFrame();
        closeUp.dishWasher.size = new Vector2(3.11f, 0.52f);
        closeUp.dishWasher.offset = new Vector2(0, 0.81f);
        StartCoroutine(ObjectFade.FadeIn(
            doors[0].GetComponent<SpriteRenderer>()));
        FindObjectOfType<AudioCtrl>().Play("OpenDishWasher");
        yield return new WaitForSeconds(1.0f);
        closeUp.dishWasher.enabled = true;
        isOpen = false;
    }

    IEnumerator TimeToFinish()
    {
        timer = startTime;
        
        do
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        while (timer > 0);

        if (timer <= 0)
        {
            Debug.Log("Done washing!");
            workingAudio[0].Stop();
            workingAudio[1].Play();
            isWorking = false;
        }
    }
}
