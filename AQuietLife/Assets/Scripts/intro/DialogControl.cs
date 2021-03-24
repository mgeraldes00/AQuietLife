using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogControl : MonoBehaviour
{
    public GameObject introTextObj;
    public GameObject introCover;
    public GameObject returnArrow;

    public Animator fadeAnim;
    public Animator explosiveAnim;

    public SpriteRenderer[] dialog;
    public SpriteRenderer explosiveChar;

    private float delay = 0.1f;

    [SerializeField]
    private string introText;

    private string currentText = "";

    [SerializeField]
    private int sequenceNum;

    // Start is called before the first frame update
    void Start()
    {
        sequenceNum = -1;
        StartCoroutine(DialogStart());
    }

    // Update is called once per frame
    void Update()
    {
        switch (sequenceNum)
        {
            case 1:
                dialog[0].enabled = false;
                dialog[1].enabled = true;
                break;
            case 2:
                dialog[1].enabled = false;
                dialog[2].enabled = true;
                break;
            case 3:
                dialog[2].enabled = false;
                dialog[3].enabled = true;
                break;
            case 4:
                dialog[3].enabled = false;
                dialog[4].enabled = true;
                break;
            case 5:
                dialog[4].enabled = false;
                dialog[5].enabled = true;
                break;
            case 6:
                dialog[5].enabled = false;
                returnArrow.SetActive(false);
                StartCoroutine(TransitionToLevel());
                break;
        }
    }

    public void ButtonBehaviour()
    {
        sequenceNum++;       
    }

    IEnumerator DialogStart()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < introText.Length; i++)
        {
            currentText = introText.Substring(0, i);
            introTextObj.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1.0f);
        //introCover.SetActive(false);
        fadeAnim.SetTrigger("FadeIn");
        sequenceNum++;
        yield return new WaitForSeconds(1.0f);
        introTextObj.SetActive(false);
        explosiveChar.enabled = true;
        explosiveAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.0f);
        dialog[0].enabled = true;
        returnArrow.SetActive(true);
    }

    IEnumerator TransitionToLevel()
    {
        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Kitchen");
    }
}
