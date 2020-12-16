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
    public BreadBoxManager breadBox;
    public DrawerManager drawers;
    public TableManager table;
    public ClockManager clock;
    
    public CabinetManager cabinet;

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

    public bool isLocked;
    public bool firstObject;
    private bool allObjectives;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
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
            || hit.collider.CompareTag("Cabinet") || hit.collider.CompareTag("BreadBox"))
        {
            Debug.Log("Object");
            inspectionText.SetActive(true);          
        }

        if (hit.collider.CompareTag("CabinetDoor1") 
            || hit.collider.CompareTag("CabinetDoor2") && cabinet.door2Open == false
            || hit.collider.CompareTag("CabinetDoor3") 
            || hit.collider.CompareTag("CabinetDoor4") && cabinet.door4Open == false
            || hit.collider.CompareTag("BreadBoxDoor") && breadBox.doorOpen == false
            || hit.collider.CompareTag("DrawerDoor1")
            || hit.collider.CompareTag("DrawerDoor2") && drawers.doorCenterOpen == false
            || hit.collider.CompareTag("DrawerDoor3"))
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

        if (objective.part1Complete == true && allObjectives == false)
        {
            allObjectives = true;
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
                SceneManager.LoadScene("Context");
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
    }

    public void ShowTutorial()
    {
        objectTutorial.SetActive(true);
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
}
