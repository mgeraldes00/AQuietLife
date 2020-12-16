using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBoxManager : MonoBehaviour
{
    public ClockManager clock;
    public InventoryManager inventory;
    public GameManager gameMng;
    public ObjectiveManager objective;
    public CloseUpBreadBox closeUp;

    public GameObject breadBoxGeneral;
    public GameObject breadBoxDoorOpen;
    public GameObject breadBoxNoBreadCenter;
    public GameObject breadBoxNoBreadRight;
    public GameObject breadBoxNoBread;

    public GameObject returnArrow;
    public GameObject noTextCollidersGeneral;
    public GameObject noTextColliderBreadBox;
    public GameObject interactionText;
    public GameObject activityText;
    public GameObject breadBoxButtons;
    public GameObject breadBoxRewindButton;
    public GameObject breadBox;
    public GameObject bread;
    public GameObject breadInteract1;
    public GameObject breadInteract2;

    public BoxCollider2D[] interactableColliders;

    public Animator doorAnim;
    public Animator breadAnim;

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

            if (hit.collider.CompareTag("Nothing"))
            {
                if (doorOpen == true && bread1Taken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    breadBoxDoorOpen.SetActive(true);
                }

                if (doorOpen == true && bread1Taken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    breadBoxNoBreadRight.SetActive(true);
                }

                if (doorOpen == false && bread1Taken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    breadBoxGeneral.SetActive(true);
                }  
                
                breadBox.SetActive(false);
                returnArrow.SetActive(false);
                noTextCollidersGeneral.SetActive(true);
                noTextColliderBreadBox.SetActive(false);
                activityText.SetActive(false);
                breadBoxRewindButton.SetActive(false);
                breadBoxButtons.SetActive(false);
            }

            /*if (hit.collider.CompareTag("BreadBox"))
            {
                breadBoxButtons.SetActive(true);
            }*/

            if (hit.collider.CompareTag("BreadBoxDoor") && doorOpen == false && inventory.hasGlove == true
                && gameMng.isLocked == false)
            {
                doorAnim.SetBool("BreadBoxDoorOpen", true);
                inventory.GloveOffInventory();
                isTrapped = false;
                doorOpen = true;
                interactionText.SetActive(false);
                LockAndUnlock();

                for (int i = 0; i <= interactableColliders.Length; i++)
                    interactableColliders[i].enabled = true;
            }

            if (hit.collider.CompareTag("BreadBoxDoor") 
                && inventory.hasGlove == false && isTrapped == true
                && gameMng.isLocked == false)
            {
                gameMng.Die();
            }
            else if (hit.collider.CompareTag("BreadBoxDoor") && inventory.hasGlove == false && isTrapped == false)
            {
                doorAnim.SetBool("BreadBoxDoorOpen", true);
                doorOpen = true;
                interactionText.SetActive(false);
                LockAndUnlock();
            }

            if (hit.collider.CompareTag("Bread1") 
                && inventory.hasObject != true && inventory.plateUsed == true)
            {
                breadInteract1.SetActive(false);
                inventory.BreadInInventory();
                bread1Taken = true;
                objective.hasBread = true;
                gameMng.pickUpText.SetActive(false);
            }
        }
    }

    public void ButtonBehaviour(int i)
    {
        if (isLocked == false && hasTime == true)
        {
            switch (i)
            {
                case (0):
                default:
                    breadBoxButtons.SetActive(true);
                    breadBoxRewindButton.SetActive(false);
                    activityText.SetActive(true);
                    rewindApplied = true;
                    break;
                case (1):
                    bread.SetActive(true);
                    doorAnim.SetTrigger("DoorPart1");
                    breadAnim.SetTrigger("BreadIdle");
                    clock.Drain();
                    break;
                case (2):
                    bread.SetActive(true);
                    doorAnim.SetTrigger("DoorPart2");
                    breadAnim.SetTrigger("BreadTaken");
                    clock.Drain();
                    break;
                case (3):
                    bread.SetActive(false);
                    doorAnim.SetTrigger("DoorPart3");
                    clock.Drain();
                    break;                
            }
        }
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
