using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCtrl : MonoBehaviour
{
    public GameManager gameMng;
    public MicrowaveManager microwave;

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
            dirButtons.SetActive(false);
        }
        if (currentView == 0)
        {
            dirButtons.SetActive(true);          
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
                    fadeAnim.SetTrigger("Transition");
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
                    fadeAnim.SetTrigger("Transition");
                    StartCoroutine(EndTransition());
                    break;
            }
        }
    }

    public void ObjectTransition()
    {
        fadeAnim.SetTrigger("TransitionObj");
    }

    public void InteractionTransition()
    {
        fadeAnim.SetTrigger("TransitionInteract");
    }

    public void BackToGeneral()
    {
        currentView--;
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
}
