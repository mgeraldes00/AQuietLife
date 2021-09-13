using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Tutorial tut;
    [SerializeField] private TutorialTxt tutTxt;

    [SerializeField] private Animator txtAnim;
    [SerializeField] private GameObject txtObj;
    [SerializeField] private GameObject[] button;

    private float delay = 0.01f;

    public string txt;
    public string currentTxt = "";

    public bool isOpen;

    public void ButtonBehaviour(int i)
    {
        if (tut.isLocked != true)
        {
            switch (i)
            {
                case 0:
                    switch (tut.stage)
                    {
                        case 1:
                            StartCoroutine(UpdateText(2, 0));
                            StartCoroutine(ButtonSwap());
                            break;
                        case 2:
                            StartCoroutine(UpdateText(4, 0));
                            StartCoroutine(ButtonSwap());
                            break;
                        case 5:
                            StartCoroutine(UpdateText(8, 0));
                            StartCoroutine(ButtonSwap());
                            break;
                        case 6:
                            StartCoroutine(UpdateText(10, 1));
                            tut.stage++;
                            break;
                        case 7:
                            StartCoroutine(UpdateText(11, 0));
                            StartCoroutine(ButtonSwap());
                            break;
                        case 8:
                            StartCoroutine(UpdateText(13, 1));
                            tut.stage++;
                            break;
                        case 9:
                            StartCoroutine(UpdateText(14, 0));
                            StartCoroutine(ButtonSwap());
                            break;
                        case 10:
                            StartCoroutine(UpdateText(16, 1));
                            tut.stage++;
                            break;
                        case 11:
                            StartCoroutine(UpdateText(17, 1));
                            tut.stage++;
                            break;
                        case 12:
                            StartCoroutine(UpdateText(18, 1));
                            tut.stage++;
                            break;
                        case 13:
                            StartCoroutine(UpdateText(19, 2));
                            StartCoroutine(ButtonSwap());
                            break;
                    }
                    break;
                case 1:
                    StartCoroutine
                        (BlurCtrl.RemoveBlur(tut.blur));
                    button[1].GetComponent<Animator>().SetTrigger("Hide");
                    StartCoroutine(tut.QuickLock());
                    StartCoroutine(HideText());
                    isOpen = false;
                    if (tut.stage >= 1)
                        tut.stage++;
                    switch (tut.stage)
                    {
                        case 4:
                            tut.phoneMng.exitLocked = false;
                            tut.phoneMng.messagesLocked = true;
                            break;
                        case 5:
                            tut.returnButton.GetComponent<Button>().enabled = true;
                            break;
                        case 6:
                            tut.directionalButton[1].GetComponent<Animator>().SetTrigger("Hide");
                            break;
                    }
                    break;
            }
        }
    }

    public IEnumerator ShowText(int i, int h)
    {
        isOpen = true;

        tut.phoneMng.isLockedFromTut = true;

        txtAnim.SetBool("Active", true);
        StartCoroutine(ShowButton(i));
        StartCoroutine
            (BlurCtrl.BlurScreen(tut.blur));

        txt = tutTxt.text[h];

        yield return new WaitForSecondsRealtime(0.5f);
        txtObj.GetComponent<TextMeshProUGUI>().raycastTarget = true;
        for (int c = 0; c < txt.Length; c++)
        {
            currentTxt = txt.Substring(0, c);
            txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    IEnumerator UpdateText(int h, int multUpdate)
    {
        txtAnim.SetTrigger("UpdateText");
        txt = "";
        currentTxt = "";
        yield return new WaitForSecondsRealtime(0.9f);
        StartCoroutine(tut.QuickLock());
        yield return new WaitForSecondsRealtime(0.1f);
        txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;

        yield return new WaitForSecondsRealtime(0.1f);
        txt = tutTxt.text[h];

        yield return new WaitForSecondsRealtime(0.1f);
        txtObj.GetComponent<TextMeshProUGUI>().raycastTarget = true;
        for (int c = 0; c < txt.Length; c++)
        {
            currentTxt = txt.Substring(0, c);
            txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;
            yield return new WaitForSecondsRealtime(delay);
        }
        if (multUpdate == 1)
            txtAnim.SetTrigger("Wait");
    }

    public IEnumerator HideText()
    {
        txtAnim.SetBool("Active", false);
        txt = "";
        currentTxt = "";
        yield return new WaitForSecondsRealtime(1.0f);
        txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;
        tut.phoneMng.isLockedFromTut = false;

        isOpen = false;
    }

    IEnumerator ShowButton(int i)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (i == 1)
            button[0].GetComponent<Animator>().SetTrigger("Show");
        else if (i == 2)
            button[1].GetComponent<Animator>().SetTrigger("Show");
    }

    IEnumerator ButtonSwap()
    {
        button[0].GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSecondsRealtime(1.0f);
        button[1].GetComponent<Animator>().SetTrigger("Show");
    }
}
