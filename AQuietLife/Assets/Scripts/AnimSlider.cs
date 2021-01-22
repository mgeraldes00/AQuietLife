using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimSlider : MonoBehaviour
{
    public Slider sliderScrubber;
    public Animator animator;

    public void Update()
    {
        float animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        Debug.Log("animationTime (normalized) is " + animationTime);
        sliderScrubber.value = animationTime;
    }

    public void OnValueChanged(float changedValue)
    {
        animator.Play("pointerMove", -1, sliderScrubber.normalizedValue);
    }

}
