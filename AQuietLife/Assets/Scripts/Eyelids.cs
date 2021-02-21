using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelids : MonoBehaviour
{
    public Animator eyelids;
    public Animator timer;

    public AudioSource tick;
    public AudioSource tickReverse;

    public GameObject[] ctrlButtons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        eyelids.SetTrigger("Close");
        timer.SetTrigger("RewindStart");
        timer.SetBool("Rewind", true);
        //StartCoroutine(TimerPress());
    }

    public void Open()
    {
        eyelids.SetTrigger("Open");
        timer.SetTrigger("RewindEnd");
    }

    IEnumerator TimerPress()
    {
        /*yield return new WaitForSeconds(2);
        timer.SetTrigger("Pressed");
        tickReverse.Play();
        yield return new WaitForSeconds(2);
        timer.SetTrigger("Pressed");
        tickReverse.Stop();
        tick.Play();
        yield return new WaitForSeconds(10);
        timer.SetTrigger("Pressed");
        timer.SetBool("Rewind", false);
        tick.Stop();*/
        yield return new WaitForSeconds(2);
        for (int i = 0; i < ctrlButtons.Length; i++)
            ctrlButtons[i].SetActive(true);
        timer.SetTrigger("Pressed");
    }
}
