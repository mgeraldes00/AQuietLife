﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private CameraCtrl cam;
    [SerializeField] private GameManager gameMng;

    public TextBox txt;
    public ThoughtManager thought;
    public Phone phoneMng;

    [SerializeField] private GameObject startCover;

    public GameObject rewindButton;

    [SerializeField] private GameObject[] phone;

    [SerializeField] private Collider2D[] area;

    public GameObject blur;
    public GameObject returnButton;
    public GameObject[] directionalButton;

    public GameObject uiPhone;
    [SerializeField] private Animator inventory;

    [SerializeField] private GameObject[] startTextObj;

    private float delay = 0.2f;
    private float dotDelay = 1.0f;

    [SerializeField] private string[] startText;

    private string currentText = "";

    public bool isOver;
    public bool isLocked;
    public bool isLockedArrow;
    public bool rewindOnce;

    public int stage;

    private void Start()
    {
        uiPhone.GetComponent<Image>().enabled = false;
        uiPhone.GetComponent<Animator>().SetBool("Enlarge", true);
        inventory.SetBool("Visible", false);

        phoneMng.isLockedFromTut = true;
        phoneMng.optionsLocked = true;
        phoneMng.exitLocked = true;

        for (int i = 0; i < cam.directionalArrows.Length; i++)
            cam.directionalArrows[i].SetTrigger("Hide");

        if (PlayerPrefs.HasKey("StoryMode"))
        {
            isLocked = true;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            StartCoroutine(BlurCtrl.BlurScreen(blur));
            StartCoroutine(OpenCover());
        }
    }

    public void ButtonBehaviour()
    {
        if (isLocked != true && txt.isOpen != true)
        {
            switch (stage)
            {
                case 5:
                    StartCoroutine
                        (ObjectFade.FadeOut(phone[2].GetComponent<SpriteRenderer>(), 0));
                    StartCoroutine(ArrowBehaviour(0));
                    break;
                case 6:
                    StartCoroutine(ArrowBehaviour(1));
                    break;
            }
            
        }
    }

    public IEnumerator PhoneBehaviour(int i)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        switch (i)
        {
            case 0:
                StartCoroutine(txt.ShowText(1, 1));
                break;
            case 1:
                StartCoroutine(txt.ShowText(1, 3));
                break;
            case 2:
                StartCoroutine(txt.ShowText(2, 5));
                break;
            case 3:
                yield return new WaitForSeconds(1.0f);
                StartCoroutine(txt.ShowText(2, 6));
                uiPhone.GetComponent<Animator>().SetBool("Enlarge", true);
                yield return new WaitForSecondsRealtime(1.0f);
                uiPhone.GetComponent<Image>().enabled = false;
                break;
        }
    }

    public IEnumerator RewindBehaviour(int i)
    {
        switch (i)
        {
            case 0:
                yield return new WaitForSecondsRealtime(0.5f);
                StartCoroutine(txt.ShowText(1, 12));
                break;
        }
    }

    IEnumerator ArrowBehaviour(int i)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        switch (i)
        {
            case 0:
                StartCoroutine(txt.ShowText(1, 7));
                isLockedArrow = true;
                break;
            case 1:
                directionalButton[0].GetComponent<Animator>().SetTrigger("Clicked");
                yield return new WaitForSeconds(0.5f);
                directionalButton[0].SetActive(false);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(txt.ShowText(1, 9));
                area[0].enabled = true;
                returnButton.SetActive(false);
                break;
        }
    }


    IEnumerator OpenCover()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < startText[0].Length; i++)
        {
            currentText = startText[0].Substring(0, i);
            startTextObj[0].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < startText[1].Length; i++)
        {
            currentText = startText[1].Substring(0, i);
            startTextObj[1].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(dotDelay);
        }
        yield return new WaitForEndOfFrame();
        startCover.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(BlurCtrl.RemoveBlur(blur));
        yield return new WaitForSeconds(0.5f);
        gameMng.cursors.ChangeCursor("Open", 1);
        yield return new WaitForSeconds(1.0f);
        //Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isLocked = false;
        Destroy(startCover);
        yield return new WaitForEndOfFrame();
        gameMng.cursors.ChangeCursor("Slide", 1);
    }

    public IEnumerator StartPhoneStage()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine
            (ObjectFade.FadeOut(phone[1].GetComponent<SpriteRenderer>(), 0));
        GameObject.Find("Phone").GetComponent<Animator>().SetBool("Enlarge", false);
        txt.isOpen = false;
    }

    public IEnumerator QuickLock()
    {
        isLocked = true;
        yield return new WaitForSecondsRealtime(1.0f);
        isLocked = false;
        if (isLockedArrow == true)
            isLockedArrow = false;
    }
}
