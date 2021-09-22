﻿using System.Collections;
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

    public string attachedObj;

    private Coroutine resetHandle;

    //public TextMeshProUGUI txt;

    public bool isThinking;
    [SerializeField] private bool balloonActive;

    public void ShowThought()
    {
        if (isThinking == false)
        {
            balloon.SetTrigger("Appear");
            isThinking = true;
            StartCoroutine(ShowText());
            //StartCoroutine(TimeToDisappear());
            if (balloonActive != true)
            {
                StartCoroutine(ShowBalloon());
            }
        }
        else
        {
            //this.RestartCoroutine(TimeToDisappear(), ref resetHandle);
            UpdateThought();
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
        else
        {
            UpdateThought();
        }
    }

    public void UpdateThought()
    {
        StartCoroutine(UpdateText());
    }

    public void HideThought()
    {
        StartCoroutine(InstantDissappear());
    }

    IEnumerator InstantDissappear()
    {
        yield return new WaitForEndOfFrame();
        isThinking = false;
        textObj.GetComponent<TextMeshProUGUI>().raycastTarget = false;
        yield return new WaitForSeconds(0.1f);
        balloon.SetTrigger("Disappear");
        text = "";
        currentText = "";
        yield return new WaitForSeconds(1.0f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;
        balloonActive = false;
        attachedObj = null;
    }

    IEnumerator TimeToDisappear()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        if (isThinking != false)
        {
            balloon.SetTrigger("Disappear");
            text = "";
            currentText = "";
            isThinking = false;
            yield return new WaitForSecondsRealtime(1.0f);
            textObj.GetComponent<TextMeshProUGUI>().text = currentText;
            attachedObj = null;
        }
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

    IEnumerator UpdateText()
    {
        isThinking = false;
        yield return new WaitForEndOfFrame();
        isThinking = true;
        balloon.SetTrigger("Update");
        //text = "";
        currentText = "";
        yield return new WaitForSeconds(0.5f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;

        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i);
            textObj.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSecondsRealtime(delay);
        }
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
                if (tut.stage == 10)
                {
                        switch (tut.rewindOnce)
                        {
                            case true:
                                StartCoroutine(InstantDissappear());
                                break;
                            case false:
                                StartCoroutine(InstantDissappear());
                                tut.rewindButton[0].GetComponent<Animator>().SetBool("Visible", true);
                                tut.rewindOnce = true;
                                break;
                        }
                }
                else
                {
                    StartCoroutine(InstantDissappear());
                }
            }
    }
}
