using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCtrl : MonoBehaviour
{
    public GameManager gameMng;
    public MicrowaveManager microwave;

    public Animator cameraAnim;
    public Animator microwaveAnim;

    public Button[] directionalButtons;

    [SerializeField]
    private int currentPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
                        directionalButtons[1].enabled = true;
                    }
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("RotateLeft");
                        directionalButtons[0].enabled = false;
                    }
                    currentPanel--;
                    gameMng.isLocked = true;
                    StartCoroutine(EndTransition());
                    break;
                case 1:
                    if (currentPanel == -1)
                    {
                        cameraAnim.SetTrigger("Return");
                        directionalButtons[0].enabled = true;
                    }
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("RotateRight");
                        if (microwave.working == true)
                            microwaveAnim.SetTrigger("Return");
                        directionalButtons[1].enabled = false;
                    }
                    currentPanel++;
                    gameMng.isLocked = true;
                    StartCoroutine(EndTransition());
                    break;
            }
        }
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1.0f);
        gameMng.isLocked = false;
    }
}
