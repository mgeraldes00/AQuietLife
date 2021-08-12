﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFade : MonoBehaviour
{
    public static IEnumerator FadeOut(SpriteRenderer sprite)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 1.0f;

        float elapsedTime = 0.0f;
        Color c = sprite.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            sprite.color = c;
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

    public static IEnumerator FadeInUI(Image image)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 1.0f;

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

    public static IEnumerator FadeOutUI(Image image)
    {
        YieldInstruction fadeInstruction = new YieldInstruction();
        float fadeTime = 0.5f;

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
}
