using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBoxManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public ObjectiveManager objective;
    public CloseUpBreadBox closeUp;
    public ThoughtManager thought;

    public GameObject[] door;
    public GameObject[] objects;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderBreadBox;
    //public GameObject interactionText;
    //public GameObject activityText;
    //public GameObject breadBoxButtons;
    //public GameObject breadBoxRewindButton;
    public GameObject bread;
    public GameObject breadInteract1;
    public GameObject breadInteract2;

    private bool isLocked;
    private bool isTrapped;
    
    public bool rewindApplied;
    public bool bread1Taken;
    public bool doorOpen;
    public bool hasTime;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        isTrapped = true;
        hasTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isLocked == false)
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("BreadBoxDoor") && doorOpen == false
                && gameMng.isLocked == false)
            {
                if (!FindObjectOfType<Glove>()
                    || FindObjectOfType<Glove>().gloveSelected == false)
                {
                    Debug.Log("Game Over");
                    gameMng.Die();
                }

                if (FindObjectOfType<Glove>().gloveSelected == true)
                {
                    door[0].SetActive(false);
                    door[1].SetActive(true);
                    objects[0].SetActive(true);
                    objects[1].SetActive(true);
                    FindObjectOfType<Glove>().gloveUsed = true;
                    zoom.InteractionTransition();
                }                
            }

            if (hit.collider.CompareTag("Bread1")
                && gameMng.isLocked == false)
            {
                breadInteract1.SetActive(false);
                bread1Taken = true;
                objective.hasBread = true;
                gameMng.pickUpText.SetActive(false);
            }
        }
    }

    public void ButtonBehaviour()
    {
        closeUp.Normalize();
        StartCoroutine(TimeToTransition());
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        returnArrow.SetActive(false);
        //rewindButton.SetActive(false);
    }

    public void LockAndUnlock()
    {
        if (isLocked == false && hasTime == true)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock());
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
        returnArrow.SetActive(true);
        bread.SetActive(false);
    }
}
