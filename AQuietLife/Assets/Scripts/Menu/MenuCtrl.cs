using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuCtrl : MonoBehaviour
{
    public GameObject[] menuTextObj;
    public GameObject[] buttons;
    public GameObject button;

    private float delay = 0.2f;
    
    [SerializeField]
    private string[] menuText;

    private string currentText = "";

    private void Start()
    {
        //StartCoroutine(ShowText());
        StartCoroutine(ShowButtons());
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
                //Options
                break;
            case (2):
                //Level Selection
                break;
            case (3):
                Debug.Log("Quit");
                Application.Quit();
                break;
            case (4):
                SceneManager.LoadScene("Intro");
                break;
        }
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < menuText[0].Length; i++)
        {
            currentText = menuText[0].Substring(0, i);
            menuTextObj[0].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        for (int i = 0; i < menuText[1].Length; i++)
        {
            currentText = menuText[1].Substring(0, i);
            menuTextObj[1].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        for (int i = 0; i < menuText[2].Length; i++)
        {
            currentText = menuText[2].Substring(0, i);
            menuTextObj[2].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator ShowButtons()
    {
        yield return new WaitForSeconds(8.0f);
        button.SetActive(true);
    }
}
