using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseDomeManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpCheeseDome closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject[] objects;

    public GameObject returnArrow;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;
    [SerializeField] private bool isTaken;

    public bool isCheese;
    public bool isOpen;

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
        else if (isLocked == false && isTrapped == false && isOpen == false)
        {
            zoom.InteractionTransition();
            StartCoroutine(OpenDome());
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
                zoom.InteractionTransition();
                LockAndUnlock();
                FindObjectOfType<Knife>().knifeUsed = true;
                FindObjectOfType<InventorySimple>().knifeInPossession = false;
                StartCoroutine(TakeCheese());
            }
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
        objects[2].GetComponent<BoxCollider2D>().enabled = false;
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
        yield return new WaitForEndOfFrame();
        isOpen = true;
        yield return new WaitForSeconds(0.2f);
        objects[0].SetActive(false);
        objects[1].SetActive(true);
        closeUp.cheeseDome.offset = new Vector2(1.68f, -1.01f);
        closeUp.cheeseDome.size = new Vector2(4.87f, 3.95f);
    }

    IEnumerator TakeCheese()
    {
        yield return new WaitForEndOfFrame();
        isTaken = true;
        yield return new WaitForSeconds(0.2f);
        objects[2].SetActive(true);
        FindObjectOfType<PickupCheese>().PickCheese();
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
