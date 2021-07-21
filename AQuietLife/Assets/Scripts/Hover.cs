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
    private PointerManager pointers;

    public Animator arrow;

    public Animator otherArrow;

    [SerializeField] private GameObject clockSelect;

    [SerializeField] private TMP_Text selectedText;

    public bool isOver = false;

    [SerializeField] private bool isDirectional;
    [SerializeField] private bool isArrow;
    [SerializeField] private bool isPhone;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isGameOver;

    [SerializeField] private int currentLvl;

    private void Start()
    {
        if (currentLvl == 0)
        {
            cam = null;
            gameMng = null;
            pointers = null;
            introHover = FindObjectOfType<IntroCursor>();
        }
        else if (currentLvl == 2)
        {
            cam = FindObjectOfType<CameraCtrl>();
            gameMng = FindObjectOfType<GameManager>();
            pointers = FindObjectOfType<PointerManager>();
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
                //arrow.SetTrigger("Clicked");
                //otherArrow.SetTrigger("Clicked");
                cam.hasClicked = true;
                StartCoroutine(Unlock());
            }
            else if (isArrow == false && isLocked == false && gameMng.isLocked == false
                && isPhone == false)
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
        if (currentLvl == 0)
            introHover.ChangeCursor(2);
        else
            if (pointers != null)
        {
            pointers.ChangeCursor(2);
        } 
        if (isArrow == true && isLocked == false)
        {
            isOver = true;
            arrow.SetBool("Selected", true);
        }
        else if (isArrow == false && isPhone == false
            && isGameOver == false)
            clockSelect.SetActive(true);
        else if (isArrow == false && isPhone == false && selectedText != null
            && isGameOver == true)
        {
            selectedText.fontStyle = FontStyles.Underline;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentLvl == 0)
            introHover.ChangeCursor(1);
        else
            if (pointers != null)
        {
            pointers.ChangeCursor(1);
        }
        if (isArrow == true)
        {
            isOver = false;
            arrow.SetBool("Selected", false);
        }
        else if (isArrow == false && isGameOver == false)
            clockSelect.SetActive(false);
        else if (isArrow == false && isPhone == false && selectedText != null
            && isGameOver == true)
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
