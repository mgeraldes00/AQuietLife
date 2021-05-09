using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpMicrowave : MonoBehaviour
{
    public GameManager gameMng;
    public MicrowaveManager microMng;
    public CameraCtrl zoom;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderMicrowave;
    //public GameObject inspectionTextGeneral;
    //public GameObject[] objsToZoom;
    //public GameObject microwave;
    //public GameObject microwaveButtons;
    //public GameObject activityText;
    //public GameObject microwaveRewindButton;

    //public GameObject microwaveGeneral;
    //public GameObject microwaveWorking;

    public BoxCollider2D microwave;
    public BoxCollider2D[] zoomableObjs;

    // Update is called once per frame
    void Update()
    {
        if (microMng.working == false)
        {
            //microwaveWorking.SetActive(false);
            //microwaveGeneral.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("Mouse Clicked");
                Vector3 mousePos =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider == null)
                {
                    //Nothing
                }

                else if (hit.collider.CompareTag("Microwave") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomMicrowave");
                    StartCoroutine(TimeToZoom());
                }           
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        microwave.enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;
        returnArrow.SetActive(true);
        microMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        microwave.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
    }
}
