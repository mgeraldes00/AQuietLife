using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.EventSystems;

public class IntroController : MonoBehaviour
{
    public ThoughtManager thought;
    public IntroCursor cursor;

    public Animator cameraAnim;
    public Animator fadeAnim;
    public Animator shutterAnim;
    public Animator deathAnim;
    public Animator speechBalloon;
    public Animator rain;
    public Animator hmmAnim;

    public Image coverFull;

    public AudioSource alarm;
    public AudioSource rainS;
    public AudioSource shutter;
    public AudioSource[] hmm;
    public AudioSource past;

    public AudioMixer musicMix;
    public AudioMixer fxMix;

    public GameObject audioMng;

    public GameObject returnArrow;
    public GameObject cursorImg;
    public GameObject[] introTextObj;
    public GameObject directionalButton;
    public GameObject introCover;
    public GameObject sceneCloseUp;
    public GameObject skipText;

    public GameObject[] phone;
    public GameObject[] deathScreen;

    public BoxCollider2D[] firstPanelCollide;

    public int currentPanel;

    [SerializeField] private bool isLocked;

    public bool isDead;

    private float delay = 0.04f;
    private float dotDelay = 1.0f;

    /*[SerializeField]
    private string introText;
    [SerializeField]
    private string introTextPart2;*/
    [SerializeField]
    private string[] introText;

    private string currentText = "";

    private YieldInstruction fadeInstruction = new YieldInstruction();
    private float fadeTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(audioMng);

        StartCoroutine(FadeMixerGroup.StartFade(fxMix, "BackFX", 5, 1));

        currentPanel = -2;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(IntroStart());
        StartCoroutine(StartAlarm());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&  isLocked == false 
            && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null)
            {
                //Nothing
            }
            else if (hit.collider.CompareTag("NoTag"))
            {
                switch (currentPanel)
                {
                    case -1:
                        
                        break;
                    case 0:
                        thought.ShowThought();
                        thought.text = "Just gonna put something on before leaving..";
                        break;
                    case 1:
                        thought.ShowThought();
                        thought.text = "Need some light in here..";
                        break;
                    case 2:
                        thought.KeepThought();
                        thought.text = "What's this doing here?.";
                        cameraAnim.SetTrigger("ZoomIn");
                        isLocked = true;
                        StartCoroutine(EndTransition());
                        break;
                    case 3:
                        Die();
                        break;
                }
            }         
            else if (hit.collider.CompareTag("Nothing"))
            {
                switch (currentPanel)
                {
                    case 0:
                        thought.ShowThought();
                        thought.text = "Need some light in here..";
                        break;
                    case 1:                     
                        shutterAnim.SetTrigger("Open");
                        shutter.Play();
                        isLocked = true;
                        StartCoroutine(EndTransition());
                        break;
                }            
            }
        }
    }

    public void PhoneOff()
    {
        Cursor.SetCursor(cursor.defaultTexture, cursor.hotSpot, cursor.curMode);
        thought.ShowThought();
        phone[0].SetActive(false);
        phone[2].SetActive(true);
        returnArrow.SetActive(true);
        returnArrow.GetComponent<Animator>().SetTrigger("Show");
        thought.text = "I'm so tired....";
        alarm.Stop();  
    }

    public void ButtonBehaviour(int i)
    {
        if (isLocked == false || isDead == true)
        {
            switch (i)
            {
                case 0:
                    phone[1].SetActive(false);
                    returnArrow.GetComponent<Animator>().SetTrigger("Hide 0");
                    cameraAnim.SetTrigger("ZoomOut");
                    fadeAnim.SetTrigger("TransitionInteract");
                    directionalButton.SetActive(true);
                    for (int b = 0; b < firstPanelCollide.Length; b++)
                        firstPanelCollide[b].enabled = true;
                    currentPanel++;
                    break;
                case 1:
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("turn");
                        fadeAnim.SetTrigger("TransitionLeft");
                        rain.SetTrigger("Turn");
                    }
                    //currentPanel++;
                    isLocked = true;
                    StartCoroutine(EndTransition());
                    break;
                case 2:
                    skipText.GetComponent<Animator>().SetTrigger("Hide");
                    StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 0));
                    StartCoroutine(FadeIn(coverFull));
                    StartCoroutine(ChangeScene());
                    break;
            }
        }
    }

    public void Die()
    {
        isLocked = true;
        isDead = true;
        deathScreen[0].SetActive(true);
        rainS.Stop();
        //Destroy(scene);
        Destroy(sceneCloseUp);
        StartCoroutine(DeathProcess());
        StartCoroutine(PastText());
        FindObjectOfType<AudioCtrl>().Play("Explosion");
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 10, 1));
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
        currentPanel++;
        directionalButton.SetActive(false);
    }

    IEnumerator DeathProcess()
    {
        yield return new WaitForSeconds(4);
        //Debug.Log("EXIT");
        /*for (int i = 0; i < 4; i++)
            deathScreen[i].SetActive(true);
        deathAnim.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        deathScreen[4].SetActive(false);*/
        //Application.Quit();
        //SceneManager.LoadScene("Dialog"); 
        //FindObjectOfType<AudioCtrl>().Play("Past");
        past.Play();
        yield return new WaitForSeconds(8.0f);
        speechBalloon.SetTrigger("Past");
        hmmAnim.SetTrigger("1"); 
        yield return new WaitForSeconds(1.0f);
        hmm[0].Play();
        for (int i = 0; i < introText[4].Length; i++)
        {
            currentText = introText[4].Substring(0, i);
            introTextObj[4].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);      
        }
        yield return new WaitForSeconds(2.0f);
        currentText = "";
        introTextObj[4].GetComponent<TextMeshProUGUI>().text = currentText;
        yield return new WaitForSeconds(4.0f);
        speechBalloon.SetTrigger("L2R");
        hmmAnim.SetTrigger("2");
        yield return new WaitForSeconds(1.0f);
        hmm[1].Play();
        for (int i = 0; i < introText[5].Length; i++)
        {
            currentText = introText[5].Substring(0, i);
            introTextObj[4].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(4.0f);
        currentText = "";
        introTextObj[4].GetComponent<TextMeshProUGUI>().text = currentText;
        yield return new WaitForSeconds(2.0f);
        speechBalloon.SetTrigger("RTT");       
        yield return new WaitForSeconds(1.0f);
        hmm[2].Play();
        for (int i = 0; i < introText[6].Length; i++)
        {
            currentText = introText[6].Substring(0, i);
            introTextObj[4].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 0));
        skipText.GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(2.1f);
        SceneManager.LoadScene("Dialog");
    }

    IEnumerator PastText()
    {
        yield return new WaitForSeconds(6.0f);
        for (int i = 0; i < introText[3].Length; i++)
        {
            currentText = introText[3].Substring(0, i);
            introTextObj[3].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay * 3);
        }
        yield return new WaitForSeconds(1.0f);
        deathAnim.SetTrigger("Past");
        skipText.GetComponent<Animator>().SetTrigger("Show");
    }

    IEnumerator IntroStart()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < introText[0].Length; i++)
        {
            currentText = introText[0].Substring(0, i);
            introTextObj[0].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < introText[1].Length; i++)
        {
            currentText = introText[1].Substring(0, i);
            introTextObj[1].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        for (int i = 0; i < introText[2].Length; i++)
        {
            currentText = introText[2].Substring(0, i);
            introTextObj[2].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(dotDelay);
        }
        introCover.GetComponent<Animator>().SetTrigger("Open");
        currentPanel++;
        yield return new WaitForSeconds(1.0f);
        cursorImg.SetActive(true);
        introCover.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        cursorImg.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator StartAlarm()
    { 
        yield return new WaitForSeconds(5.8f);
        alarm.Play();
    }

    IEnumerator FadeIn(Image image)
    {
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

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2.1f);
        SceneManager.LoadScene("Dialog");
    }
}
