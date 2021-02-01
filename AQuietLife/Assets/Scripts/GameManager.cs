using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public InventoryManager inventory;
    public ObjectiveManager objective;
    public CabinetManager cabinet;
    public BreadBoxManager breadBox;
    public DrawerManager drawers;
    public FridgeManager fridge;
    public MicrowaveManager microwave;
    public TableManager table;
    public ClockManager clock;

    public GameObject[] context;
    public GameObject[] contextButtons;

    public GameObject timeText;
    public GameObject inspectionText;
    public GameObject tableInteractionText;
    public GameObject interactionText;
    public GameObject pickUpText;
    public GameObject pickUpTextFinal;
    public GameObject exitText;
    public GameObject returnArrow;
    public GameObject startTutorial;
    public GameObject gloveTutorial;
    public GameObject objectTutorial;
    public GameObject finalTutorial;
    public GameObject noTextCollidersGeneral;

    public Animator kitchenClock;

    public bool isLocked;
    public bool firstObject;
    public bool firstGlove;
    private bool allObjectives;

    public bool breadHeated;

    public int numOfIngredients;
    public int glovesUsed;

    // Start is called before the first frame update
    void Start()
    {        
        isLocked = true;
        numOfIngredients = 0;
        glovesUsed = 0;
        startTutorial.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timeText.GetComponent<Text>().text =
            "" + clock.GetComponent<ClockManager>().pTime;

        Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider.CompareTag("Table") || hit.collider.CompareTag("Drawers") 
            || hit.collider.CompareTag("Cabinet") || hit.collider.CompareTag("BreadBox")
            || hit.collider.CompareTag("Fridge") || hit.collider.CompareTag("Microwave"))
        {
            Debug.Log("Object");
            inspectionText.SetActive(true);          
        }

        if (hit.collider.CompareTag("CabinetBreach"))
        {
            inspectionText.SetActive(true);
        }

        if (hit.collider.CompareTag("CabinetDoor1") 
            || hit.collider.CompareTag("CabinetDoor2") && cabinet.door2Open == false
            || hit.collider.CompareTag("CabinetDoor3") 
            || hit.collider.CompareTag("CabinetDoor4") && cabinet.door4Open == false
            || hit.collider.CompareTag("BreadBoxDoor") && breadBox.doorOpen == false
            || hit.collider.CompareTag("DrawerDoor1")
            || hit.collider.CompareTag("DrawerDoor2") && drawers.doorCenterOpen == false
            || hit.collider.CompareTag("DrawerDoor3")
            || hit.collider.CompareTag("FridgeDoor1") && fridge.doorLeftOpen == false
            || hit.collider.CompareTag("FridgeDoor2") && fridge.doorRightOpen == false)
        {
            interactionText.SetActive(true);
        }

        if (hit.collider.CompareTag("Plate") && inventory.plateUsed == false
            || hit.collider.CompareTag("Bread1") && inventory.plateUsed == true
            || hit.collider.CompareTag("Knife"))
        {
            pickUpText.SetActive(true);
        }

        if (hit.collider.CompareTag("Plate") && objective.part1Complete == true)
        {
            pickUpTextFinal.SetActive(true);
        }

        if (hit.collider.CompareTag("TableClose")
            && cabinet.plateTaken == true && table.plateOnTable == false
            || hit.collider.CompareTag("TableClose")
            && breadBox.bread1Taken == true && table.breadOnTable == false
            || hit.collider.CompareTag("TableClose")
            && drawers.knifeTaken == true && table.knifeOnTable == false)
        {
            tableInteractionText.SetActive(true);
        }

        if (hit.collider.CompareTag("Door") && inventory.hasPlateWBread == true)
        {
            exitText.SetActive(true);
        }

        if (hit.collider.CompareTag("NoTag"))
        {
            inspectionText.SetActive(false);
            interactionText.SetActive(false);
            pickUpText.SetActive(false);
            pickUpTextFinal.SetActive(false);
            tableInteractionText.SetActive(false);
            exitText.SetActive(false);
        }

        /*if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Table") ||
            Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Drawers") ||
            Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Cabinet") ||
            Input.GetMouseButtonDown(0) && hit.collider.CompareTag("BreadBox"))
        {
            inspectionText.SetActive(false);
            noTextCollidersGeneral.SetActive(false);
        }*/

        if (objective.hasPlate == true && firstObject == false ||
            objective.hasBread == true && firstObject == false ||
            objective.hasKnife == true && firstObject == false)
        {
            firstObject = true;
            ShowTutorial();
        }

        if (inventory.hamUsed == true && allObjectives == false)
        {
            allObjectives = true;
            numOfIngredients++;
            ShowFinalTutorial();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void ButtonBehaviour(int i)
    {
        switch (i)
        {
            case (0):
            default:
                SceneManager.LoadScene("Kitchen");
                break;
            case (1):
                context[0].SetActive(false);
                context[1].SetActive(true);
                contextButtons[0].SetActive(false);
                contextButtons[1].SetActive(true);
                break;
            case (2):
                SceneManager.LoadScene("Kitchen");
                break;
            case (3):
                Debug.Log("Quit");
                Application.Quit();
                break;
        }
    }

    public void Unlock()
    {
        returnArrow.SetActive(true);
        isLocked = false;
        gloveTutorial.SetActive(false);
        objectTutorial.SetActive(false);
        finalTutorial.SetActive(false);
    }

    public void UnlockStart()
    {
        startTutorial.SetActive(false);
        isLocked = false;
        kitchenClock.SetBool("Active", true);
        StartCoroutine(TimeTillLock());
    }

    public void ShowTutorial()
    {
        objectTutorial.SetActive(true);
        Lock();
    }

    public void ShowGloveTutorial()
    {
        gloveTutorial.SetActive(true);
        Lock();
    }

    public void ShowFinalTutorial()
    {
        finalTutorial.SetActive(true);
        Lock();
    }

    public void Lock()
    {
        returnArrow.SetActive(false);
        isLocked = true;
    }

    IEnumerator TimeTillLock()
    {
        yield return new WaitForSeconds(900);
        isLocked = true;
        yield return new WaitForSeconds(5);
        isLocked = true;
        yield return new WaitForSeconds(5);
        isLocked = true;
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(10);
        microwave.working = false;
        //microwave.doorAnim.SetBool("Working", false);
    }
}
