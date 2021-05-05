using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public ThoughtManager thought;
    public GameManager gameMng;
    //public ObjectiveManager objective;
    public CloseUpMicrowave closeUp;
    public ObjectSelection select;

    public GameObject[] door;
    public GameObject[] objects;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral; //Activate on final
    //public GameObject noTextColliderMicrowave;
    //public GameObject interactionText;
    //public GameObject activityText;
    //public GameObject microwaveRewindButton;
    //public GameObject microwaveButtons;
    //public GameObject breadObj;

    //public BoxCollider2D[] interactableColliders;
    //public BoxCollider2D structure;
    //public BoxCollider2D bread;

    //public Animator doorAnim; //Activate on final

    public AudioSource microwaveWork;

    [SerializeField]
    private bool isLocked;
    private bool breadPlaced;
    [SerializeField]
    private bool worked;

    public bool rewindApplied;
    public bool BreadTaken;
    public bool doorOpen;
    public bool working;
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
        if (Input.GetMouseButtonDown(0) && isLocked == false)
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("MicrowaveDoor") && gameMng.isLocked == false
                && doorOpen == false && working == false)
            {
                //if (worked == true)
                    //bread.enabled = true;
                //doorAnim.SetTrigger("Open");
                //doorOpen = true;
                //structure.enabled = true;
                OpenAndUnlock();
                //doorOpen = true;
                door[0].SetActive(false);
                door[1].SetActive(true);
                objects[1].SetActive(true);
                zoom.InteractionTransition();
            }

            if (hit.collider.CompareTag("MicrowaveDoor") && gameMng.isLocked == false
                && doorOpen == true)
            {
                //if (worked == true)
                //bread.enabled = false;
                //doorAnim.SetTrigger("Close");
                //doorAnim.SetBool("PrevOpen", false);
                //doorOpen = false;
                CloseAndUnlock();
                //doorOpen = false;
                door[0].SetActive(true);
                door[1].SetActive(false);
                objects[1].SetActive(false);
                objects[2].SetActive(true);
                zoom.InteractionTransition();
            }

            if (hit.collider.CompareTag("Nothing") && gameMng.isLocked == false
                && doorOpen == true && select.usingPlateWFrozenBread)
            {
                objects[0].SetActive(true);
                objects[1].SetActive(false);
                breadPlaced = true;
                FindObjectOfType<PlateWFrozenBread>().frozenBreadWPlateUsed = true;
            }

            if (hit.collider.CompareTag("Nothing") && gameMng.isLocked == false
                && doorOpen == true)
            {
                thought.ShowThought();
                thought.text = "Better place this in a plate first.";
            }

            if (hit.collider.CompareTag("Microwave") && gameMng.isLocked == false
                && doorOpen == false && breadPlaced == true && worked == false)
            {
                //Start audio
                //doorAnim.SetBool("Working", true);
                door[0].SetActive(false);
                door[2].SetActive(true);
                working = true;
                worked = true;
                microwaveWork.Play();
                //StartCoroutine(Unfreeze());
                gameMng.StartCoroutine(Unfreeze());
            }

            if (hit.collider.CompareTag("Bread1") && gameMng.isLocked == false
                && worked == true)
            {
                //breadObj.SetActive(false);
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

    public void OpenAndUnlock()
    {
        if (isLocked == false)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(OpenUnlock());
        }
    }

    public void CloseAndUnlock()
    {
        if (isLocked == false)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(CloseUnlock());
        }
    }

    IEnumerator OpenUnlock()
    {
        yield return new WaitForSeconds(1);
        isLocked = false;
        returnArrow.SetActive(true);
        doorOpen = true;
    }

    IEnumerator CloseUnlock()
    {
        yield return new WaitForSeconds(1);
        isLocked = false;
        returnArrow.SetActive(true);
        doorOpen = false;
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(10);
        //doorAnim.SetBool("Working", false);
        working = false;
        door[2].SetActive(false);
        door[0].SetActive(true);
    }
}
