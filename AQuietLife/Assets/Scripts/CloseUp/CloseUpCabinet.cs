using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpCabinet : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public CabinetManager cabinetMng;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderCabinet;
    //public GameObject inspectionTextGeneral;
    //public GameObject[] objsToZoom;
    //public GameObject cabinet;
    public GameObject cabinetRewindButton;
    //public GameObject activityText;
    //public GameObject cabinetButtons;

    public BoxCollider2D cabinet;
    public BoxCollider2D[] zoomableObjs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");
            Vector3 mousePos = 
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null)
            {
                //Nothing
            }

            else if (hit.collider.CompareTag("Cabinet") && gameMng.isLocked == false
                && zoom.currentView == 0)
            {
                Debug.Log(hit.collider.gameObject.name);
                zoom.ObjectTransition();
                zoom.cameraAnim.SetTrigger("ZoomCabinet");
                StartCoroutine(TimeToZoom());
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        if (cabinetMng.rewindApplied == true)
        {
            //cabinetButtons.SetActive(true);
            //activityText.SetActive(true);
        }
        else if (cabinetMng.rewindApplied == false)
        {
            cabinetRewindButton.SetActive(true);
        }

        cabinet.enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;     
        /*for (int i = 0; i < objsToZoom.Length; i++)
            objsToZoom[i].SetActive(false);*/
        //objToZoom.SetActive(false);
        //cabinet.SetActive(true);
        //cabinetRewindButton.SetActive(true);
        //returnArrow.SetActive(true);
        //noTextCollidersGeneral.SetActive(false);
        //inspectionTextGeneral.SetActive(false);
        //noTextColliderCabinet.SetActive(true);
        //cabinetButtons.SetActive(true);
        cabinetMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        cabinet.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
    }
}
