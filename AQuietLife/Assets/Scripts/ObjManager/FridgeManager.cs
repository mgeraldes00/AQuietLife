using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpFridge closeUp;
    public ThoughtManager thought;
    public ObjectSelection select;

    public GameObject[] doors;
    public GameObject[] objects;

    public GameObject returnArrow;
    //public GameObject fridgeRewindButton;

    private bool isLocked;
    private bool isTrapped;

    public bool rewindApplied;
    public bool hamTaken;
    public bool frozenBreadTaken;
    public bool doorLeftOpen;
    public bool doorRightOpen;
    public bool lookAtFridge;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        isTrapped = true;
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

            if (hit.collider.CompareTag("FridgeDoor1") && gameMng.isLocked == false
                && lookAtFridge == true)
            {
                if (doorLeftOpen == false)
                {
                    doors[2].SetActive(false);
                    doors[3].SetActive(true);
                    LockAndUnlock(doorLeftOpen);
                }               
            }

            if (hit.collider.CompareTag("FridgeDoor2") && doorRightOpen == false
                && gameMng.isLocked == false && lookAtFridge == false)
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
                
                if (isTrapped == false)
                {
                    doors[0].SetActive(false);
                    doors[1].SetActive(true);
                    LockAndUnlock(doorRightOpen);
                }
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

    public void LockAndUnlock(bool doorOpen)
    {
        if (isLocked == false)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock(doorOpen));
        }
    }

    IEnumerator Unlock(bool doorOpen)
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        doorOpen = true;
        returnArrow.SetActive(true);
    }
}
