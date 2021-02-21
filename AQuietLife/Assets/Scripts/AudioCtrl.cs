﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public AudioSource rewindAudio;

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
                pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 1.5f);
                break;
            case 1:
                rewindAudio.pitch = -1.5f;
                pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 0.5f);
                break;
            case 2:
                rewindAudio.pitch = 1.0f;
                break;
            case 3:
                rewindAudio.pitch = 0.0f;
                break;
        }
    }
}
