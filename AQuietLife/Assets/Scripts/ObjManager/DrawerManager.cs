using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    public ClockManager clock;
    public InventoryManager inventory;
    public GameManager gameOver;
    public CloseUpDrawers closeUp;

    public GameObject drawersGeneral;
    //public GameObject drawersMiddleDoorOpen;
    //public GameObject drawersMiddleDoorOpenNoKnife;

    public GameObject returnArrow;
    public GameObject drawerButtons;
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
    private bool knifeTaken;

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

            if (hit.collider.CompareTag("Nothing"))
            {
                for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                    closeUp.zoomableObjs[i].enabled = true;
                drawersGeneral.SetActive(true);
                drawers.SetActive(false);
                returnArrow.SetActive(false);
                drawerButtons.SetActive(false);
            }

            if (hit.collider.CompareTag("Drawers"))
            {
                drawerButtons.SetActive(true);
            }

            if (hit.collider.CompareTag("DrawerDoor1") ||
                hit.collider.CompareTag("DrawerDoor3"))
            {
                Debug.Log("Game Over");
                gameOver.Die();
            }

            if (hit.collider.CompareTag("DrawerDoor2"))
            {
                doorCenterAnim.SetBool("DrawerCenterOpen", true);

                for (int i = 0; i <= interactableColliders.Length; i++)
                    interactableColliders[i].enabled = true;
            }

            if (hit.collider.CompareTag("Knife") && inventory.hasObject != true)
            {
                knifeInteract.SetActive(false);
                inventory.KnifeInInventory();
                knifeTaken = true;
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
                    spoon.SetActive(true);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart1");
                    clock.Drain();
                    break;
                case (1):
                    spoon.SetActive(true);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart2");
                    spoonAnim.SetTrigger("SpoonTaken");
                    clock.Drain();
                    break;
                case (2):
                    spoon.SetActive(false);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart3");
                    clock.Drain();
                    break;
                case (3):
                    doorLeftAnim.SetTrigger("DrawerLeftPart1");
                    clock.Drain();
                    break;
                case (4):
                    doorLeftAnim.SetTrigger("DrawerLeftPart3");
                    clock.Drain();
                    break;
                case (5):                   
                    spoon.SetActive(false);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart1");
                    clock.Drain();
                    break;
                case (6):
                    spoon.SetActive(false);
                    knife.SetActive(true);
                    doorRightAnim.SetTrigger("DrawerRightPart2");
                    knifeAnim.SetTrigger("KnifeTaken");
                    clock.Drain();
                    break;
                case (7):
                    spoon.SetActive(false);
                    knife.SetActive(false);
                    doorRightAnim.SetTrigger("DrawerRightPart3");
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
