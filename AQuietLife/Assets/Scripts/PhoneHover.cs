using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhoneHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PointerManager pointers;

    [SerializeField] private Image selectedImg;

    private void Start()
    {
        pointers = FindObjectOfType<PointerManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = true;
        pointers.ChangeCursor(2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = false;
        pointers.ChangeCursor(1);
    }
}
