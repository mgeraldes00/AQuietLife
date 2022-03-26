using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ThoughtManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Tutorial tut;
    [SerializeField] private GameManager gameMng;

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
    [SerializeField] private bool isHovering;

    public void ShowThought()
    {
        if (isThinking == false)
        {
            balloon.SetTrigger("Appear");
            isThinking = true;
            StartCoroutine(ShowText());
            this.StartCoroutine(TimeToDisappear(), ref resetHandle);
            if (balloonActive != true)
            {
                StartCoroutine(ShowBalloon());
            }
        }
        else
        {
            this.TryStopCoroutine(ref resetHandle);

            UpdateThought(1);
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
            this.TryStopCoroutine(ref resetHandle);

            UpdateThought(2);
        }
    }

    public void UpdateThought(int i)
    {
        StartCoroutine(UpdateText(i));
    }

    public void HideThought()
    {
        StartCoroutine(InstantDissappear());
    }

    IEnumerator InstantDissappear()
    {
        yield return new WaitForEndOfFrame();
        isThinking = false;
        balloonActive = false;
        textObj.GetComponent<TextMeshProUGUI>().raycastTarget = false;
        yield return new WaitForSeconds(0.1f);
        balloon.SetTrigger("Disappear");
        text = "";
        currentText = "";
        yield return new WaitForSeconds(1.0f);
        textObj.GetComponent<TextMeshProUGUI>().text = currentText;
        attachedObj = null;
    }

    IEnumerator TimeToDisappear()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        while (isHovering == true)
            yield return null;
        if (isThinking != false)
        {
            textObj.GetComponent<TextMeshProUGUI>().raycastTarget = false;
            balloon.SetTrigger("Disappear");
            balloonActive = false;
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
        yield return new WaitForEndOfFrame();
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

    IEnumerator UpdateText(int typeOfThought)
    {
        isThinking = false;
        yield return new WaitForEndOfFrame();
        if (typeOfThought == 1)
            this.RestartCoroutine(TimeToDisappear(), ref resetHandle);
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
        isHovering = true;
        //this.TryStopCoroutine(ref resetHandle);
        gameMng.cursors.ChangeCursor("Point", 1);
        gameMng.cursors.RemoveCursorForUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        balloon.SetBool("OverBalloon", false);
        isHovering = false;
        //this.StartCoroutine(TimeToDisappear(), ref resetHandle);
        gameMng.cursors.ChangeCursor("Point", 0);
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
