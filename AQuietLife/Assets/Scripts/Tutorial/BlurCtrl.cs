using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurCtrl : MonoBehaviour
{
    public static IEnumerator BlurScreen(GameObject rend)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 1.0f;

        float elapsedTime = 0.0f;

        float maxBlurValue = 5;
        float minBlurValue = 0;
        float blurValue = 0;
        float bps;
        float blurTime = -1;

        bps = (minBlurValue - maxBlurValue) / blurTime;

        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            blurValue =
                Mathf.Clamp(blurValue + bps * Time.deltaTime, minBlurValue, maxBlurValue);
            rend.GetComponent<Image>().material.SetFloat("_Size", blurValue);
        }
    }

    public static IEnumerator RemoveBlur(GameObject rend)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 1.0f;

        float elapsedTime = 0.0f;

        float maxBlurValue = 5;
        float minBlurValue = 0;
        float blurValue = 5;
        float bpsDown;
        float blurTimeDown = 1;

        bpsDown = (minBlurValue - maxBlurValue) / blurTimeDown;

        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            blurValue =
                Mathf.Clamp(blurValue + bpsDown * Time.deltaTime, minBlurValue, maxBlurValue);
            rend.GetComponent<Image>().material.SetFloat("_Size", blurValue);
        }
    }
}
