using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ObjectiveManager objective;

    public GameObject[] context;
    public GameObject[] contextButtons;

    public GameObject inspectionText;
    public GameObject returnArrow;
    public GameObject gloveTutorial;
    public GameObject objectTutorial;
    public GameObject finalTutorial;

    public bool isLocked;
    public bool firstObject;
    private bool allObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        else if (hit.collider.CompareTag("Nothing"))
        {
            inspectionText.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            inspectionText.SetActive(false);
        }

        if (objective.hasPlate == true && firstObject == false ||
            objective.hasBread == true && firstObject == false ||
            objective.hasKnife == true && firstObject == false)
        {
            firstObject = true;
            ShowTutorial();
        }

        if (objective.hasPlate == true && objective.hasBread == true 
            && objective.hasKnife == true && allObjects == false)
        {
            allObjects = true;
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
