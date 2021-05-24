using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image selectedImg;

    public bool isOver = false;

    [SerializeField] private bool hasClicked;
    [SerializeField] private bool isDirectional;

    public void OnPointerClick(PointerEventData eventData)
    {       
        if (selectedImg != null)
            selectedImg.enabled = false;
        if (isDirectional == true)
        {
            hasClicked = true;
            StartCoroutine(Unlock());
        }       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        if (selectedImg != null && hasClicked == false)
            selectedImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        if (selectedImg != null)
            selectedImg.enabled = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(0.5f);
        hasClicked = false;
        if (selectedImg != null && isOver == true)
            selectedImg.enabled = true;
    }
}
