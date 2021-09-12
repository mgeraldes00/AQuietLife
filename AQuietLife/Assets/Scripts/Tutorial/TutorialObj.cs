using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialObj : MonoBehaviour
{
    private GameManager gameMng;
    private CameraCtrl cam;

    private Tutorial tut;
    private TextBox txt;

    [SerializeField] private GameObject[] obj;

    [SerializeField] private string camTrigger;

    [SerializeField] private int stagePhase;

    [SerializeField] private bool isThinking;

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
        if (txt.isOpen != true)
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
        if (tut.isLocked != true && txt.isOpen != true)
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
}
