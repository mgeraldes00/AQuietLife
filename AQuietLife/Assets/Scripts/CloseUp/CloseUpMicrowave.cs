using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpMicrowave : MonoBehaviour
{
    public GameManager gameMng;
    public MicrowaveManager microMng;
    public CameraCtrl zoom;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderMicrowave;
    //public GameObject inspectionTextGeneral;
    public GameObject[] objsToZoom;
    public GameObject microwave;
    //public GameObject microwaveButtons;
    //public GameObject activityText;
    //public GameObject microwaveRewindButton;

    public GameObject microwaveGeneral;
    //public GameObject microwaveWorking;

    public BoxCollider2D[] zoomableObjs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("Microwave") && gameMng.isLocked == false
                && zoom.currentView == 0)
            {
                if (microMng.rewindApplied == true)
                {
                    //fridgeButtons.SetActive(true);
                    //activityText.SetActive(true);
                }
                else if (microMng.rewindApplied == false)
                {
                    //fridgeRewindButton.SetActive(true);
                }

                for (int i = 0; i < zoomableObjs.Length; i++)
                    zoomableObjs[i].enabled = false;
                Debug.Log(hit.collider.gameObject.name);
                for (int i = 0; i < objsToZoom.Length; i++)
                    objsToZoom[i].SetActive(false);
                //objToZoom.SetActive(false);
                microwave.SetActive(true);
                returnArrow.SetActive(true);
                //noTextCollidersGeneral.SetActive(false);
                //noTextColliderMicrowave.SetActive(true);
                //inspectionTextGeneral.SetActive(false);
                zoom.currentView++;
            }
        }
    }
}
