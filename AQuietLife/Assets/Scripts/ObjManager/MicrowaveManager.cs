using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveManager : MonoBehaviour
{
    //public ClockManager clock;
    public CameraCtrl zoom;
    public InventoryManager inventory;
    public ThoughtManager thought;
    public GameManager gameMng;
    public ObjectiveManager objective;
    public CloseUpMicrowave closeUp;

    public Text thoughtText;

    public GameObject microwaveGeneral;
    public GameObject microwaveDoorOpen;
    public GameObject microwaveWorking;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral; //Activate on final
    //public GameObject noTextColliderMicrowave;
    //public GameObject interactionText;
    //public GameObject activityText;
    //public GameObject microwaveRewindButton;
    //public GameObject microwaveButtons;
    public GameObject microwave;
    public GameObject Bread;

    //public BoxCollider2D[] interactableColliders;
    public BoxCollider2D structure;
    public BoxCollider2D bread;

    public Animator doorAnim; //Activate on final

    public AudioSource microwaveWork;

    private bool isLocked;
    //private bool isTrapped;
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
        //isTrapped = true;
        hasTime = true;       
    }

    // Update is called once per frame
    void Update()
    {
        if (working == true)
        {
            doorAnim.SetTrigger("KeepLit");
        }

        if (doorOpen == true)
        {
            doorAnim.SetBool("PrevOpen", true);
        }
        if (doorOpen == false)
        {
            doorAnim.SetBool("PrevOpen", false);
        }

        if (Input.GetMouseButtonDown(0) && isLocked == false)
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("Nothing"))
            {
                if (doorOpen == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    microwaveDoorOpen.SetActive(true);
                }
                if (working == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    microwaveWorking.SetActive(true);
                }
                if (doorOpen == false && working == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    microwaveGeneral.SetActive(true);
                }

                microwave.SetActive(false);
                returnArrow.SetActive(false);
                //noTextCollidersGeneral.SetActive(true);
                //noTextColliderMicrowave.SetActive(false);
                //activityText.SetActive(false);
                //microwaveRewindButton.SetActive(false);
                //microwaveButtons.SetActive(false);
            }

            if (hit.collider.CompareTag("MicrowaveDoor") && gameMng.isLocked == false
                && doorOpen == false && working == false)
            {
                if (worked == true)
                    bread.enabled = true;
                doorAnim.SetTrigger("Open");
                //doorOpen = true;
                structure.enabled = true;
                OpenAndUnlock();
            }

            if (hit.collider.CompareTag("MicrowaveDoor") && gameMng.isLocked == false
                && doorOpen == true)
            {
                if (worked == true)
                    bread.enabled = false;
                doorAnim.SetTrigger("Close");
                //doorAnim.SetBool("PrevOpen", false);
                //doorOpen = false;
                CloseAndUnlock();
            }

            if (hit.collider.CompareTag("Microwave") && gameMng.isLocked == false
                && doorOpen == true && inventory.hasFrozenBreadInPlate == true)
            {
                Bread.SetActive(true);
                breadPlaced = true;
                inventory.PlateBreadUncutOffInventory();
                //doorAnim.SetBool("Open", false);
                //doorOpen = false;
                //LockAndUnlock();
            }

            if (hit.collider.CompareTag("Microwave") && gameMng.isLocked == false
                && doorOpen == true && inventory.hasFrozenBread == true)
            {
                thought.ShowThought();
                thoughtText.text = "Better place this in a plate first.";
            }

            if (hit.collider.CompareTag("Microwave") && gameMng.isLocked == false
                && doorOpen == false && breadPlaced == true && worked == false)
            {
                //Start audio
                doorAnim.SetBool("Working", true);
                working = true;
                worked = true;
                microwaveWork.Play();
                //StartCoroutine(Unfreeze());
                gameMng.StartCoroutine(Unfreeze());
            }

            if (hit.collider.CompareTag("Bread1") && gameMng.isLocked == false
                && worked == true)
            {
                inventory.PlateBreadUncutUnfrozenInInventory();
                Bread.SetActive(false);
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (doorOpen == true)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            microwaveDoorOpen.SetActive(true);
        }
        if (working == true)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            microwaveWorking.SetActive(true);
        }
        if (doorOpen == false && working == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            microwaveGeneral.SetActive(true);
        }

        //microwaveGeneral.SetActive(true);
        microwave.SetActive(false);
        returnArrow.SetActive(false);
        //noTextCollidersGeneral.SetActive(true);
        //noTextColliderMicrowave.SetActive(false);
        //activityText.SetActive(false);
        //microwaveRewindButton.SetActive(false);
        //microwaveButtons.SetActive(false);
        zoom.currentView--;
    }

    public void OpenAndUnlock()
    {
        if (isLocked == false && hasTime == true)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(OpenUnlock());
        }
    }

    public void CloseAndUnlock()
    {
        if (isLocked == false && hasTime == true)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(CloseUnlock());
        }
    }

    public void NoMoreTime()
    {
        hasTime = false;
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
        doorAnim.SetBool("Working", false);
        working = false;
    }
}
