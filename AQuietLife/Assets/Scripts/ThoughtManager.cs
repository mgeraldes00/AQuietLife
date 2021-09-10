using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ThoughtManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Tutorial tut;

    public Animator balloon;

    private float delay = 0.01f;

    public string text;

    public GameObject textObj;

    public string currentText = "";

    //public TextMeshProUGUI txt;

    [SerializeField] private bool isThinking;
    [SerializeField] private bool balloonActive;

    public void ShowThought()
    {
        if (isThinking == false)
        {
            balloon.SetTrigger("Appear");
            isThinking = true;
            StartCoroutine(ShowText());
            StartCoroutine(TimeToDisappear());
        }
    }

    public void KeepThought()
    {
        if (isThinking == false)
        {
            balloon.SetTrigger("Appear");
            isThinking = true;
            StartCoroutine(ShowText());
            if (balloonActive != true)
            {
                StartCoroutine(ShowBalloon());
            }
        }
    }

    public void HideThought()
    {
        StartCoroutine(InstantDissappear());
    }

    IEnumerator InstantDissappear()
    {
        yield return new WaitForEndOfFrame();
        textObj.GetComponent<TextMeshProUGUI>().raycastTarget = false;
        yield return new WaitForSeconds(0.1f);
        balloon.SetTrigger("Disappear");
        text = "";
        currentText = "";
        yield return new WaitForSeconds(1.0f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;
        isThinking = false;
        balloonActive = false;
    }

    IEnumerator ShowBalloon()
    {
        yield return new WaitForSeconds(0.5f);
        balloonActive = true;
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        textObj.GetComponent<TextMeshProUGUI>().raycastTarget = true;
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i);
            textObj.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    IEnumerator TimeToDisappear()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        balloon.SetTrigger("Disappear");
        text = "";
        currentText = "";
        isThinking = false;
        yield return new WaitForSecondsRealtime(1.0f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        balloon.SetBool("OverBalloon", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        balloon.SetBool("OverBalloon", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tut == null && balloonActive == true)
        {
            StartCoroutine(InstantDissappear());
        }
        else if (tut != null)
            if (tut.txt.isOpen != true && balloonActive == true)
            {
                switch (tut.rewindOnce)
                {
                    case true:
                        StartCoroutine(InstantDissappear());
                        break;
                    case false:
                        StartCoroutine(InstantDissappear());
                        tut.rewindButton.GetComponent<Animator>().SetBool("Visible", true);
                        tut.rewindOnce = true;
                        break;
                }
                
            }
    }
}
