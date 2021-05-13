using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaPlayer : MonoBehaviour
{
    public Eyelids eyelids;

    public AudioSource rewindAudio;

    //public Animator[] audioButtons;

    public GameObject[] pressedButtons;

    public void ButtonBehaviour(int i)
    {
        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("Pitch Bend Mixer");
        rewindAudio.outputAudioMixerGroup = pitchBendGroup;

        if (eyelids.mediaFunction == true)
        {
            switch (i)
            {
                case 0:
                    rewindAudio.pitch = 1.5f;
                    /*audioButtons[0].SetBool("FF", true);
                    audioButtons[1].SetBool("FB", false);
                    audioButtons[2].SetBool("Play", false);
                    audioButtons[3].SetBool("Pause", false);*/
                    pressedButtons[0].SetActive(true);
                    pressedButtons[1].SetActive(false);
                    pressedButtons[2].SetActive(false);
                    pressedButtons[3].SetActive(false);
                    pressedButtons[4].SetActive(false);
                    eyelids.pointer.speed = 1.5f;
                    pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 1.5f);
                    break;
                case 1:
                    rewindAudio.pitch = -1.5f;
                    /*audioButtons[0].SetBool("FF", false);
                    audioButtons[1].SetBool("FB", true);
                    audioButtons[2].SetBool("Play", false);
                    audioButtons[3].SetBool("Pause", false);*/
                    pressedButtons[0].SetActive(false);
                    pressedButtons[1].SetActive(true);
                    pressedButtons[2].SetActive(false);
                    pressedButtons[3].SetActive(false);
                    pressedButtons[4].SetActive(false);
                    eyelids.pointer.StartPlayback();
                    eyelids.pointer.speed = -1.5f;
                    pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 0.5f);
                    break;
                case 2:
                    rewindAudio.pitch = 1.0f;
                    /*audioButtons[0].SetBool("FF", false);
                    audioButtons[1].SetBool("FB", false);
                    audioButtons[2].SetBool("Play", true);
                    audioButtons[3].SetBool("Pause", false);*/
                    pressedButtons[0].SetActive(false);
                    pressedButtons[1].SetActive(false);
                    pressedButtons[2].SetActive(true);
                    pressedButtons[3].SetActive(false);
                    pressedButtons[4].SetActive(false);
                    eyelids.pointer.speed = 1.0f;
                    break;
                case 3:
                    rewindAudio.pitch = 0.0f;
                    /*audioButtons[0].SetBool("FF", false);
                    audioButtons[1].SetBool("FB", false);
                    audioButtons[2].SetBool("Play", false);
                    audioButtons[3].SetBool("Pause", true);*/
                    pressedButtons[0].SetActive(false);
                    pressedButtons[1].SetActive(false);
                    pressedButtons[2].SetActive(false);
                    pressedButtons[3].SetActive(true);
                    pressedButtons[4].SetActive(false);
                    eyelids.pointer.speed = 0.0f;
                    break;
                case 4:
                    rewindAudio.pitch = 0.0f;
                    rewindAudio.Stop();
                    /*audioButtons[0].SetBool("FF", false);
                    audioButtons[1].SetBool("FB", false);
                    audioButtons[2].SetBool("Play", false);
                    audioButtons[3].SetBool("Pause", false);*/
                    pressedButtons[0].SetActive(false);
                    pressedButtons[1].SetActive(false);
                    pressedButtons[2].SetActive(false);
                    pressedButtons[3].SetActive(false);
                    pressedButtons[4].SetActive(true);
                    eyelids.Open();
                    break;
            }
        }      
    }

    public void MoreRewind()
    {
        rewindAudio.pitch = 1.0f;
        eyelids.pointer.speed = 1.0f;
    }
}
