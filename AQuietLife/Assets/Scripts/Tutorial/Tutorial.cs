using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] phone;

    public bool isOver;

    public int stage;

    public IEnumerator PhoneStage()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine
            (ObjectFade.FadeOut(phone[1].GetComponent<SpriteRenderer>(), 0));
        GameObject.Find("Phone").GetComponent<Animator>().SetBool("Enlarge", false);
    }
}
