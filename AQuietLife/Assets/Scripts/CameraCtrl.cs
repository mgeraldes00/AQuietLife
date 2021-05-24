using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCtrl : MonoBehaviour
{
    public GameManager gameMng;
    public MicrowaveManager microwave;
    public FridgeManager fridge;

    public Animator cameraAnim;
    public Animator fadeAnim;
    public Animator microwaveAnim;

    public GameObject[] directionalButtons;

    [SerializeField]
    private int currentPanel;

    [SerializeField]
    public int currentView;

    public GameObject dirButtons;

    // Start is called before the first frame update
    void Start()
    {
        currentPanel = -1;       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentView == 1)
        {
            directionalButtons[0].SetActive(false);
            directionalButtons[1].SetActive(false);
        }
        if (currentView == 0)
        {
            directionalButtons[0].SetActive(true);
            directionalButtons[1].SetActive(true);
        }
    }

    public void ButtonBehaviour(int i)
    {
        if (gameMng.isLocked == false)
        {
            switch (i)
            {
                case 0:
                    if (currentPanel == -1)
                    {
                        cameraAnim.SetTrigger("Return");
                    }
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("Return");
                    }
                    if (currentPanel == 1)
                    {
                        cameraAnim.SetTrigger("Return");
                        if (microwave.working == true)
                            microwaveAnim.SetTrigger("RotateLeft");
                    }
                    if (currentPanel == 2)
                    {
                        cameraAnim.SetTrigger("Return");
                    }
                    currentPanel--;
                    gameMng.isLocked = true;
                    fadeAnim.SetTrigger("TransitionLeft");
                    FindObjectOfType<AudioCtrl>().Play("PageTurn");
                    StartCoroutine(EndTransition());
                    break;
                case 1:
                    if (currentPanel == -1)
                    {
                        cameraAnim.SetTrigger("RotateRight");
                    }
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("RotateRight");
                        if (microwave.working == true)
                            microwaveAnim.SetTrigger("Return");
                    }
                    if (currentPanel == 1)
                    {
                        cameraAnim.SetTrigger("RotateRight");
                    }
                    if (currentPanel == 2)
                    {
                        cameraAnim.SetTrigger("RotateRight");
                    }
                    currentPanel++;
                    gameMng.isLocked = true;
                    fadeAnim.SetTrigger("TransitionRight");
                    FindObjectOfType<AudioCtrl>().Play("PageTurnRight");
                    StartCoroutine(EndTransition());
                    break;
                case 2:
                    cameraAnim.SetTrigger("Alt");
                    ObjectTransition();
                    directionalButtons[2].SetActive(false);
                    directionalButtons[3].SetActive(true);
                    fridge.lookAtFridge = false;
                    break;
                case 3:
                    cameraAnim.SetTrigger("Alt");
                    ObjectTransition();
                    directionalButtons[2].SetActive(true);
                    directionalButtons[3].SetActive(false);
                    fridge.lookAtFridge = true;
                    break;   
            }
        }
    }

    public void ObjectTransition()
    {
        //fadeAnim.SetTrigger("TransitionObj");
    }

    public void InteractionTransition()
    {
        fadeAnim.SetTrigger("TransitionInteract");
    }

    public void BackToGeneral()
    {
        StartCoroutine(RestoreView());
        cameraAnim.SetTrigger("Return2");
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentPanel == 3)
            currentPanel = -1;
        if (currentPanel == -2)
            currentPanel = 2;
        gameMng.isLocked = false;
    }

    IEnumerator RestoreView()
    {
        yield return new WaitForSeconds(0.1f);
        currentView--;
    }
}
