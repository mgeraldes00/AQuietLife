using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public float maxValue = 100;
    public float minValue = 0;

    [SerializeField]
    private float time;

    public float subtractTime = -5;

    public Animator anim;

    public AudioSource tick;

    public CabinetManager cabinet;
    public BreadBoxManager breadBox;
    public DrawerManager drawers;

    private bool draining;
    private bool drainingMore;

    void Start()
    {
        anim.speed = 0;
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Debug.Log("Time's Up!");
        }

        if (draining == true || drainingMore == true)
        {
            time = Mathf.Clamp(
                time - subtractTime * Time.deltaTime, minValue, maxValue);
        }
    }

    public void Drain()
    {
        draining = true;
        anim.speed = 1;
        tick.Play();      
        StartCoroutine(StopDrain());
    }

    public void DrainMore()
    {
        drainingMore = true;
        anim.speed = 1;
        tick.Play();
        StartCoroutine(StopDrainMore());
    }

    IEnumerator StopDrain()
    {
        yield return new WaitForSeconds(2);
        draining = false;
        anim.speed = 0;
        tick.Stop();
    }

    IEnumerator StopDrainMore()
    {
        yield return new WaitForSeconds(4);
        drainingMore = false;
        anim.speed = 0;
        tick.Stop();
    }

    void UpdateStatus()
    {
        if (!PlayerPrefs.HasKey("time"))
        {
            time = 100;
            PlayerPrefs.SetFloat("time", time);
        }
        else
        {
            time = PlayerPrefs.GetFloat("time");
        }
    }

    public float pTime
    {
        get { return time; }
        set { time = value; }
    }
}
