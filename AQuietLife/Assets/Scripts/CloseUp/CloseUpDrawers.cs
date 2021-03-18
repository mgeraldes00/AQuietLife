using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpDrawers : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public DrawerManager drawerMng;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderDrawers;
    //public GameObject inspectionTextGeneral;
    public GameObject[] objsToZoom;
    public GameObject drawers;
    //public GameObject drawerButtons;
    //public GameObject activityText;
    //public GameObject drawerRewindButton;

    public BoxCollider2D[] zoomableObjs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("Drawers") && gameMng.isLocked == false)
            {
                if (drawerMng.rewindApplied == true)
                {
                    //drawerButtons.SetActive(true);
                    //activityText.SetActive(true);
                }
                else if (drawerMng.rewindApplied == false)
                {
                    //drawerRewindButton.SetActive(true);
                }

                for (int i = 0; i < zoomableObjs.Length; i++)
                    zoomableObjs[i].enabled = false;
                Debug.Log(hit.collider.gameObject.name);
                /*for (int i = 0; i < objsToZoom.Length; i++)
                    objsToZoom[i].SetActive(false);*/
                //objToZoom.SetActive(false);
                drawers.SetActive(true);
                returnArrow.SetActive(true);
                //noTextCollidersGeneral.SetActive(false);
                //noTextColliderDrawers.SetActive(true);
                //inspectionTextGeneral.SetActive(false);
                zoom.currentView++;
            }
        }
    }
}
