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
    public Animator splash;

    public Animator cover;
    public Animator timerSmall;
    public Animator timer;
    public Animator pointer;
    public Animator mediaPlayer;
    public Animator map;

    //public Animator text;

    public AudioSource tick;
    public AudioSource tickReverse;

    public AudioMixer musicMix;
    public AudioMixer objMix;

    public GameObject[] ctrlButtons;

    public GameObject slider;
    public GameObject dots;
    public GameObject returnArrow;

    public Image[] waveform;
    public Image[] rewindClock;

    public bool mediaFunction;

    public void Close()
    {
        //eyelids.SetTrigger("Close");
        splash.SetTrigger("Rewind");
        returnArrow.GetComponent<Animator>().SetBool("Hide 0", true);
        //timerSmall.SetTrigger("RewindStart");
        timerSmall.SetBool("Visible", false);
        media.dirButtons.SetActive(false);
        StartCoroutine(StartRewind());
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 0));
        StartCoroutine(FadeMixerGroup.StartFade(objMix, "DynamicVol", 2, 0.5f));
    }

    public void Open()
    {
        timer.SetTrigger("Pressed");
        StartCoroutine(TimerPress());
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 1));
        StartCoroutine(FadeMixerGroup.StartFade(objMix, "DynamicVol", 2, 1.0f));
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
        splash.SetBool("Rewinding", true);
        for (int i = 0; i < rewindClock.Length; i++)
            rewindClock[i].enabled = true;
        timer.SetBool("Rewind", true);
        timer.SetTrigger("RewindStart");
        map.SetBool("Active", true);
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
        splash.SetBool("Rewinding", false);
        //text.SetBool("Working", false);
        timer.SetTrigger("RewindEnd");
        map.SetBool("Active", false);
        pointer.SetBool("Moving", false);
        slider.SetActive(false);
        for (int i = 0; i < waveform.Length; i++)
            waveform[i].enabled = false;
        /*for (int b = 0; b < ctrlButtons.Length; b++)
            ctrlButtons[b].SetActive(false);*/
        mediaFunction = false;
        mediaPlayer.SetBool("Rewinding", false);
        dots.SetActive(false);
        for (int c = 0; c < media.pressedButtons.Length; c++)
            media.pressedButtons[c].SetActive(false);
        yield return new WaitForSeconds(1.0f);       
        for (int i = 0; i < rewindClock.Length; i++)
            rewindClock[i].enabled = false;
        yield return new WaitForSeconds(0.25f);
        //eyelids.SetTrigger("Open");
        yield return new WaitForSeconds(0.25f);
        gameMng.isLocked = false;
        returnArrow.SetActive(true);
        timerSmall.SetBool("Visible", true);
        returnArrow.GetComponent<Animator>().SetTrigger("Show");
    }
}
