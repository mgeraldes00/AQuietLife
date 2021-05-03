using System.Collections;
using UnityEngine;

public class OvenManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpOven closeUp;
    public ThoughtManager thought;

    public GameObject[] objects;

    public GameObject returnArrow;
    public GameObject cloth;

    public BoxCollider2D glove;

    private bool isLocked;

    private void Start()
    {
        isLocked = true;
    }

    public void ButtonBehaviour()
    {
        closeUp.Normalize();
        StartCoroutine(TimeToTransition());
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        glove.enabled = false;
        returnArrow.SetActive(false);
        //rewindButton.SetActive(false);
    }

    public void LockAndUnlock()
    {
        if (isLocked == false)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock());
        }
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        returnArrow.SetActive(true);
    }
}
