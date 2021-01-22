using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Texture2D PaintWaveformSpectrum(AudioClip audio, float saturation, int width, int height, Color col)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        float[] samples = new float[audio.samples];
        float[] waveform = new float[width];
        audio.GetData(samples, 0);
        int packSize = (audio.samples / width) + 1;
        int s = 0;
        for (int i = 0; i < audio.samples; i += packSize)
        {
            waveform[s] = Mathf.Abs(samples[i]);
            s++;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tex.SetPixel(x, y, Color.black);
            }
        }

        for (int x = 0; x < waveform.Length; x++)
        {
            for (int y = 0; y <= waveform[x] * ((float)height * .75f); y++)
            {
                tex.SetPixel(x, (height / 2) + y, col);
                tex.SetPixel(x, (height / 2) - y, col);
            }
        }
        tex.Apply();

        return tex;
    }

    /*public static float[] GetWaveform(AudioClip audio, int size, float sat)
    {
        float[] samples = new float[audio.samples];
        float[] waveform = new float[size];
        audio.GetData(samples, 0);
        int packSize = audio.samples / size;

        float max = 0f;
        int c = 0;
        int s = 0;
        for (int i = 0; i < 2 * audio.samples; i++)
        {
            waveform[c] += Mathf.Abs(samples[i]);
            s++;
            if (s > packSize)
            {
                if (max < waveform[c])
                    max = waveform[c];
                c++;
                s = 0;
            }
        }
        for (int i = 0; i < size; i++)
        {
            waveform[i] /= (max * sat);
            if (waveform[i] > 1f)
                waveform[i] = 1f;
        }

        return waveform;
    }

    public static Texture2D PaintWaveformSpectrum(float[] waveform, int height, Color c)
    {
        Texture2D tex = new Texture2D(waveform.Length, height, TextureFormat.RGBA32, false);

        for (int x = 0; x < waveform.Length; x++)
        {
            for (int y = 0; y <= waveform[x] * (float)height / 2f; y++)
            {
                tex.SetPixel(x, (height / 2) + y, c);
                tex.SetPixel(x, (height / 2) - y, c);
            }
        }
        tex.Apply();

        return tex;
    }*/
}
