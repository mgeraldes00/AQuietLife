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

    public Animator[] directionalArrows;
    public Animator[] upDownArrows;
    public Animator[] returnArrows;

    public Animator microwaveAnim;

    public GameObject[] directionalButtons;
    public GameObject[] returnButtons;

    public int currentPanel;

    public int currentView;

    public GameObject dirButtons;

    public bool hasClicked;

    // Start is called before the first frame update
    void Start()
    {
        currentPanel = -1;
    }

    public void ButtonBehaviour(int i)
    {
        Tutorial tut = FindObjectOfType<Tutorial>();
        if (tut.txt.isOpen != true && tut.isLocked != true || tut == null)
        {
            if (gameMng.isLocked == false)
            {
                switch (i)
                {
                    case 0:
                        if (currentPanel == -1)
                        {
                            cameraAnim.SetTrigger("Return");
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("Return2");
                        }
                        if (currentPanel == 0)
                        {
                            cameraAnim.SetTrigger("Return");
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("Center2");
                        }
                        if (currentPanel == 1)
                        {
                            cameraAnim.SetTrigger("Return");
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("RotateLeft");
                        }
                        if (currentPanel == 2)
                        {
                            cameraAnim.SetTrigger("Return");
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("Return");
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
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("Return3");
                        }
                        if (currentPanel == 0)
                        {
                            cameraAnim.SetTrigger("RotateRight");
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("Return");
                        }
                        if (currentPanel == 1)
                        {
                            cameraAnim.SetTrigger("RotateRight");
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("RotateLeft");
                        }
                        if (currentPanel == 2)
                        {
                            cameraAnim.SetTrigger("RotateRight");
                            if (microwave != null && microwave.working == true)
                                microwaveAnim.SetTrigger("Center");
                        }
                        currentPanel++;
                        gameMng.isLocked = true;
                        fadeAnim.SetTrigger("TransitionRight");
                        FindObjectOfType<AudioCtrl>().Play("PageTurnRight");
                        StartCoroutine(EndTransition());
                        break;
                    case 2:
                        cameraAnim.SetTrigger("Alt");
                        upDownArrows[0].SetTrigger("Hide");
                        upDownArrows[1].SetTrigger("Show");
                        fridge.lookAtFridge = false;
                        break;
                    case 3:
                        cameraAnim.SetTrigger("Alt");
                        upDownArrows[0].SetTrigger("Show");
                        upDownArrows[1].SetTrigger("Hide");
                        fridge.lookAtFridge = true;
                        break;
                }
            }

        }
    }

    public void ObjectTransition()
    {
        //fadeAnim.SetTrigger("TransitionObj");
        for (int i = 0; i < directionalArrows.Length; i++)
            directionalArrows[i].SetTrigger("Hide");
        returnArrows[0].SetTrigger("Show");
        gameMng.isLocked = true;
        StartCoroutine(ArrowUnlock());
        StartCoroutine(gameMng.QuickUnlock(0.5f));
    }

    public void InteractionTransition()
    {
        fadeAnim.SetTrigger("TransitionInteract");
    }

    public void BackToGeneral()
    {
        if (gameMng.returnable == true && gameMng.isLocked == false)
        {
            cameraAnim.SetTrigger("Return2");
            if (currentView == 1)
            {
                gameMng.isLocked = true;
                StartCoroutine(RestoreView(1));
                returnArrows[0].SetBool("Hide 0", true);
                returnButtons[0].GetComponent<Button>().enabled = false;
                for (int i = 0; i < directionalArrows.Length; i++)
                    directionalArrows[i].SetTrigger("Show");
            }
            else if (currentView == 2)
            {
                gameMng.isLocked = true;
                StartCoroutine(RestoreView(2));
            }
        }
        else
        {
            StartCoroutine(gameMng.LiberateReturn());
        }
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1.0f);
        if (currentPanel == 3)
            currentPanel = -1;
        if (currentPanel == -2)
            currentPanel = 2;
        gameMng.isLocked = false;
    }

    IEnumerator RestoreView(int i)
    {
        yield return new WaitForSeconds(0.1f);
        if (i == 1)
            currentView--;
        yield return new WaitForSeconds(0.4f);
        switch (i)
        {
            case 1:
                returnButtons[0].SetActive(true);
                returnArrows[0].SetBool("Hide 0", false);
                break;
            case 2:
                currentView--;
                break;
        }
        gameMng.isLocked = false;
    }

    IEnumerator ArrowUnlock()
    {
        yield return new WaitForSeconds(0.5f);
        returnButtons[0].GetComponent<Button>().enabled = true;
    }
}
