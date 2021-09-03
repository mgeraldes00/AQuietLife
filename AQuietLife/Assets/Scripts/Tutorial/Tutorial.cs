using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private CameraCtrl cam;

    [SerializeField] private GameObject startCover;

    [SerializeField] private GameObject[] phone;

    public GameObject blur;

    [SerializeField] private Animator uiPhone;
    [SerializeField] private Animator inventory;

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
        yield return new WaitForSeconds(4.0f);
        startCover.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(BlurCtrl.RemoveBlur(blur));
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isLocked = false;
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
