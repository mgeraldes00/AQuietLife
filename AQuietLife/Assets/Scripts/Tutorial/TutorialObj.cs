﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialObj : MonoBehaviour
{
    private GameManager gameMng;
    private CameraCtrl cam;

    [SerializeField] private MediaPlayer audioCtrl;
    [SerializeField] private AudioSlider audioSlider;
    [SerializeField] private Eyelids eyelids;

    private Tutorial tut;
    private TextBox txt;

    [SerializeField] private GameObject dotAnim;
    [SerializeField] private GameObject slider;

    [SerializeField] private GameObject[] obj;

    [SerializeField] private Animator rewindAnim;

    [SerializeField] private Image waveformObj;

    [SerializeField] private AudioSource rewindAudio;
    [SerializeField] private AudioSource rewindReverseAudio;

    [SerializeField] private string camTrigger;

    [SerializeField] private int stagePhase;

    [SerializeField] private bool isThinking;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool rewindOnce;

    private void Start()
    {
        gameMng = GameObject.Find("GameManager").GetComponent<GameManager>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraCtrl>();

        tut = GameObject.Find("Scene").GetComponent<Tutorial>();
        txt = GameObject.Find("TextBox").GetComponent<TextBox>();
    }

    private void Update()
    {
        if (isThinking == true && tut.thought.isThinking == false)
        {
            isThinking = false;
        }
    }

    private void OnMouseEnter()
    {
        if (txt.isOpen != true && gameMng.isLocked != true)
        {
            if (tut.stage == 0)
            {
                switch (stagePhase)
                {
                    case 1:
                        gameMng.cursors.ChangeCursor("Slide", 1);
                        break;
                    case 2:
                        gameMng.cursors.ChangeCursor("Grab", 1);
                        break;
                }
            }
            else
            {
                switch (tut.stage)
                {
                    case 7:
                        gameMng.cursors.ChangeCursor("Inspect", 1);
                        break;
                    case 8:
                        if (stagePhase == 1 || stagePhase == 2)
                            gameMng.cursors.ChangeCursor("Inspect", 1);
                        else
                            gameMng.cursors.ChangeCursor("Grab", 1);
                        break;
                    case 10:
                        if (tut.thought.isThinking == false || isThinking == true)
                            gameMng.cursors.ChangeCursor("Grab", 1);
                        break;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        if (tut.stage == 0)
        {
            switch (stagePhase)
            {
                case 1:
                    gameMng.cursors.ChangeCursor("Slide", 0);
                    break;
                case 2:
                    gameMng.cursors.ChangeCursor("Grab", 0);
                    break;
            }
        }
        else
        {
            switch (tut.stage)
            {
                case 7:
                    gameMng.cursors.ChangeCursor("Inspect", 0);
                    break;
                case 8:
                    if (stagePhase == 1 || stagePhase == 2)
                        gameMng.cursors.ChangeCursor("Inspect", 0);
                    else
                        gameMng.cursors.ChangeCursor("Grab", 0);
                    break;
                case 10:
                    if (tut.thought.isThinking == false || isThinking == true)
                        gameMng.cursors.ChangeCursor("Grab", 0);
                    break;
            }
        }
    }

    private void OnMouseUp()
    {
        if (tut.isLocked != true && txt.isOpen != true && gameMng.isLocked != true)
        {
            switch (tut.stage)
            {
                case 0:
                    StartCoroutine(NextPhase());
                    txt.isOpen = true;
                    switch (stagePhase)
                    {
                        case 1:
                            obj[0].SetActive(true);
                            StartCoroutine
                                (ObjectFade.FadeOut(obj[1].GetComponent<SpriteRenderer>(), 0));
                            StartCoroutine(txt.ShowText(2, 0));
                            gameMng.cursors.ChangeCursor("Slide", 0);
                            break;
                        case 2:
                            StartCoroutine(tut.StartPhoneStage());
                            gameMng.cursors.ChangeCursor("Grab", 0);
                            GetComponent<Collider2D>().enabled = false;
                            tut.uiPhone.GetComponent<Image>().enabled = true;
                            tut.stage++;
                            break;
                    }
                    break;
                case 8:
                    switch (stagePhase)
                    {
                        case 1:
                            cam.ObjectTransition();
                            cam.GetComponent<Animator>().SetTrigger(camTrigger);
                            GetComponent<Collider2D>().enabled = false;
                            GameObject.Find("Chair").GetComponent<Collider2D>().enabled = true;
                            StartCoroutine(Zoom());
                            break;
                        case 2:
                            cam.ObjectTransition();
                            cam.GetComponent<Animator>().SetTrigger(camTrigger);
                            GetComponent<Collider2D>().enabled = false;
                            GameObject.Find("backpack_prop").GetComponent<Collider2D>().enabled = true;
                            StartCoroutine(Zoom());
                            tut.thought.KeepThought();
                            tut.thought.text = "Why is this here?.";
                            StartCoroutine(tut.RewindBehaviour(0));
                            break;
                    }
                    break;
                case 10:
                    if (isThinking == false && tut.thought.isThinking == false)
                    {
                        tut.thought.KeepThought();
                        tut.thought.text = "Should see what happened earlier before touching anything..";
                        isThinking = true;
                    }
                    break;
            }
        }
    }

    public void Rewind()
    {
        if (isLocked == false && gameMng.isLocked == false)
        {
            audioCtrl.rewindAudio = rewindAudio;
            audioSlider.rewindAudio = rewindAudio;
            eyelids.timerSmall = rewindAnim;
            eyelids.dots = dotAnim;
            StartCoroutine(TimeToOpen());

            switch (stagePhase)
            {
                case 3:
                    StartCoroutine(tut.RewindBehaviour(1));
                    break;
            }
        }
    }

    public void RewindLock()
    {
        if (isLocked == false && gameMng.isLocked == false)
        {
            isLocked = true;
            gameMng.isLocked = true;
        }
    }

    public void RewindUnlock()
    {
        isLocked = false;
    }

    IEnumerator NextPhase()
    {
        yield return new WaitForEndOfFrame();
        stagePhase++;
    }

    IEnumerator Zoom()
    {
        yield return new WaitForEndOfFrame();
        cam.currentView++;
    }

    IEnumerator QuickHideCollider()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForEndOfFrame();
        GetComponent<Collider2D>().enabled = true;

    }

    IEnumerator TimeToOpen()
    {
        yield return new WaitForSeconds(0.1f);
        eyelids.Close(1);
        if (rewindOnce != true)
        {
            yield return new WaitForSeconds(2.0f);
            rewindReverseAudio.Play();
            yield return new WaitForSeconds(1);
            eyelids.pointer.SetTrigger("CabinetRewind");
            eyelids.timer.SetTrigger("Pressed");
            waveformObj.enabled = true;
            eyelids.Uncover(1);
            rewindOnce = true;
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            eyelids.mediaFunction = true;
            audioCtrl.pressedButtons[2].SetActive(true);
            slider.SetActive(true);
            rewindAudio.Play();
            eyelids.dots.SetActive(true);
        }
        else if (rewindOnce == true)
        {
            yield return new WaitForSeconds(2);
            eyelids.timer.SetTrigger("Pressed");
            eyelids.pointer.SetBool("Moving", true);
            eyelids.mediaFunction = true;
            audioCtrl.pressedButtons[2].SetActive(true);
            audioCtrl.MoreRewind();
            slider.SetActive(true);
            waveformObj.enabled = true;
            rewindAudio.Play();
            eyelids.dots.SetActive(true);
        }
    }
}
