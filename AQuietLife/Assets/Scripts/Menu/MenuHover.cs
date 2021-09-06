using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuHover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private PointerManager pointers;
    private UIFollowMouse cursors;

    public Image selectedImg;

    public TMP_Text selectedText;

    [SerializeField] private Animator imgAnim;

    public bool isOver = false;

    [SerializeField] private bool isLocked;

    [SerializeField] private int currentScene;

    private void Start()
    {
        pointers = FindObjectOfType<PointerManager>();
        cursors = FindObjectOfType<UIFollowMouse>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentScene == 0 && isLocked == false)
        {   
            if (FindObjectOfType<MenuCtrl>().watchingCredits == false)
            {
                isOver = true;
                //pointers.ChangeCursor(2);
                cursors.ChangeCursor("Point", 1);
                if (selectedImg != null)
                    selectedImg.enabled = true;
                if (selectedText != null)
                    selectedText.fontStyle = FontStyles.Underline;
                if (selectedImg == null && selectedText == null)
                    imgAnim.SetTrigger("Enlarge");
                FindObjectOfType<AudioCtrl>().Play("MenuSelect");
            }
        }
        else if (currentScene == 1)
        {
            isOver = true;
            if (selectedText != null)
                selectedText.fontStyle = FontStyles.Underline;
            if (cursors != null)
                //pointers.ChangeCursor(2);
                cursors.ChangeCursor("Point", 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentScene == 0 && isLocked == false)
        {
            if (!FindObjectOfType<MenuCtrl>())
            {

            }
            else if (FindObjectOfType<MenuCtrl>().watchingCredits == false)
            {
                isOver = false;
                //pointers.ChangeCursor(1);
                cursors.ChangeCursor("Point", 0);
                if (selectedImg != null)
                    selectedImg.enabled = false;
                if (selectedText != null)
                    selectedText.fontStyle = FontStyles.Normal;
                if (selectedImg == null && selectedText == null)
                    imgAnim.SetTrigger("Normalize");
            }
        }
        else if (currentScene == 1)
        {
            isOver = false;
            if (selectedText != null)
                selectedText.fontStyle = FontStyles.Normal;
            if (cursors != null)
                //pointers.ChangeCursor(1);
                cursors.ChangeCursor("Point", 0);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectedImg != null)
            selectedImg.enabled = false;
        if (cursors != null)
            //pointers.ChangeCursor(1);
            cursors.ChangeCursor("Point", 0);
        StartCoroutine(Unlock());
        FindObjectOfType<AudioCtrl>().Play("MenuClick");
    }

    IEnumerator Unlock()
    {
        yield return new WaitForEndOfFrame();
        isLocked = true;
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
    }
}
