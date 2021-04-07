using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eyelids : MonoBehaviour
{
    public GameManager gameMng;

    public Animator eyelids;
    public Animator timerSmall;
    public Animator timer;
    public Animator pointer;
    public Animator text;

    public AudioSource tick;
    public AudioSource tickReverse;

    public GameObject[] ctrlButtons;

    public GameObject slider;
    public GameObject returnArrow;

    public Image[] waveform;
    public Image[] rewindClock;

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
        //timerSmall.SetTrigger("RewindStart");
        timerSmall.SetBool("Rewind", true);
        StartCoroutine(StartRewind());
    }

    public void Open()
    {
        timer.SetTrigger("Pressed");
        StartCoroutine(TimerPress());        
    }

    IEnumerator StartRewind()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < rewindClock.Length; i++)
            rewindClock[i].enabled = true;
        timer.SetBool("Rewind", true);
        timer.SetTrigger("RewindStart");
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
        yield return new WaitForSeconds(0.2f);
        text.SetBool("Working", false);
        timer.SetTrigger("RewindEnd");
        timerSmall.SetBool("Rewind", false);
        pointer.SetBool("Moving", false);
        slider.SetActive(false);
        for (int i = 0; i < waveform.Length; i++)
            waveform[i].enabled = false;
        for (int b = 0; b < ctrlButtons.Length; b++)
            ctrlButtons[b].SetActive(false);
        yield return new WaitForSeconds(1.2f);
        eyelids.SetTrigger("Open");     
        gameMng.isLocked = false;
        returnArrow.SetActive(true);
    }
}
