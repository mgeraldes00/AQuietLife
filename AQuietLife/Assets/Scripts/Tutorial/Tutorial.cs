using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private CameraCtrl cam;

    [SerializeField] private GameObject startCover;

    [SerializeField] private GameObject[] phone;

    public GameObject blur;

    [SerializeField] private Animator uiPhone;
    [SerializeField] private Animator inventory;

    [SerializeField] private GameObject[] startTextObj;

    private float delay = 0.2f;
    private float dotDelay = 1.0f;

    [SerializeField] private string[] startText;

    private string currentText = "";

    public bool isOver;
    public bool isLocked;

    public int stage;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isLocked = true;

        uiPhone.SetBool("Enlarge", true);
        inventory.SetBool("Visible", false);

        for (int i = 0; i < cam.directionalArrows.Length; i++)
            cam.directionalArrows[i].SetTrigger("Hide");

        StartCoroutine(BlurCtrl.BlurScreen(blur));
        StartCoroutine(OpenCover());
    }

    IEnumerator OpenCover()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < startText[0].Length; i++)
        {
            currentText = startText[0].Substring(0, i);
            startTextObj[0].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < startText[1].Length; i++)
        {
            currentText = startText[1].Substring(0, i);
            startTextObj[1].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(dotDelay);
        }
        yield return new WaitForEndOfFrame();
        startCover.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(BlurCtrl.RemoveBlur(blur));
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isLocked = false;
        Destroy(startCover);
    }

    public IEnumerator PhoneStage()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine
            (ObjectFade.FadeOut(phone[1].GetComponent<SpriteRenderer>(), 0));
        GameObject.Find("Phone").GetComponent<Animator>().SetBool("Enlarge", false);
    }

    public IEnumerator QuickLock()
    {
        isLocked = true;
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
    }
}
