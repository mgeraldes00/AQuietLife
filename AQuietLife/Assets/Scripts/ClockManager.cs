using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public Animator anim;

    public CabinetManager cabinet;

    void Start()
    {
        anim.speed = 0;
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
        {
            Debug.Log("End");
            cabinet.NoMoreTime();
        }
    }

    public void Drain()
    {
        anim.speed = 1;
        StartCoroutine(StopDrain());
    }

    IEnumerator StopDrain()
    {
        yield return new WaitForSeconds(2);
        anim.speed = 0;
    }
}
