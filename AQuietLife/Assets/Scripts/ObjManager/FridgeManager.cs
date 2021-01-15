using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{
    public ClockManager clock;
    public InventoryManager inventory;
    public GameManager gameMng;
    public ObjectiveManager objective;
    public CloseUpFridge closeUp;

    public GameObject fridgeGeneral;
    public GameObject fridgeDoor1Open;
    public GameObject fridgeDoor2Open;
    public GameObject fridgeDoor12Open;

    public GameObject returnArrow;
    public GameObject noTextCollidersGeneral;
    public GameObject noTextColliderFridge;
    public GameObject interactionText;
    public GameObject activityText;
    public GameObject fridgeRewindButton;
    public GameObject fridgeButtons;
    public GameObject fridge;
    public GameObject ham;
    public GameObject cheese;
    public GameObject frozenBread;

    public BoxCollider2D[] interactableColliders;

    public Animator doorLeftAnim;
    public Animator doorRightAnim;
    public Animator cheeseAnim;

    private bool isLocked;
    private bool isTrapped;

    public bool rewindApplied;
    public bool hamTaken;
    public bool frozenBreadTaken;
    public bool doorLeftOpen;
    public bool doorRightOpen;
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
                if (doorLeftOpen == true && doorRightOpen == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    fridgeDoor1Open.SetActive(true);
                }
                if (doorLeftOpen == false && doorRightOpen == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    fridgeDoor2Open.SetActive(true);
                }
                if (doorLeftOpen == true && doorRightOpen == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    fridgeDoor12Open.SetActive(true);
                }
                if (doorLeftOpen == false && doorRightOpen == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    fridgeGeneral.SetActive(true);
                }

                fridge.SetActive(false);
                returnArrow.SetActive(false);
                noTextCollidersGeneral.SetActive(true);
                noTextColliderFridge.SetActive(false);
                activityText.SetActive(false);
                fridgeRewindButton.SetActive(false);
                fridgeButtons.SetActive(false);
            }

            if (hit.collider.CompareTag("FridgeDoor1") && gameMng.isLocked == false
                && doorLeftOpen == false)
            {
                doorLeftAnim.SetBool("Open", true);
                doorLeftOpen = true;
                frozenBread.SetActive(true);
                interactionText.SetActive(false);
                LockAndUnlock();
            }

            if (hit.collider.CompareTag("FridgeDoor2") && doorRightOpen == false
                && inventory.hasGlove == true && gameMng.isLocked == false)
            {
                doorRightAnim.SetBool("Open", true);
                inventory.GloveOffInventory();
                ham.SetActive(true);
                isTrapped = false;
                doorRightOpen = true;
                interactionText.SetActive(false);
                LockAndUnlock();

                for (int i = 0; i <= interactableColliders.Length; i++)
                    interactableColliders[i].enabled = true;
            }

            if (hit.collider.CompareTag("FridgeDoor2") && isTrapped == true
                && inventory.hasGlove == false && gameMng.isLocked == false)
            {
                Debug.Log("Game Over");
                gameMng.Die();
            }
            else if (hit.collider.CompareTag("FridgeDoor2") && inventory.hasGlove == false 
                && isTrapped == false)
            {
                doorRightAnim.SetBool("BreadBoxDoorOpen", true);
                doorRightOpen = true;
                interactionText.SetActive(false);
                LockAndUnlock();
            }

            if (hit.collider.CompareTag("Bread1")
                && inventory.hasObject != true && inventory.plateUsed == true)
            {
                frozenBread.SetActive(false);
                inventory.BreadInInventory();
                frozenBreadTaken = true;
                objective.hasBread = true;
                gameMng.pickUpText.SetActive(false);
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
    }
}
