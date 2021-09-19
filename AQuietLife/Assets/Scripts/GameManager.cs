using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Video;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public InventoryManager inventory;
    public ThoughtManager thought;
    //public CabinetManager cabinet;
    //public BreadBoxManager breadBox;
    //public DrawerManager drawers;
    //public FridgeManager fridge;
    //public MicrowaveManager microwave;
    //public TableManager table;
    public PointerManager pointer;

    public UIFollowMouse cursors;

    public GameObject introText;

    public GameObject[] deathScreen;

    public GameObject canvas;

    public GameObject returnArrow;

    public GameObject audioMng;

    public Animator levelClock;
    public Animator deathAnim;

    public AudioSource backMusic;
    public AudioMixer musicMix;
    public AudioMixer dynamicMix;

    [SerializeField] private Animator fadeAnim;
    [SerializeField] private Animator cursorAnim;

    public bool returnable;

    public bool isLocked;

    public bool isDead;

    //public int numOfIngredients;
    //public int glovesUsed;

    private int currentMode;

    public int currLvl;

    // Start is called before the first frame update
    void Start()
    {
        cursors = FindObjectOfType<UIFollowMouse>();

        Cursor.visible = false;

        PlayerPrefs.SetString("StoryMode", "StoryMode");
        //PlayerPrefs.DeleteKey("StoryMode");

        if (PlayerPrefs.HasKey("StoryMode"))
        {
            if (currLvl == 2)
            {
                fadeAnim.speed = 0.3f;
                introText.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (currLvl == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (!PlayerPrefs.HasKey("StoryMode") && currLvl == 1)
        {
            fadeAnim.SetTrigger("FadeIn");
            Cursor.lockState = CursorLockMode.Locked;
        }

        returnable = true;
        isLocked = true;
        Instantiate(audioMng); 
        StartCoroutine(UnlockStart());
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 1, 1));
        //numOfIngredients = 0;
        //glovesUsed = 0;
        //startTutorial.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider == null)
        {
            //Nothing
        }

        else if (hit.collider.CompareTag("Table") || hit.collider.CompareTag("Drawers")
            || hit.collider.CompareTag("Cabinet") || hit.collider.CompareTag("BreadBox")
            || hit.collider.CompareTag("Fridge") || hit.collider.CompareTag("Microwave"))
        {
            Debug.Log("Object");
            //inspectionText.SetActive(true);          
        }

        else if (hit.collider.CompareTag("CabinetBreach"))
        {
            //inspectionText.SetActive(true);
        }

        else if (hit.collider.CompareTag("NoTag"))
        {

        }

        /*if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Table") ||
            Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Drawers") ||
            Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Cabinet") ||
            Input.GetMouseButtonDown(0) && hit.collider.CompareTag("BreadBox"))
        {
            inspectionText.SetActive(false);
            noTextCollidersGeneral.SetActive(false);
        }*/
    }

    public void Die()
    {
        //SceneManager.LoadScene("GameOver");
        backMusic.Stop();
        StartCoroutine(FadeMixerGroup.StartFade(dynamicMix, "DynamicVol", 1, 0));
        FindObjectOfType<AudioCtrl>().Play("ExplosionKitchen");
        isLocked = true;
        isDead = true;
        deathScreen[0].SetActive(true);
        deathScreen[5].SetActive(true);
        //Destroy(scene);
        //Destroy(sceneCloseUp);
        canvas.SetActive(false);
        StartCoroutine(DeathProcess());
    }

    public void Restart()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void ButtonBehaviour(int i)
    {
        switch (i)
        {
            case (0):
            default:
                SceneManager.LoadScene("Kitchen");
                break;
            case (1):
                /*context[0].SetActive(false);
                context[1].SetActive(true);
                contextButtons[0].SetActive(false);
                contextButtons[1].SetActive(true);*/
                deathScreen[6].GetComponent<Animator>().SetTrigger("FadeOut");
                StartCoroutine(RestartLevel(1));
                break;
            case (2):
                //SceneManager.LoadScene("Kitchen");
                deathScreen[6].GetComponent<Animator>().SetTrigger("FadeOut");
                StartCoroutine(RestartLevel(0));
                break;
            case (3):
                Debug.Log("Quit");
                Application.Quit();
                break;
            case (4):
                SceneManager.LoadScene("Intro");
                break;
        }
    }

    public void Unlock()
    {
        
        returnArrow.SetActive(true);
        isLocked = false;
        //gloveTutorial.SetActive(false);
        //objectTutorial.SetActive(false);
        //finalTutorial.SetActive(false);
    }

    public void ShowTutorial()
    {
        //objectTutorial.SetActive(true);
        Lock();
    }

    public void ShowGloveTutorial()
    {
        //gloveTutorial.SetActive(true);
        Lock();
    }

    public void ShowFinalTutorial()
    {
        //finalTutorial.SetActive(true);
        Lock();
    }

    public void Lock()
    {
        returnArrow.SetActive(false);
        isLocked = true;
    }

    IEnumerator UnlockStart()
    {
        if (PlayerPrefs.HasKey("StoryMode"))
        {
            switch (currLvl)
            {
                case 0:
                    yield return new WaitForSeconds(5.0f);
                    isLocked = false;
                    break;
                case 1:
                    yield return new WaitForEndOfFrame();
                    isLocked = false;
                    cursors.ChangeCursor("Open", 1);
                    break;
                case 2:
                    yield return new WaitForSeconds(4.0f);
                    introText.GetComponent<Animator>().SetTrigger("HideIntroText");
                    yield return new WaitForSeconds(0.5f);
                    levelClock.SetBool("Active", true);
                    yield return new WaitForSeconds(1.5f);
                    introText.SetActive(false);
                    isLocked = false;
                    //StartCoroutine(TimeTillLock());
                    yield return new WaitForSeconds(1.0f);
                    fadeAnim.speed = 1.0f;
                    break;
            }
        }
        else
        {
            if (currLvl == 1)
            {
                cursorAnim.runtimeAnimatorController = 
                    Resources.Load("Animation/Cursors/Cursors") as RuntimeAnimatorController;
            }
            yield return new WaitForSeconds(0.5f);
            levelClock.SetBool("Active", true);
            yield return new WaitForSeconds(2.0f);
            isLocked = false;
            cursors.ChangeCursor("Open", 1);
            yield return new WaitForSeconds(1.0f);
            Cursor.lockState = CursorLockMode.None;
            
        }
    }

    IEnumerator TimeTillLock()
    {
        yield return new WaitForSeconds(900);
        isLocked = true;
        yield return new WaitForSeconds(5);
        isLocked = true;
        yield return new WaitForSeconds(5);
        isLocked = true;
    }

    IEnumerator DeathProcess()
    {
        yield return new WaitForSeconds(1.9f);
        deathScreen[5].GetComponent<VideoPlayer>().Play();
        yield return new WaitForSeconds(0.1f);
        deathAnim.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        deathScreen[4].SetActive(false);
        yield return new WaitForEndOfFrame();
        deathScreen[1].SetActive(true);
        deathScreen[2].SetActive(true);
    }

    public IEnumerator BackToMenu()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 1.5f, 0));
        Time.timeScale = 1;
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(2.0f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator RestartLevel(int i)
    {
        yield return new WaitForSeconds(1.0f);
        if (i == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else if (i == 1)
            SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator LiberateReturn()
    {
        yield return new WaitForEndOfFrame();
        returnable = true;
    }

    public IEnumerator EndLevel(string levelName)
    {
        yield return new WaitForSeconds(1.2f);
        fadeAnim.SetTrigger("FadeOutDark");
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 1, 0));
        StartCoroutine(FadeMixerGroup.StartFade(dynamicMix, "DynamicVol", 1, 0));
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForEndOfFrame();
        fadeAnim.SetTrigger("FadeOut");
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 1, 0));
        StartCoroutine(FadeMixerGroup.StartFade(dynamicMix, "DynamicVol", 1, 0));
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("ThankYou");
    }

    public IEnumerator QuickUnlock(float i)
    {
        yield return new WaitForSeconds(i);
        isLocked = false;
    }
}
