using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhoneHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PointerManager pointers;
    private UIFollowMouse cursors;

    [SerializeField] private Image selectedImg;

    private void Start()
    {
        pointers = FindObjectOfType<PointerManager>();
        cursors = FindObjectOfType<UIFollowMouse>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = true;
        //pointers.ChangeCursor(2);
        cursors.ChangeCursor("Point", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = false;
        //pointers.ChangeCursor(1);
        //cursors.ChangeCursor("Point", 0);
    }
}
