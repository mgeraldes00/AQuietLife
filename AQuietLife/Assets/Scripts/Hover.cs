using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CameraCtrl cam;

    public Animator arrow;

    public Animator otherArrow;

    public bool isOver = false;

    [SerializeField] private bool isDirectional;

    private void Start()
    {
        cam = FindObjectOfType<CameraCtrl>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDirectional == true && cam.hasClicked == false)
        {
            arrow.SetTrigger("Clicked");
            otherArrow.SetTrigger("Clicked");
            cam.hasClicked = true;
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
        cam.hasClicked = false;
    }
}
