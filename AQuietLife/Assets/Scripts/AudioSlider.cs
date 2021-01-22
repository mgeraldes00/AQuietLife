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

    public Slider TimeLine;
    // Flag to know if we are draging the Timeline handle
    private bool TimeLineOnDrag = false;

    void Update()
    {
        if (TimeLineOnDrag)
        {

            GetComponent<AudioSource>().timeSamples = (int)(GetComponent<AudioSource>().clip.samples * TimeLine.value);

        }

        else
        {
            TimeLine.value = (float)GetComponent<AudioSource>().timeSamples / (float)GetComponent<AudioSource>().clip.samples;
        }
    }

    // Called by the event trigger when the drag begin
    public void TimeLineOnBeginDrag()
    {
        TimeLineOnDrag = true;

        GetComponent<AudioSource>().Pause();
    }


    // Called at the end of the drag of the TimeLine
    public void TimeLineOnEndDrag()
    {
        GetComponent<AudioSource>().Play();

        TimeLineOnDrag = false;
    }

}
