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
    public ObjectSelection select;

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

            if (hit.collider.CompareTag("Bread1") && bread1Taken == false
                && gameMng.isLocked == false && select.usingKnife == true)
            {
                bread1Taken = true;
                objects[3].SetActive(true);
                objects[2].SetActive(true);
                objects[1].GetComponent<BoxCollider2D>().enabled = false;
                FindObjectOfType<Knife>().knifeUsed = true;
                zoom.InteractionTransition();
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
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
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
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        returnArrow.SetActive(true);
        bread.SetActive(false);
    }
}
