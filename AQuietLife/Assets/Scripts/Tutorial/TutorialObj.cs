using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObj : MonoBehaviour
{
    private Tutorial tut;

    [SerializeField] private GameObject[] obj;

    [SerializeField] private string camTrigger;

    [SerializeField] private int stagePhase;

    private void Start()
    {
        tut = GameObject.Find("Scene").GetComponent<Tutorial>();

        stagePhase = 1;
    }

    private void OnMouseUp()
    {
        switch (tut.stage)
        {
            case 0:
                StartCoroutine(NextPhase());
                switch (stagePhase)
                {
                    case 1:
                        obj[0].SetActive(true);
                        StartCoroutine
                            (ObjectFade.FadeOut(obj[1].GetComponent<SpriteRenderer>(), 0));
                        break;
                    case 2:
                        StartCoroutine(tut.PhoneStage());
                        break;
                    case 5:
                        StartCoroutine
                            (ObjectFade.FadeOut(obj[0].GetComponent<SpriteRenderer>(), 0));
                        break;
                }
                break;
        }
    }

    IEnumerator NextPhase()
    {
        yield return new WaitForEndOfFrame();
        stagePhase++;
    }
}
