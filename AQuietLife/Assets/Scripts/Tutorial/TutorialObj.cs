using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObj : MonoBehaviour
{
    private Tutorial tut;

    [SerializeField] private string camTrigger;

    [SerializeField] private GameObject obj;

    private void Start()
    {
        tut = GameObject.Find("Scene").GetComponent<Tutorial>();
    }

    private void OnMouseUp()
    {
        switch (tut.stage)
        {
            case 0:
                StartCoroutine 
                    (ObjectFade.FadeOut(obj.GetComponent<SpriteRenderer>(), 0));
                break;
        }
    }
}
