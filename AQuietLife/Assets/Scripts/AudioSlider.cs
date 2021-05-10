using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    /*public Slider timeLine;

    public AudioSource cabinet;

    private bool timeLineOnDrag = false;

    private void Update()
    {
        if (timeLineOnDrag)
        {
            cabinet.timeSamples = (int)(cabinet.clip.samples * timeLine.value);
        }
        else
        {
            timeLine.value = (float)cabinet.timeSamples / (float)cabinet.clip.samples;
        }
    }

    public void TimeLineOnBeginDrag()
    {
        timeLineOnDrag = true;

        cabinet.Pause();
    }

    public void TimeLineOnEndDrag()
    {
        cabinet.Play();

        timeLineOnDrag = false;
    }*/

    public AudioSource rewindAudio;

    public Slider TimeLine;
    // Flag to know if we are draging the Timeline handle
    private bool TimeLineOnDrag = false;

    private void Start()
    {
        rewindAudio = null;
    }

    void Update()
    {
        if (rewindAudio != null)
        {
            if (TimeLineOnDrag)
            {

                rewindAudio.timeSamples = (int)(rewindAudio.clip.samples * TimeLine.value);

            }

            else
            {
                TimeLine.value = (float)rewindAudio.timeSamples / (float)rewindAudio.clip.samples;
            }
        }      
    }

    // Called by the event trigger when the drag begin
    public void TimeLineOnBeginDrag()
    {
        TimeLineOnDrag = true;

        rewindAudio.Pause();
    }


    // Called at the end of the drag of the TimeLine
    public void TimeLineOnEndDrag()
    {
        rewindAudio.Play();

        TimeLineOnDrag = false;
    }

}
