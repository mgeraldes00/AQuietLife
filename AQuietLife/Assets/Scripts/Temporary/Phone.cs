using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Phone : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager gameMng;
    public Tutorial tut;

    public Animator phone;
    public Animator actualPhone;

    public GameObject rend;

    public GameObject[] screens;

    public GameObject[] buttons;

    [SerializeField] private GameObject[] arrows;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject[] messagesClosed;
    [SerializeField] private GameObject phoneMask;

    [SerializeField] private Sprite[] notif;
    [SerializeField] private Sprite[] select;
    [SerializeField] private Sprite msgOpened;

    AudioLowPassFilter filterAudio;
    public GameObject backMusic;
    public float frequencyLerped;
    public float audioChange;
    public float audioChangeUp;

    public AudioMixer dynamicMix;

    private float maxBlurValue = 5;
    private float minBlurValue = 0;
    [SerializeField] private float blurValue = 10;
    private float bps;
    private float bpsDown;
    private float blurTime = -1;
    private float blurTimeDown = 1;

    [SerializeField] private bool phoneSelect;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool hasNotif;
    [SerializeField] private bool isMsg;
    [SerializeField] private bool isActive;

    public bool isLockedFromTut;
    public bool optionsLocked;
    public bool messagesLocked;
    public bool exitLocked;

    [SerializeField] private int currentScreen;

    private void Awake()
    {
        if (gameMng.currLvl == 1)
        {
            backMusic = GameObject.Find("BackMusic");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //FindObjectOfType<PointerManager>().ChangeCursor(2);
        gameMng.cursors.ChangeCursor("Point", 1);
        if (hasNotif == true)
            gameObject.GetComponent<Image>().sprite = select[0];
        else
            gameObject.GetComponent<Image>().sprite = select[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //FindObjectOfType<PointerManager>().ChangeCursor(1);
        gameMng.cursors.ChangeCursor("Point", 0);
        if (hasNotif == true)
            gameObject.GetComponent<Image>().sprite = notif[0];
        else
            gameObject.GetComponent<Image>().sprite = notif[1];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //FindObjectOfType<PointerManager>().ChangeCursor(1);
        gameMng.cursors.ChangeCursor("Point", 0);
        if (gameMng.currLvl == 0 && tut.txt.isOpen == true)
        {
            //Do nothing
        }
        else if (isLocked == false)
        {
            gameMng.isLocked = true;
            StartCoroutine(RevealPhone());
            phoneSelect = true;
            isLocked = true;
            currentScreen = 0;
            StartCoroutine(FadeMixerGroup.StartFade(dynamicMix, "DynamicVol", 1, 0));

            if (FindObjectOfType<CameraCtrl>().currentView == 0)
            {
                arrows[0].GetComponent<Animator>().SetTrigger("Hide");
                arrows[1].GetComponent<Animator>().SetTrigger("Hide");
            }
            else
            {
                arrows[2].GetComponent<Animator>().SetBool("Hide 0", true);
            }

            inventory.GetComponent<Animator>().SetBool("Visible", false);

            StartCoroutine(FadeOutLowPass());
        }
    }

    private void Start()
    {
        isLocked = false;
        //hasNotif = true;

        bps = (minBlurValue - maxBlurValue) / blurTime;
        bpsDown = (minBlurValue - maxBlurValue) / blurTimeDown;
    }

    private void Update()
    {
        if (phoneSelect == true)
        {
            blurValue =
                Mathf.Clamp(blurValue + bps * Time.deltaTime, minBlurValue, maxBlurValue);
            rend.GetComponent<Image>().material.SetFloat("_Size", blurValue);
        }
        else if (phoneSelect == false && isLocked == true)
        {
            blurValue =
                Mathf.Clamp(blurValue + bpsDown * Time.deltaTime, minBlurValue, maxBlurValue);
            rend.GetComponent<Image>().material.SetFloat("_Size", blurValue);
        }

    }

    public void ButtonBehaviour(int i)
    {
        if (isLockedFromTut != true)
        {
            switch (i)
            {
                case 0:
                    if (exitLocked != true)
                    {
                        switch (currentScreen)
                        {
                            case 0:
                                if (phoneSelect == true)
                                {
                                    if (tut != null)
                                        if (tut.stage == 4)
                                            StartCoroutine(tut.PhoneBehaviour(3));
                                    Time.timeScale = 1.0f;
                                    StartCoroutine(FadeMixerGroup.StartFade(dynamicMix, "DynamicVol", 1, 1));
                                    phoneSelect = false;
                                    actualPhone.SetBool("ShowPhone", false);
                                    actualPhone.SetBool("Messages", false);
                                    for (int b = 0; b < buttons.Length; b++)
                                        buttons[b].SetActive(false);
                                    for (int c = 0; c < screens.Length; c++)
                                        screens[c].SetActive(false);
                                    StartCoroutine(Unlock());
                                    StartCoroutine(FadeInLowPass());
                                }
                                break;
                            case 1:
                                StartCoroutine(SwitchScreen());
                                if (isMsg == true)
                                {
                                    actualPhone.SetBool("Messages", false);
                                    isMsg = false;
                                }
                                else
                                {
                                    actualPhone.SetBool("Options", false);
                                }
                                break;
                            case 2:
                                actualPhone.SetBool("Msg1", false);
                                StartCoroutine(SwitchScreen());
                                break;
                        }
                    }
                    break;
                case 1:
                    if (messagesLocked != true)
                    {
                        if (tut != null)
                            if (tut.stage == 2)
                                StartCoroutine(tut.PhoneBehaviour(1));
                        screens[0].SetActive(true);
                        actualPhone.SetBool("Messages", true);
                        FindObjectOfType<AudioCtrl>().Play("PhoneGlide");
                        isMsg = true;
                        currentScreen++;
                    }
                    break;
                case 2:
                    if (messagesLocked != true)
                    {
                        if (tut != null)
                            if (tut.stage == 3)
                                StartCoroutine(tut.PhoneBehaviour(2));
                        screens[1].SetActive(true);
                        hasNotif = false;
                        GetComponent<Image>().sprite = notif[1];
                        messagesClosed[0].GetComponent<Image>().sprite = msgOpened;
                        actualPhone.SetBool("Msg1", true);
                        currentScreen++;
                    }
                    break;
                case 3:
                    if (optionsLocked != true)
                    {
                        screens[2].SetActive(true);
                        actualPhone.SetBool("Options", true);
                        FindObjectOfType<AudioCtrl>().Play("PhoneGlide");
                        currentScreen++;
                    }
                    break;
                case 4:
                    phoneSelect = false;
                    isLocked = true;
                    StartCoroutine(gameMng.BackToMenu());
                    break;
                case 5:
                    //msg2
                    break;
                case 6:
                    //msg3
                    break;
                case 7:
                    //Credits
                    buttons[1].GetComponent<Animator>().SetTrigger("Hide");
                    phoneMask.GetComponent<Animator>().SetTrigger("Hide");
                    StartCoroutine(Credits());
                    break;
                case 8:
                    //returnArrow
                    StartCoroutine(HideCredits());
                    break;
            }
        }
    }

    IEnumerator RevealPhone()
    {
        yield return new WaitForSeconds(0.1f);
        if (tut != null)
            if (tut.stage == 1)
                StartCoroutine(tut.PhoneBehaviour(0));
        Debug.Log("Phone");
        phone.SetBool("Enlarge", true);
        actualPhone.SetBool("ShowPhone", true);
        arrows[2].GetComponent<Animator>().SetBool("Hide 0", false);
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioCtrl>().Play("OpenPhone");
        GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(0.6f);
        buttons[1].SetActive(true);
        buttons[1].GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        for (int b = 0; b < buttons.Length; b++)
            buttons[b].SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        isActive = true;
        Time.timeScale = 0.0f;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("Unlock");
        if (gameMng.currLvl >= 1)
        {
            GetComponent<Image>().enabled = true;
            phone.SetBool("Enlarge", false);
        }
        if (FindObjectOfType<CameraCtrl>().currentView == 0)
        {
            arrows[0].GetComponent<Animator>().SetTrigger("Show");
            arrows[1].GetComponent<Animator>().SetTrigger("Show");
        }
        else
        {
            arrows[2].GetComponent<Animator>().SetTrigger("Show");
        }
        if (gameMng.currLvl >= 1)
        {
            inventory.GetComponent<Animator>().SetBool("Visible", true);
        }
        yield return new WaitForSeconds(0.5f);
        gameMng.isLocked = false;
        isLocked = false;
        yield return new WaitForSeconds(0.5f);
        isActive = false;
    }

    IEnumerator SwitchScreen()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        currentScreen--;
    }

    IEnumerator FadeOutLowPass()
    {
        do
        {
            filterAudio = backMusic.GetComponent<AudioLowPassFilter>();
            frequencyLerped = Mathf.SmoothDamp(frequencyLerped, 350, ref audioChange, 0.1f);
            filterAudio.cutoffFrequency = frequencyLerped;

            yield return null;
        }
        while (frequencyLerped > 350 && isActive == false);
    }

    IEnumerator FadeInLowPass()
    {
        do
        {
            filterAudio = backMusic.GetComponent<AudioLowPassFilter>();
            frequencyLerped = Mathf.SmoothDamp(frequencyLerped, 5000, ref audioChangeUp, 0.1f);
            filterAudio.cutoffFrequency = frequencyLerped;

            yield return null;
        }
        while (frequencyLerped < 5000 && isActive == true);
    }

    IEnumerator Credits()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        actualPhone.SetBool("Credits", true);
    }

    IEnumerator HideCredits()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        phoneMask.GetComponent<Animator>().SetTrigger("Show");
        actualPhone.SetBool("Credits", false);
        yield return new WaitForSecondsRealtime(0.5f);
        buttons[1].GetComponent<Animator>().SetTrigger("Show");
    }
}
