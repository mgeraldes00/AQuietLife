using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindBehaviour : MonoBehaviour
{
    private GameManager gameMng;

    private Eyelids eyelids;
    private MediaPlayer audioCtrl;
    private AudioSlider audioSlider;

    [SerializeField] private Animator rewindAnim;

    public Image waveformObj;

    public GameObject dotAnim;

    [SerializeField] private GameObject slider;

    public AudioSource rewindAudio;
    [SerializeField] private AudioSource rewindReverseAudio;

    public bool hasAudio;
    public bool rewindOnce;

    private void Awake()
    {
        gameMng = FindObjectOfType<GameManager>();

        eyelids = FindObjectOfType<Eyelids>();
        audioCtrl = FindObjectOfType<MediaPlayer>();
    }

    public void MRewind()
    {
        if (gameMng.isLocked != true)
        {
            audioCtrl.rewindAudio = rewindAudio;
            audioSlider.rewindAudio = rewindAudio;
            eyelids.timerSmall = rewindAnim;
            eyelids.dots = dotAnim;

            StartCoroutine(Rewind());
        }
    }

    public IEnumerator Rewind()
    {
        yield return new WaitForSeconds(0.1f);
        eyelids.Close(1);

        if (hasAudio == true)
        {
            if (rewindOnce != true)
            {
                yield return new WaitForSeconds(2.0f);
                rewindReverseAudio.Play();
                yield return new WaitForSeconds(1);
                eyelids.pointer.SetTrigger("CabinetRewind");
                eyelids.timer.SetTrigger("Pressed");
                waveformObj.enabled = true;
                eyelids.Uncover(1);
                rewindOnce = true;
                yield return new WaitForSeconds(2);
                eyelids.timer.SetTrigger("Pressed");
                eyelids.pointer.SetBool("Moving", true);
                eyelids.mediaFunction = true;
                audioCtrl.pressedButtons[2].SetActive(true);
                slider.SetActive(true);
                rewindAudio.Play();
                eyelids.dots.SetActive(true);
            }
            else if (rewindOnce == true)
            {
                yield return new WaitForSeconds(2.0f);
                eyelids.timer.SetTrigger("Pressed");
                eyelids.pointer.SetBool("Moving", true);
                eyelids.mediaFunction = true;
                audioCtrl.pressedButtons[2].SetActive(true);
                audioCtrl.MoreRewind();
                slider.SetActive(true);
                waveformObj.enabled = true;
                rewindAudio.Play();
                eyelids.dots.SetActive(true);
            }
        }
        else if (hasAudio != true)
        {

        }
    }
}
