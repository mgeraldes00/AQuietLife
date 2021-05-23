using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float startTime = 5f;
    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] TextMeshProUGUI timerText1;
    [SerializeField] TextMeshProUGUI timerText2;

    float timer1 = 0f;
    float timer2 = 0f;

    private void Start()
    {
        StartCoroutine(Timer1());
        //StartCoroutine(Timer2());
    }

    private IEnumerator Timer1()
    {
        timer1 = startTime;

        do
        {
            timer1 -= Time.deltaTime;

            slider1.value = 1 - timer1 / startTime;

            FormatText1();

            yield return null;
        }
        while (timer1 > 0);

        if (timer1 <= 0)
        {
            Debug.Log("Time's up!");
        }
    }

    private void FormatText1()
    {
        int minutes = (int)(timer1 / 60) % 60;
        int seconds = (int)(timer1 % 60);

        timerText1.text = "";
        if (minutes > 0) { timerText1.text += minutes + "m"; }
        if (seconds > 0) { timerText1.text += seconds + "s"; }
    }

    private IEnumerator Timer2()
    {
        timer2 = 0;

        do
        {
            timer2 += Time.deltaTime;

            slider2.value = timer2 / startTime;

            FormatText2();

            yield return null;
        }
        while (timer2 < startTime);
    }

    private void FormatText2()
    {
        int minutes = (int)(timer2 / 60) % 60;
        float seconds = (timer2 % 60);

        string secondsString = seconds.ToString("F3");

        timerText2.text = minutes + "m" + secondsString;
    }
}
