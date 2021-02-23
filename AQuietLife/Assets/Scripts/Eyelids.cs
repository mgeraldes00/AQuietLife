using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eyelids : MonoBehaviour
{
    public GameManager gameMng;

    public Animator eyelids;
    public Animator timer;
    public Animator pointer;

    public AudioSource tick;
    public AudioSource tickReverse;

    public GameObject[] ctrlButtons;

    public GameObject slider;
    public GameObject returnArrow;

    public Image[] waveform;

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
        timer.SetTrigger("Pressed");
        StartCoroutine(TimerPress());        
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
        eyelids.SetTrigger("Open");
        timer.SetTrigger("RewindEnd");
        pointer.SetBool("Moving", false);
        slider.SetActive(false);
        for (int i = 0; i < waveform.Length; i++)
            waveform[i].enabled = false;
        for (int b = 0; b < ctrlButtons.Length; b++)
            ctrlButtons[b].SetActive(false);
        gameMng.isLocked = false;
        returnArrow.SetActive(true);
    }
}
