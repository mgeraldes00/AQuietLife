using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    public int lvl;

    void Awake()
    {
        if (instance == null && lvl == 0)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (lvl == 0)
            DontDestroyOnLoad(gameObject);
    }
}
