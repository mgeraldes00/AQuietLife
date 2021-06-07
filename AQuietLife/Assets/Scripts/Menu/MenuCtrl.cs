using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Audio;
using TMPro;

public class MenuCtrl : MonoBehaviour
{
    public GameObject[] menuTextObj;
    public GameObject[] buttons;
    public GameObject button;
    public GameObject options;
    public GameObject returnArrow;

    [SerializeField] private VideoPlayer video;

    [SerializeField] private GameObject audioMng;

    [SerializeField] private Image back;
    [SerializeField] private Image backFull;
    [SerializeField] private Image fade;
    [SerializeField] private Image creditsFull;

    [SerializeField] private GameObject[] levelImg;

    [SerializeField] private Animator skipAnim;

    [SerializeField] private VideoPlayer credits;

    public AudioMixer musicMix;

    private float delay = 0.2f;

    [SerializeField]
    private string[] menuText;

    [SerializeField] private bool isFirstTime;
    [SerializeField] private bool isCreditsFull;
    [SerializeField] private bool hasSkipped;
    [SerializeField] private bool hasReturned;

    public bool watchingCredits;

    [SerializeField] private int currentPanel;

    private string currentText = "";

    private YieldInstruction fadeInstruction = new YieldInstruction();
    private float fadeTime = 1.0f;

    private void Start()
    {
        skipAnim.SetTrigger("Show");
        StartCoroutine(ShowButtons());
        Instantiate(audioMng);
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 0.1f, 1));
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
                options.SetActive(true);
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
                //returnArrow.GetComponent<Animator>().SetTrigger("Return");
                StartCoroutine(FadeOut(back));
                if (currentPanel == 2)
                {
                    for (int b = 0; b < levelImg.Length; b++)
                        levelImg[b].GetComponent<Animator>().SetTrigger("Hide");
                    button.GetComponent<Animator>().SetTrigger("Return");
                    returnArrow.GetComponent<Animator>().SetTrigger("Return");
                    StartCoroutine(ChangePanel(0));
                }
                else if (currentPanel == 1)
                {
                    options.GetComponent<Animator>().SetTrigger("Switch");
                    //button.SetActive(true);
                    button.GetComponent<Animator>().SetTrigger("Return");
                    returnArrow.GetComponent<Animator>().SetTrigger("Return");
                    StartCoroutine(HideObject(options));
                    StartCoroutine(FadeIn(backFull));
                    watchingCredits = false;
                }
                else if (currentPanel == 3)
                {
                    options.SetActive(true);
                    //options.GetComponent<Animator>().SetTrigger("Return");
                    StartCoroutine(FadeOut(creditsFull));
                    back.enabled = true;
                    StartCoroutine(FadeIn(back));
                    credits.Stop();
                    StartCoroutine(ChangePanel(1));
                    backFull.enabled = true;
                }
                break;
            case (6):
                fade.enabled = true;
                fade.GetComponent<Animator>().SetTrigger("White");
                returnArrow.GetComponent<Animator>().SetTrigger("Return");
                StartCoroutine(Continue("Kitchen"));
                StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 1, 0));
                break;
            case (7):
                //fade.GetComponent<Animator>().SetTrigger("White");
                //StartCoroutine(Continue("Bedroom"));
                break;
            case (8):
                if (isFirstTime == true && currentPanel == 0)
                {
                    hasSkipped = true;
                    skipAnim.SetTrigger("Hide");
                    isFirstTime = false;
                    StartCoroutine(FadeIn(backFull));
                    StartCoroutine(ResetSkip(button, 0));
                }
                else if (isCreditsFull == true && currentPanel == 3)
                {
                    hasSkipped = true;
                    hasReturned = true;
                    skipAnim.SetTrigger("Hide");
                    isCreditsFull = false;
                    StartCoroutine(FadeIn(creditsFull));
                    StartCoroutine(ResetSkip(returnArrow, 1));
                    StartCoroutine(RevealArrow());
                }
                break;
            case (9):
                //Credits
                watchingCredits = true;
                fade.GetComponent<Animator>().SetTrigger("White");
                StartCoroutine(FadeOut(back));
                StartCoroutine(FadeOut(backFull));
                backFull.enabled = false;
                StartCoroutine(PlayCredits());
                //button.SetActive(false);
                options.GetComponent<Animator>().SetTrigger("Switch");
                returnArrow.GetComponent<Animator>().SetTrigger("Return");
                isCreditsFull = true;
                currentPanel = 3;
                //backFull.enabled = false;
                break;
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
        yield return new WaitForSeconds(6.0f);
        CheckForSkip();
        yield return new WaitForSeconds(1.5f);
        button.SetActive(true);
    }

    private void CheckForSkip()
    {
        if (hasSkipped == true)
            hasSkipped = false;
        else
            skipAnim.SetTrigger("Hide");
    }

    IEnumerator ShowOptions()
    {
        yield return new WaitForSeconds(1.0f);
        options.SetActive(true);
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
            //currentPanel = 0;
        }
    }

    IEnumerator PlayCredits()
    {
        yield return new WaitForSeconds(0.8f);
        credits.Play();
        options.SetActive(false);
        skipAnim.SetTrigger("Show");
        yield return new WaitForSeconds(0.3f);
        fade.enabled = false;
        back.enabled = false;
        backFull.enabled = false;
        returnArrow.SetActive(false);
        yield return new WaitForSeconds(9.0f);
        CheckForSkip();
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(ResetSkip(returnArrow, 2));
        StartCoroutine(RevealArrow());
    }

    IEnumerator ResetSkip(GameObject gameObject, int i)
    {
        yield return new WaitForSeconds(1.0f);
        if (i == 0)
            isFirstTime = true;
        else if (i == 1)
            isCreditsFull = true;
        gameObject.SetActive(true);
        video.Stop();
    }

    IEnumerator RevealArrow()
    {
        yield return new WaitForSeconds(1.0f);
        if (hasSkipped == true)
            returnArrow.GetComponent<Animator>().SetTrigger("Switch");
        else
            CheckForReturn();
    }

    private void CheckForReturn()
    {
        if (hasReturned == true)
            hasReturned = false;
        else
            returnArrow.GetComponent<Animator>().SetTrigger("Switch");
    }

    IEnumerator HideObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

    IEnumerator ChangePanel(int i)
    {
        yield return new WaitForSeconds(1.0f);
        currentPanel = i;
    }

    IEnumerator ChangeBool(bool thisBool)
    {
        yield return new WaitForSeconds(1.0f);
        thisBool = false;
    }
}
