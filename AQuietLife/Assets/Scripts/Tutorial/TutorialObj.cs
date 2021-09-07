using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObj : MonoBehaviour
{
    private GameManager gameMng;
    private Tutorial tut;
    private TextBox txt;

    [SerializeField] private GameObject[] obj;

    [SerializeField] private string camTrigger;

    [SerializeField] private int stagePhase;

    private void Start()
    {
        gameMng = GameObject.Find("GameManager").GetComponent<GameManager>();
        tut = GameObject.Find("Scene").GetComponent<Tutorial>();
        txt = GameObject.Find("TextBox").GetComponent<TextBox>();

        stagePhase = 1;
    }

    private void OnMouseEnter()
    {
        if (txt.isOpen != true)
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
    }

    private void OnMouseExit()
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
                            tut.stage++;
                            break;
                        case 5:
                            StartCoroutine
                                (ObjectFade.FadeOut(obj[0].GetComponent<SpriteRenderer>(), 0));
                            break;
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
}
