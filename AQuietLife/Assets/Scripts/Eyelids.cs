using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Eyelids : MonoBehaviour
{
    public GameManager gameMng;
    public MediaPlayer media;

    public Animator eyelids;
    public Animator cover;
    public Animator timerSmall;
    public Animator timer;
    public Animator pointer;
    public Animator mediaPlayer;
    public Animator text;

    public AudioSource tick;
    public AudioSource tickReverse;

    public AudioMixer musicMix;

    public GameObject[] ctrlButtons;

    public GameObject slider;
    public GameObject returnArrow;

    public Image[] waveform;
    public Image[] rewindClock;

    public bool mediaFunction;

    public void Close()
    {
        eyelids.SetTrigger("Close");
        returnArrow.GetComponent<Animator>().SetBool("Hide 0", true);
        //timerSmall.SetTrigger("RewindStart");
        timerSmall.SetBool("Visible", false);
        media.dirButtons.SetActive(false);
        StartCoroutine(StartRewind());
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 0));
    }

    public void Open()
    {
        timer.SetTrigger("Pressed");
        StartCoroutine(TimerPress());
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 1));
    }

    public void Uncover()
    {
        cover.SetTrigger("FirstRewind");
    }

    IEnumerator StartRewind()
    {
        yield return new WaitForEndOfFrame();
        returnArrow.GetComponent<Animator>().SetBool("Hide 0", false);
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < rewindClock.Length; i++)
            rewindClock[i].enabled = true;
        timer.SetBool("Rewind", true);
        timer.SetTrigger("RewindStart");
        yield return new WaitForSeconds(1.0f);
        mediaPlayer.SetBool("Rewinding", true);
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
        //text.SetBool("Working", false);
        timer.SetTrigger("RewindEnd");       
        pointer.SetBool("Moving", false);
        slider.SetActive(false);
        for (int i = 0; i < waveform.Length; i++)
            waveform[i].enabled = false;
        /*for (int b = 0; b < ctrlButtons.Length; b++)
            ctrlButtons[b].SetActive(false);*/
        mediaFunction = false;
        mediaPlayer.SetBool("Rewinding", false);
        for (int c = 0; c < media.pressedButtons.Length; c++)
            media.pressedButtons[c].SetActive(false);
        yield return new WaitForSeconds(1.5f);       
        for (int i = 0; i < rewindClock.Length; i++)
            rewindClock[i].enabled = false;     
        yield return new WaitForSeconds(0.5f);
        eyelids.SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        gameMng.isLocked = false;
        returnArrow.SetActive(true);
        timerSmall.SetBool("Visible", true);
        returnArrow.GetComponent<Animator>().SetTrigger("Show");
    }
}
