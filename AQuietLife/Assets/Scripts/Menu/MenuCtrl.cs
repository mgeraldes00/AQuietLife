using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuCtrl : MonoBehaviour
{
    public GameObject[] menuTextObj;
    public GameObject[] buttons;
    public GameObject button;
    public GameObject returnArrow;

    [SerializeField] private UnityEngine.Video.VideoPlayer video;

    [SerializeField] private Image back;
    [SerializeField] private Image fade;

    [SerializeField] private GameObject[] levelImg;

    private float delay = 0.2f;
    
    [SerializeField]
    private string[] menuText;

    [SerializeField] private bool isFirstTime;

    [SerializeField] private int currentPanel;

    private string currentText = "";

    private YieldInstruction fadeInstruction = new YieldInstruction();
    private float fadeTime = 1.0f;

    private void Start()
    {
        if (PlayerPrefs.GetInt("isFirstTime") != 1)
        {
            isFirstTime = true;
        }
        else
        {
            isFirstTime = false;
            video.playbackSpeed = 5;
        }
        StartCoroutine(ShowButtons());
        //StartCoroutine(ShowText());
    }

    public void ButtonBehaviour(int i)
    {
        switch (i)
        {
            case (0):
            default:
                //StartCoroutine(Continue());

                break;
            case (1):
                //Options
                button.GetComponent<Animator>().SetTrigger("Switch");
                returnArrow.SetActive(true);
                returnArrow.GetComponent<Animator>().SetTrigger("Switch");
                StartCoroutine(FadeIn(back));
                currentPanel = 1;
                break;
            case (2):
                //Level Selection
                button.GetComponent<Animator>().SetTrigger("Switch");
                returnArrow.SetActive(true);
                returnArrow.GetComponent<Animator>().SetTrigger("Switch");
                StartCoroutine(FadeIn(back));
                for (int b = 0; b < levelImg.Length; b++)
                    levelImg[b].GetComponent<Animator>().SetTrigger("Show");
                currentPanel = 2;
                break;
            case (3):
                Debug.Log("Quit");
                PlayerPrefs.DeleteKey("isFirstTime");
                Application.Quit();
                break;
            case (4):
                fade.GetComponent<Animator>().SetTrigger("Black");
                StartCoroutine(NewGame()); 
                break;
            case (5):
                //Return
                returnArrow.GetComponent<Animator>().SetTrigger("Return");
                StartCoroutine(FadeOut(back));
                if (currentPanel == 2)
                {
                    for (int b = 0; b < levelImg.Length; b++)
                        levelImg[b].GetComponent<Animator>().SetTrigger("Hide");
                    button.GetComponent<Animator>().SetTrigger("Return");
                }
                else if (currentPanel == 1)
                {
                    button.GetComponent<Animator>().SetTrigger("Return");
                }
                break;
            case (6):
                fade.GetComponent<Animator>().SetTrigger("White");
                StartCoroutine(Continue("Kitchen"));
                break;
            case (7):
                //fade.GetComponent<Animator>().SetTrigger("White");
                //StartCoroutine(Continue("Bedroom"));
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

    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Intro");
    }

    IEnumerator Continue(string s)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(s);
    }

    IEnumerator ShowButtons()
    {
        if (isFirstTime == true)
        {
            yield return new WaitForSeconds(7.5f);
            button.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
            button.SetActive(true);
        }
    }

    IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }

    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
            currentPanel = 0;
        }
    }
}
