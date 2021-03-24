using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaPlayer : MonoBehaviour
{
    public Eyelids eyelids;

    public AudioSource rewindAudio;

    public Animator[] audioButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonBehaviour(int i)
    {
        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("Pitch Bend Mixer");
        rewindAudio.outputAudioMixerGroup = pitchBendGroup;

        switch (i)
        {
            case 0:
                rewindAudio.pitch = 1.5f;
                audioButtons[0].SetBool("FF", true);
                audioButtons[1].SetBool("FB", false);
                audioButtons[2].SetBool("Play", false);
                audioButtons[3].SetBool("Pause", false);
                eyelids.pointer.speed = 1.5f;
                pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 1.5f);
                break;
            case 1:
                rewindAudio.pitch = -1.5f;
                audioButtons[0].SetBool("FF", false);
                audioButtons[1].SetBool("FB", true);
                audioButtons[2].SetBool("Play", false);
                audioButtons[3].SetBool("Pause", false);
                eyelids.pointer.StartPlayback();
                eyelids.pointer.speed = -1.5f;
                pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 0.5f);
                break;
            case 2:
                rewindAudio.pitch = 1.0f;
                audioButtons[0].SetBool("FF", false);
                audioButtons[1].SetBool("FB", false);
                audioButtons[2].SetBool("Play", true);
                audioButtons[3].SetBool("Pause", false);
                eyelids.pointer.speed = 1.0f;
                break;
            case 3:
                rewindAudio.pitch = 0.0f;
                audioButtons[0].SetBool("FF", false);
                audioButtons[1].SetBool("FB", false);
                audioButtons[2].SetBool("Play", false);
                audioButtons[3].SetBool("Pause", true);
                eyelids.pointer.speed = 0.0f;
                break;
            case 4:
                rewindAudio.pitch = 0.0f;
                rewindAudio.Stop();
                audioButtons[0].SetBool("FF", false);
                audioButtons[1].SetBool("FB", false);
                audioButtons[2].SetBool("Play", false);
                audioButtons[3].SetBool("Pause", false);
                eyelids.Open();
                break;
        }
    }
}
