using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CameraCtrl cam;
    private GameManager gameMng;
    private IntroCursor introHover;

    public Animator arrow;

    public Animator otherArrow;

    [SerializeField] private GameObject clockSelect;

    [SerializeField] private TMP_Text selectedText;

    public bool isOver = false;

    [SerializeField] private bool isDirectional;
    [SerializeField] private bool isArrow;
    [SerializeField] private bool isPhone;
    [SerializeField] private bool isLocked;

    [SerializeField] private int currentLvl;

    private void Start()
    {
        if (currentLvl == 0)
        {
            cam = null;
            gameMng = null;
            introHover = FindObjectOfType<IntroCursor>();
        }
        else if (currentLvl == 2)
        {
            cam = FindObjectOfType<CameraCtrl>();
            gameMng = FindObjectOfType<GameManager>();
            introHover = null;
        }   
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentLvl == 0)
        {
            if (isArrow == true)
            {
                arrow.SetTrigger("Clicked");
                StartCoroutine(UnlockClock());
                introHover.ChangeCursor(1);
            }
        }
        else if (currentLvl == 2)
        {
            if (isDirectional == true && cam.hasClicked == false && isArrow == true
            && isPhone == false)
            {
                arrow.SetTrigger("Clicked");
                otherArrow.SetTrigger("Clicked");
                cam.hasClicked = true;
                StartCoroutine(Unlock());
            }
            else if (isArrow == false && isLocked == false && gameMng.isLocked == false
                && isPhone == true)
            {
                clockSelect.SetActive(false);
                isLocked = true;
                StartCoroutine(UnlockClock());
            }
            else if (isArrow == false && isLocked == false && gameMng.isLocked == false
                && isPhone == false)
            {
                //Nothing
            }
        }       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isArrow == true && isLocked == false)
        {
            isOver = true;
            arrow.SetBool("Selected", true);
            introHover.ChangeCursor(2);
        }
        else if (isArrow == false && isPhone == false && gameMng.isLocked == false)
            clockSelect.SetActive(true);
        else if (isArrow == false && isPhone == false && selectedText != null)
        {
            selectedText.fontStyle = FontStyles.Underline;
        }           
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isArrow == true)
        {
            isOver = false;
            arrow.SetBool("Selected", false);
            introHover.ChangeCursor(1);
        }
        else if (isArrow == false && gameMng.isLocked == false)
            clockSelect.SetActive(false);
        else if (isArrow == false && isPhone == false && selectedText != null)
            selectedText.fontStyle = FontStyles.Normal;
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
