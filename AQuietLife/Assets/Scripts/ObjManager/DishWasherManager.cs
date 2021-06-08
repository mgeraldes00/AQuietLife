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

    public GameObject[] doors;
    public GameObject[] objects;

    public Animator returnArrow;

    public bool isWorking;
    public bool isActive;

    [SerializeField]private bool isLocked;
    public bool isOpen;

    [SerializeField] float startTime;
    float timer = 0f;

    private void Start()
    {
        isWorking = true;
        isLocked = false;
        closeUp.dishWasher = closeUp.dishWasher.GetComponent<BoxCollider2D>();

        StartCoroutine(TimeToFinish());
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isActive == true
            && isLocked == false)
        {
            StartCoroutine(OpenDoor());
            if (isOpen != true)
            {
                doors[0].SetActive(false);
                closeUp.dishWasher.size = new Vector2(3.11f, 0.4f);
                closeUp.dishWasher.offset = new Vector2(0, -0.88f);

                for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                    closeUp.zoomableObjs[i].enabled = true;
            }
            else if (isOpen == true)
            {
                doors[0].SetActive(true);
                closeUp.dishWasher.size = new Vector2(3.11f, 2.13f);
                closeUp.dishWasher.offset = new Vector2(0, 0);

                for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                    closeUp.zoomableObjs[i].enabled = false;
            }

            zoom.InteractionTransition();
            LockAndUnlock();
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
        yield return new WaitForEndOfFrame();
        if (isOpen != true)
            isOpen = true;
        else
            isOpen = false;
    }

    IEnumerator TimeToFinish()
    {
        timer = startTime;
        {
            do
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            while (timer > 0);

            if (timer <= 0)
            {
                Debug.Log("Done washing!");
                isWorking = false;
            }
        }
    }
}
