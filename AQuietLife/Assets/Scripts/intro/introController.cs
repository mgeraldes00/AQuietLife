using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroController : MonoBehaviour
{
    public ThoughtManager thought;

    public Text thoughtText;

    public Animator cameraAnim;
    public Animator fadeAnim;
    public Animator shutterAnim;
    public Animator deathAnim;

    public GameObject returnArrow;
    public GameObject[] introTextObj;
    public GameObject directionalButton;
    public GameObject introCover;
    public GameObject sceneCloseUp;

    public GameObject[] phone;
    public GameObject[] deathScreen;

    public BoxCollider2D[] firstPanelCollide;

    [SerializeField]
    private int currentPanel;

    private bool isLocked;
    private bool isDead;

    private float delay = 0.04f;
    private float dotDelay = 1.0f;

    /*[SerializeField]
    private string introText;
    [SerializeField]
    private string introTextPart2;*/
    [SerializeField]
    private string[] introText;

    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        currentPanel = -2;
        StartCoroutine(IntroStart());
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

            if (hit.collider.CompareTag("NoTag"))
            {
                thought.ShowThought();

                switch (currentPanel)
                {
                    case -1:
                        phone[0].SetActive(false);
                        returnArrow.SetActive(true);
                        thoughtText.text = "I'm so tired...";
                        break;
                    case 0:
                        thoughtText.text = "Just gonna put something on before leaving.";
                        break;
                    case 1:
                        thoughtText.text = "Need some light in here.";
                        break;
                    case 2:
                        thoughtText.text = "What's this doing here?";
                        cameraAnim.SetTrigger("ZoomIn");
                        isLocked = true;
                        StartCoroutine(EndTransition());
                        break;
                    case 3:
                        Die();
                        break;
                }
            }
            
            if (hit.collider.CompareTag("Nothing"))
            {
                switch (currentPanel)
                {
                    case 0:
                        thought.ShowThought();
                        thoughtText.text = "Need some light in here.";
                        break;
                    case 1:                     
                        shutterAnim.SetTrigger("Open");
                        isLocked = true;
                        StartCoroutine(EndTransition());
                        break;
                }            
            }
        }
    }

    public void ButtonBehaviour(int i)
    {
        if (isLocked == false)
        {
            switch (i)
            {
                case 0:
                    phone[1].SetActive(false);
                    returnArrow.SetActive(false);
                    directionalButton.SetActive(true);
                    for (int b = 0; b < firstPanelCollide.Length; b++)
                        firstPanelCollide[b].enabled = true;
                    currentPanel++;
                    break;
                case 1:
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("turn");
                        fadeAnim.SetTrigger("Transition");
                        directionalButton.SetActive(false);
                    }
                    //currentPanel++;
                    isLocked = true;
                    StartCoroutine(EndTransition());
                    break;
            }
        }
    }

    public void Die()
    {
        isLocked = true;
        isDead = true;
        deathScreen[0].SetActive(true);
        //Destroy(scene);
        Destroy(sceneCloseUp);
        StartCoroutine(DeathProcess());
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
        currentPanel++;
    }

    IEnumerator DeathProcess()
    {
        yield return new WaitForSeconds(2);
        /*for (int i = 0; i < 4; i++)
            deathScreen[i].SetActive(true);
        deathAnim.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        deathScreen[4].SetActive(false);*/
        Application.Quit();
    }

    IEnumerator IntroStart()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < introText[0].Length; i++)
        {
            currentText = introText[0].Substring(0, i);
            introTextObj[0].GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < introText[1].Length; i++)
        {
            currentText = introText[1].Substring(0, i);
            introTextObj[1].GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        for (int i = 0; i < introText[2].Length; i++)
        {
            currentText = introText[2].Substring(0, i);
            introTextObj[2].GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(dotDelay);
        }
        introCover.SetActive(false);
        currentPanel++;
    }
}
