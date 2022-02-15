using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectFade : MonoBehaviour
{
    public static IEnumerator FadeOut(GameObject sprite, int i, float f)
    {
        SpriteRenderer s = sprite.GetComponent<SpriteRenderer>();
        if (f == 1.0f)
            yield return new WaitForSeconds(1.0f);
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 1.0f;

        float elapsedTime = 0.0f;
        Color c = s.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            s.color = c;
        }
        if (i == 1)
        {
            yield return new WaitForSeconds(1.0f);
            sprite.SetActive(false);
        }
    }

    public static IEnumerator FadeIn(SpriteRenderer sprite)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 1.0f;

        float elapsedTime = 0.0f;
        Color c = sprite.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            sprite.color = c;
        }
    }

    public static IEnumerator FadeInUI(Image image, float f)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = f;

        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }

    public static IEnumerator FadeOutUI(Image image, float f)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = f;

        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }

    public static IEnumerator FadeInText(TextMeshProUGUI text)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 1.0f;

        float elapsedTime = 0.0f;
        Color c = text.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            text.color = c;
        }
    }

    public static IEnumerator FadeOutText(TextMeshProUGUI text)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 0.5f;

        float elapsedTime = 0.0f;
        Color c = text.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            text.color = c;
        }
    }
}
