using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public Animator anim;

    public AudioSource tick;

    public CabinetManager cabinet;

    void Start()
    {
        anim.speed = 0;
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Debug.Log("Time's Up!");
            cabinet.NoMoreTime();
        }
    }

    public void Drain()
    {
        anim.speed = 1;
        tick.Play();
        StartCoroutine(StopDrain());
    }

    IEnumerator StopDrain()
    {
        yield return new WaitForSeconds(2);
        anim.speed = 0;
        tick.Stop();
    }
}
