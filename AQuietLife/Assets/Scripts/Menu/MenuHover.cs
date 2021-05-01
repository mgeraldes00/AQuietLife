using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image selectedImg;

    public TMP_Text selectedText;

    public bool isOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        if (selectedImg != null)
            selectedImg.enabled = true;
        if (selectedText != null)
            selectedText.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        if (selectedImg != null)
            selectedImg.enabled = false;
        if (selectedText != null)
            selectedText.fontStyle = FontStyles.Normal;
    }
}
