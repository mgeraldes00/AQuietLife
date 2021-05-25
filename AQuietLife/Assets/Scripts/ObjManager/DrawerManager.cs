using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpDrawers closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject[] doors;
    public GameObject[] objects;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderDrawers;
    //public GameObject interactionText;
    //public GameObject activityText;
    //public GameObject drawerRewindButton;
    //public GameObject drawerButtons;
    //public GameObject drawers;
    //public GameObject spoon;
    //public GameObject knife;
    //public GameObject knifeInteract;

    //public BoxCollider2D[] interactableColliders;

    private bool isLocked;

    public bool rewindApplied;
    public bool knifeTaken;
    public bool doorCenterOpen;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
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

            if (hit.collider == null)
            {
                //Nothing
            }

            else if (hit.collider.CompareTag("DrawerDoor1") && gameMng.isLocked == false)
            {
                if (select.usingNothing == true)
                {
                    Debug.Log("Game Over");
                    gameMng.Die();
                }

                if (select.usingGlove == true)
                {
                    doors[0].SetActive(false);
                    doors[1].SetActive(true);
                    objects[0].SetActive(true);
                    FindObjectOfType<Glove>().gloveUsed = true;
                    zoom.InteractionTransition();
                }

                if (select.usingStoveCloth == true)
                {
                    doors[0].SetActive(false);
                    doors[1].SetActive(true);
                    objects[0].SetActive(true);
                    FindObjectOfType<StoveCloth>().gloveUsed = true;
                    zoom.InteractionTransition();
                }
            }

            else if (hit.collider.CompareTag("DrawerDoor3") && gameMng.isLocked == false)
            {
                if (select.usingNothing == true)
                {
                    Debug.Log("Game Over");
                    gameMng.Die();
                }

                if (select.usingGlove == true)
                {
                    doors[4].SetActive(false);
                    doors[5].SetActive(true);
                    FindObjectOfType<Glove>().gloveUsed = true;
                    zoom.InteractionTransition();
                }

                if (select.usingStoveCloth == true)
                {
                    doors[4].SetActive(false);
                    doors[5].SetActive(true);
                    FindObjectOfType<StoveCloth>().gloveUsed = true;
                    zoom.InteractionTransition();
                }
            }

            else if (hit.collider.CompareTag("DrawerDoor2") && doorCenterOpen == false
                && gameMng.isLocked == false)
            {
                doors[2].SetActive(false);
                doors[3].SetActive(true);
                objects[1].SetActive(true);
                doorCenterOpen = true;
                LockAndUnlock();
                zoom.InteractionTransition();
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
        //returnArrow.SetActive(false);
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
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    public void LockAndUnlockLonger()
    {
        if (isLocked == false)
        {
            //returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(UnlockLonger());
        }
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator UnlockLonger()
    {
        yield return new WaitForSeconds(4);
        isLocked = false;
        //returnArrow.SetActive(true);
    }

    IEnumerator TakeSpoon()
    {
        yield return new WaitForSeconds(2);
    }
}
