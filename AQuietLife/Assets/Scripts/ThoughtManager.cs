using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThoughtManager : MonoBehaviour
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

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i);
            textObj.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator TimeToDisappear()
    {
        yield return new WaitForSeconds(2.5f);
        balloon.SetTrigger("Disappear");
        text = "";
        currentText = "";
        isThinking = false;
        yield return new WaitForSeconds(1.0f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;
    }
}
