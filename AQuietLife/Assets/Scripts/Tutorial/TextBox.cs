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

                    break;
                case 1:
                    StartCoroutine
                        (BlurCtrl.RemoveBlur(tut.blur));
                    button[1].GetComponent<Animator>().SetTrigger("Hide");
                    StartCoroutine(tut.QuickLock());
                    StartCoroutine(HideText());
                    isOpen = false;
                    break;
            }
        }
    }

    public IEnumerator ShowText(int i, int h)
    {
        txtAnim.SetBool("Active", true);
        StartCoroutine(ShowButton(i));
        StartCoroutine
            (BlurCtrl.BlurScreen(tut.blur));

        txt = tutTxt.text[h];

        yield return new WaitForSeconds(0.5f);
        txtObj.GetComponent<TextMeshProUGUI>().raycastTarget = true;
        for (int c = 0; c < txt.Length; c++)
        {
            currentTxt = txt.Substring(0, c);
            txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    public IEnumerator HideText()
    {
        txtAnim.SetBool("Active", false);
        txt = "";
        currentTxt = "";
        yield return new WaitForSecondsRealtime(1.0f);
        txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;
    }

    IEnumerator ShowButton(int i)
    {
        yield return new WaitForSeconds(1.0f);
        if (i == 1)
            button[0].GetComponent<Animator>().SetTrigger("Show");
        else if (i == 2)
            button[1].GetComponent<Animator>().SetTrigger("Show");
    }
}
