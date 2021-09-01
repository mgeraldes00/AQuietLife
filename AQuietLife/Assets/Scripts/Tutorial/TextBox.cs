using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Animator txtAnim;
    [SerializeField] private GameObject txtObj;
    [SerializeField] private GameObject[] button;

    private float delay = 0.01f;

    public string txt;
    public string currentTxt = "";

    public IEnumerator ShowText(int i)
    {
        txtAnim.SetBool("Active", true);
        StartCoroutine(ShowButton(i));

        yield return new WaitForSeconds(0.5f);
        txtObj.GetComponent<TextMeshProUGUI>().raycastTarget = true;
        for (int c = 0; c < txt.Length; c++)
        {
            currentTxt = txt.Substring(0, c);
            txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    public IEnumerator HideText()
    {
        txtAnim.SetBool("Active", false);
        txt = "";
        currentTxt = "";
        yield return new WaitForSecondsRealtime(1.0f);
        txtObj.GetComponent<TextMeshProUGUI>().text = currentTxt;
    }

    IEnumerator ShowButton(int i)
    {
        yield return new WaitForSeconds(1.0f);
        if (i == 1)
            button[0].GetComponent<Animator>().SetTrigger("Show");
    }
}
