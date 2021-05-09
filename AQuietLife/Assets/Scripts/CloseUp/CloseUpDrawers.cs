using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpDrawers : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public DrawerManager drawerMng;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderDrawers;
    //public GameObject inspectionTextGeneral;
    //public GameObject[] objsToZoom;
    //public GameObject drawers;
    //public GameObject drawerButtons;
    //public GameObject activityText;
    //public GameObject drawerRewindButton;

    public BoxCollider2D drawers;
    public BoxCollider2D[] zoomableObjs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
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

                else if (hit.collider.CompareTag("Drawers") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomDrawers");
                    StartCoroutine(TimeToZoom());
                }
            }           
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //drawersRewindButton.SetActive(true);
        drawers.enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;
        returnArrow.SetActive(true);
        drawerMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        drawers.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
    }
}
