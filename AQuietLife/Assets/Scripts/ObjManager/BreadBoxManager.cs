using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBoxManager : MonoBehaviour
{
    public ClockManager clock;
    public InventoryManager inventory;
    public GameManager gameOver;
    public CloseUpBreadBox closeUp;

    public GameObject breadBoxGeneral;
    //public GameObject breadBoxDoorOpen;
    //public GameObject breadBoxNoBreadCenter;
    //public GameObject breadBoxNoBreadRight;

    public GameObject returnArrow;
    public GameObject breadBoxButtons;
    public GameObject breadBox;
    public GameObject bread;
    public GameObject breadInteract1;
    public GameObject breadInteract2;

    public BoxCollider2D[] interactableColliders;

    public Animator doorAnim;
    public Animator breadAnim;

    private bool isLocked;
    private bool isTrapped;
    private bool bread1Taken;

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
                for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                    closeUp.zoomableObjs[i].enabled = true;
                breadBoxGeneral.SetActive(true);
                breadBox.SetActive(false);
                returnArrow.SetActive(false);
                breadBoxButtons.SetActive(false);
            }

            if (hit.collider.CompareTag("BreadBox"))
            {
                breadBoxButtons.SetActive(true);
            }

            if (hit.collider.CompareTag("BreadBoxDoor") && inventory.hasGlove == true)
            {
                doorAnim.SetBool("BreadBoxDoorOpen", true);
                inventory.GloveOffInventory();
                isTrapped = false;

                for (int i = 0; i <= interactableColliders.Length; i++)
                    interactableColliders[i].enabled = true;

            }

            if (hit.collider.CompareTag("BreadBoxDoor") && inventory.hasGlove == false && isTrapped == true)
            {
                gameOver.Die();
            }
            else if (hit.collider.CompareTag("BreadBoxDoor") && inventory.hasGlove == false && isTrapped == false)
            {
                doorAnim.SetBool("BreadBoxDoorOpen", true);
            }

            if (hit.collider.CompareTag("Bread1") 
                && inventory.hasObject != true && inventory.plateUsed == true)
            {
                breadInteract1.SetActive(false);
                inventory.BreadInInventory();
                bread1Taken = true;
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
                    bread.SetActive(true);
                    doorAnim.SetTrigger("DoorPart1");
                    clock.Drain();
                    break;
                case (1):
                    bread.SetActive(true);
                    doorAnim.SetTrigger("DoorPart2");
                    breadAnim.SetTrigger("BreadTaken");
                    clock.Drain();
                    break;
                case (2):
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
    }
}
