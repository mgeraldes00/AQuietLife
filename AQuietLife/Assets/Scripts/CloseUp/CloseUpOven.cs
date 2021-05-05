using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpOven : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public OvenManager ovenMng;

    public GameObject returnArrow;

    //public BoxCollider2D oven;
    public BoxCollider2D[] zoomableObjs;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (gameMng.isLocked == false && zoom.currentView == 0)
            {
                zoom.ObjectTransition();
                zoom.cameraAnim.SetTrigger("ZoomOven");
                StartCoroutine(TimeToZoom());
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //ovenRewindButton.SetActive(true);
        //oven.enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;
        returnArrow.SetActive(true);
        ovenMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        //oven.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
    }
}
