using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introController : MonoBehaviour
{
    public Animator cameraAnim;

    public GameObject directionalButton;

    [SerializeField]
    private int currentPanel;

    private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonBehaviour(int i)
    {
        if (isLocked == false)
        {
            switch (i)
            {
                case 1:
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("turn");
                        directionalButton.SetActive(false);
                    }
                    currentPanel++;
                    isLocked = true;
                    StartCoroutine(EndTransition());
                    break;
            }
        }
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
    }
}
