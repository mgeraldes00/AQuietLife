using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Animator arrow;

    public bool isOver = false;

    [SerializeField] private bool hasClicked;
    [SerializeField] private bool isDirectional;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDirectional == true && hasClicked == false)
        {
            arrow.SetTrigger("Clicked");
            hasClicked = true;
            StartCoroutine(Unlock());
        }       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        arrow.SetBool("Selected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        arrow.SetBool("Selected", false);
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1.5f);
        hasClicked = false;
    }
}
