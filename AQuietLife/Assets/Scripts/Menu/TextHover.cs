using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class TextHover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text selectedText;

    public GameObject fade;

    public bool isOver = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(NextScene());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedText != null)
            selectedText.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectedText != null)
            selectedText.fontStyle = FontStyles.Normal;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForEndOfFrame();
        FindObjectOfType<AudioCtrl>().Play("DoorBell");
        yield return new WaitForSeconds(1.0f);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Dialog2");
    }
}
