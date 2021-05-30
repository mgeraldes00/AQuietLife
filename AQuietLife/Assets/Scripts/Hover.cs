using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CameraCtrl cam;
    private GameManager gameMng;

    public Animator arrow;

    public Animator otherArrow;

    [SerializeField] private GameObject clockSelect;

    public bool isOver = false;

    [SerializeField] private bool isDirectional;
    [SerializeField] private bool isArrow;
    [SerializeField] private bool isLocked;

    private void Start()
    {
        cam = FindObjectOfType<CameraCtrl>();
        gameMng = FindObjectOfType<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDirectional == true && cam.hasClicked == false && isArrow == true)
        {
            arrow.SetTrigger("Clicked");
            otherArrow.SetTrigger("Clicked");
            cam.hasClicked = true;
            StartCoroutine(Unlock());
        }
        else if (isArrow == false && isLocked == false && gameMng.isLocked == false)
        {
            clockSelect.SetActive(false);
            isLocked = true;
            StartCoroutine(UnlockClock());
        }   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isArrow == true)
        {
            isOver = true;
            arrow.SetBool("Selected", true);
        }
        else if (isArrow == false && gameMng.isLocked == false)
            clockSelect.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isArrow == true)
        {
            isOver = false;
            arrow.SetBool("Selected", false);
        }
        else if (isArrow == false && gameMng.isLocked == false)
            clockSelect.SetActive(false);
    }

    IEnumerator UnlockClock()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1.5f);
        cam.hasClicked = false;
    }
}
