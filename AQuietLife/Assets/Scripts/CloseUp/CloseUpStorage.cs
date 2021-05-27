using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpStorage : MonoBehaviour
{
    public CameraCtrl zoom;

    [SerializeField] private BoxCollider2D[] doors;

    private void OnMouseDown()
    {
        zoom.cameraAnim.SetTrigger("ZoomStorage");
        zoom.ObjectTransition();
        StartCoroutine(TimeToZoom());
    }

    public void Normalize()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        for (int i = 0; i < doors.Length; i++)
            doors[i].enabled = false;
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < doors.Length; i++)
            doors[i].enabled = true;
        zoom.currentView++;
    }
}
