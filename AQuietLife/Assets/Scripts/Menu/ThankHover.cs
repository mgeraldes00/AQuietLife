using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class ThankHover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image selectedImg;

    public GameObject fade;

    public bool isOver = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = false;
        StartCoroutine(ReturnToMenu());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = false;
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForEndOfFrame();
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
