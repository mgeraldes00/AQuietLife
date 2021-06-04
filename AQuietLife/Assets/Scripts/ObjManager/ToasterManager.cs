using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterManager : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CloseUpToaster closeUp;
    public ThoughtManager thought;

    public GameObject[] objects;

    public GameObject returnArrow;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;

    private void Start()
    {
        isLocked = false;
        isTrapped = true;
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1)
        {
            closeUp.Normalize();
            StartCoroutine(TimeToTransition());
        }
        else
        {

        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        //returnArrow.SetActive(false);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableObjs()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;
    }
}
