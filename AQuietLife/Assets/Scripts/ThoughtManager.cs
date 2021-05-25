using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ThoughtManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator balloon;

    private float delay = 0.02f;

    public string text;

    public GameObject textObj;

    public string currentText = "";

    //public TextMeshProUGUI txt;

    public bool isThinking;

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
        }
    }

    public void HideThought()
    {
        StartCoroutine(InstantDissappear());
    }

    IEnumerator InstantDissappear()
    {
        yield return new WaitForSeconds(0.1f);
        balloon.SetTrigger("Disappear");
        text = "";
        currentText = "";
        isThinking = false;
        yield return new WaitForSeconds(1.0f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i);
            textObj.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator TimeToDisappear()
    {
        yield return new WaitForSeconds(3.5f);
        balloon.SetTrigger("Disappear");
        text = "";
        currentText = "";
        isThinking = false;
        yield return new WaitForSeconds(1.0f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Thinking");
        balloon.SetBool("OverBalloon", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        balloon.SetBool("OverBalloon", false);
    }
}
