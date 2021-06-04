using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhoneHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image selectedImg;

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
}
