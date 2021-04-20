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
      
        /*Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider.CompareTag("Direction")
            || hit.collider.CompareTag("Direction2"))
        {
            //Highlight arrow
        }

        if (Input.GetMouseButtonDown(0) && gameMng.isLocked == false)
        {
            if (hit.collider.CompareTag("Direction")
                || hit.collider.CompareTag("Direction2"))
            {
                switch (currentPanel)
                {
                    case -1:
                        cameraAnim.SetTrigger("Return");
                        currentPanel++;
                        break;
                    case 0:

                        break;
                    case 1:

                        break;
                }
            }
        }*/
    }

    public void ButtonBehaviour(int i)
    {
        if (gameMng.isLocked == false)
        {
            switch (i)
            {
                case 0:
                    if (currentPanel == 1)
                    {
                        cameraAnim.SetTrigger("Return");
                        if (microwave.working == true)
                            microwaveAnim.SetTrigger("RotateLeft");
                        directionalButtons[1].SetActive(true);
                    }
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("Return");
                        directionalButtons[0].SetActive(false);
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
                        directionalButtons[0].SetActive(true);
                    }
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("RotateRight");
                        if (microwave.working == true)
                            microwaveAnim.SetTrigger("Return");
                        directionalButtons[1].SetActive(false);
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
        fadeAnim.SetTrigger("Transition");
    }

    public void BackToGeneral()
    {
        currentView--;
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(0.5f);
        gameMng.isLocked = false;
    }
}
