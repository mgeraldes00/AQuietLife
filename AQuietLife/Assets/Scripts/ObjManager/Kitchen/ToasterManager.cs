using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpToaster closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject[] objects;

    public GameObject returnArrow;

    [SerializeField] private Animator toasterAnim;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;
    [SerializeField] private bool hasHeated;

    public bool isPlaced;
    public bool isToaster;

    private void Start()
    {
        isLocked = false;
        isTrapped = true;
    }

    public void CheckTrap()
    {
        if (isTrapped == true)
        {
            if (select.usingNothing == true && isPlaced == true)
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
        else if (isLocked == false && isTrapped == false && hasHeated == false
            && isPlaced == true)
        {
            toasterAnim.SetBool("Toasting", true);
            StartCoroutine(Toasting());
        }
    }

    public void ToasterBehaviour()
    {
        if (isLocked == false)
        {
            if (select.usingFrozenBread == false && isPlaced == false)
            {
                thought.ShowThought();
                thought.text = "Could defrost bread here if I needed to..";
            }
            else if (select.usingFrozenBread == true)
            {
                //zoom.InteractionTransition();
                StartCoroutine(PlaceBread());
                LockAndUnlock();
                FindObjectOfType<FrozenBread>().frozenBreadUsed = true;
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1 && isToaster == true)
        {
            closeUp.Normalize();
            StartCoroutine(TimeToTransition());
        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
    }

    public void LockAndUnlock()
    {
        if (isLocked == false)
        {
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    IEnumerator PlaceBread()
    {
        yield return new WaitForSeconds(0.2f);
        objects[0].SetActive(true);
        isPlaced = true;
        closeUp.toaster.size = new Vector2(0.32f, 0.8f);
        closeUp.toaster.offset = new Vector2(-0.48f, 0);
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

    IEnumerator Toasting()
    {
        yield return new WaitForEndOfFrame();
        isLocked = true;
        yield return new WaitForSeconds(10.0f);
        isLocked = false;
        hasHeated = true;
        toasterAnim.SetBool("Toasting", false);
        if (zoom.currentView == 1)
            for (int i = 0; i < objects.Length; i++)
                objects[i].GetComponent<BoxCollider2D>().enabled = true;
    }
}
