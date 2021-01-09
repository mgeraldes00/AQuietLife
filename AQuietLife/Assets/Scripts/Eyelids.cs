using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelids : MonoBehaviour
{
    public Animator eyelids;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        eyelids.SetTrigger("Close");
    }

    public void Open()
    {
        eyelids.SetTrigger("Open");
    }
}
