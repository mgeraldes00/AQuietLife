using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtManager : MonoBehaviour
{
    public Animator balloon;

    public bool isThinking;

    public void ShowThought()
    {
        if (isThinking == false)
        {
            balloon.SetTrigger("Appear");
            isThinking = true;
            StartCoroutine(TimeToDisappear());
        }                 
    }

    IEnumerator TimeToDisappear()
    {
        yield return new WaitForSeconds(2.5f);
        balloon.SetTrigger("Disappear");
        isThinking = false;
    }
}
