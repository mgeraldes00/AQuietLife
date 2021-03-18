using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerManager : MonoBehaviour
{
    //public ClockManager clock;
    public InventoryManager inventory;
    public GameManager gameMng;
    public ObjectiveManager objective;
    public CloseUpDrawers closeUp;
    public ThoughtManager thought;

    public Text thoughtText;

    //public GameObject drawersGeneral;
    //public GameObject drawersMiddleDoorOpen;
    //public GameObject drawersMiddleDoorOpenNoKnife;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderDrawers;
    //public GameObject interactionText;
    //public GameObject activityText;
    //public GameObject drawerRewindButton;
    //public GameObject drawerButtons;
    public GameObject drawers;
    public GameObject spoon;
    public GameObject knife;
    public GameObject knifeInteract;

    public BoxCollider2D[] interactableColliders;

    public Animator doorLeftAnim;
    public Animator doorRightAnim;
    public Animator doorCenterAnim;
    public Animator spoonAnim;
    public Animator knifeAnim;

    private bool isLocked;

    public bool rewindApplied;
    public bool knifeTaken;
    public bool doorCenterOpen;
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

            /*if (hit.collider.CompareTag("Nothing"))
            {
                if (doorCenterOpen == true && knifeTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    drawersMiddleDoorOpen.SetActive(true);
                }

                if (doorCenterOpen == true && knifeTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    drawersMiddleDoorOpenNoKnife.SetActive(true);
                }

                if (doorCenterOpen == false && knifeTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    drawersGeneral.SetActive(true);
                }   
                
                drawers.SetActive(false);
                returnArrow.SetActive(false);
                noTextCollidersGeneral.SetActive(true);
                noTextColliderDrawers.SetActive(false);
                activityText.SetActive(false);
                drawerRewindButton.SetActive(false);
                drawerButtons.SetActive(false);
            }*/

            /*if (hit.collider.CompareTag("Drawers"))
            {
                drawerButtons.SetActive(true);
            }*/

            if (hit.collider.CompareTag("DrawerDoor1") && gameMng.isLocked == false
                || hit.collider.CompareTag("DrawerDoor3") && gameMng.isLocked == false)
            {
                Debug.Log("Game Over");
                gameMng.Die();
            }

            if (hit.collider.CompareTag("DrawerDoor2") && doorCenterOpen == false
                && gameMng.isLocked == false)
            {
                doorCenterAnim.SetBool("DrawerCenterOpen", true);
                doorCenterOpen = true;
                //interactionText.SetActive(false);
                LockAndUnlock();
                for (int i = 0; i <= interactableColliders.Length; i++)
                    interactableColliders[i].enabled = true;
            }

            if (hit.collider.CompareTag("Knife") && inventory.hasObject != true
                && gameMng.isLocked == false)
            {
                knifeInteract.SetActive(false);
                inventory.KnifeInInventory();
                knifeTaken = true;
                objective.hasKnife = true;
                gameMng.pickUpText.SetActive(false);
            }

            if (hit.collider.CompareTag("CabinetBreach") && gameMng.isLocked == false)
            {
                thought.ShowThought();
                thoughtText.text = "I cleaned this yesterday...";
            }
        }
    }

    public void ButtonBehaviour()
    {
        /*if (isLocked == false && hasTime == true)
        {
            switch (i)
            {
                case (0):
                default:
                    drawerButtons.SetActive(true);
                    drawerRewindButton.SetActive(false);
                    activityText.SetActive(true);
                    rewindApplied = true;
                    break;
                case (1):
                    spoon.SetActive(true);
                    spoonAnim.SetTrigger("Idle");
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart1");
                    StartCoroutine(TakeSpoon());
                    clock.DrainMore();
                    break;
                case (2):
                    //spoon.SetActive(true);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart3");
                    //spoonAnim.SetTrigger("SpoonTaken");
                    clock.DrainMore();
                    break;
                case (3):
                    //spoon.SetActive(false);
                    //knife.SetActive(true);
                    //doorRightAnim.SetTrigger("DrawerRightPart3");
                    doorLeftAnim.SetTrigger("DrawerLeftPart1");
                    doorLeftAnim.SetTrigger("DrawerLeftPart3");
                    clock.DrainMore();
                    break;
                case (4):
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart1Late");
                    clock.DrainMore();
                    break;
                case (5):
                    knife.SetActive(true);
                    knifeAnim.SetTrigger("KnifeTaken");
                    doorRightAnim.SetTrigger("DrawerRightPart3Late");
                    clock.DrainMore();
                    break;
                case (6):                   
                    spoon.SetActive(false);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart1");
                    clock.Drain();
                    break;
                case (7):
                    spoon.SetActive(false);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart2");
                    knifeAnim.SetTrigger("KnifeTaken");
                    clock.Drain();
                    break;
                case (8):
                    spoon.SetActive(false);
                    knife.SetActive(false);
                    doorRightAnim.SetTrigger("DrawerRightPart3");
                    clock.Drain();
                    break;
            }
        }*/

        if (doorCenterOpen == true && knifeTaken == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //drawersMiddleDoorOpen.SetActive(true);
        }

        if (doorCenterOpen == true && knifeTaken == true)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //drawersMiddleDoorOpenNoKnife.SetActive(true);
        }

        if (doorCenterOpen == false && knifeTaken == false)
        {
            for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                closeUp.zoomableObjs[i].enabled = true;
            //drawersGeneral.SetActive(true);
        }

        drawers.SetActive(false);
        returnArrow.SetActive(false);
        //noTextCollidersGeneral.SetActive(true);
        //noTextColliderDrawers.SetActive(false);
        //activityText.SetActive(false);
        //drawerRewindButton.SetActive(false);
        //drawerButtons.SetActive(false);
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

    public void LockAndUnlockLonger()
    {
        if (isLocked == false && hasTime == true)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(UnlockLonger());
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
        spoon.SetActive(false);
        knife.SetActive(false);
    }

    IEnumerator UnlockLonger()
    {
        yield return new WaitForSeconds(4);
        isLocked = false;
        returnArrow.SetActive(true);
        spoon.SetActive(false);
        knife.SetActive(false);
    }

    IEnumerator TakeSpoon()
    {
        yield return new WaitForSeconds(2);
        spoonAnim.SetTrigger("SpoonTaken");
    }
}
