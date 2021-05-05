using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpFridge : MonoBehaviour
{
    public GameManager gameMng;
    public FridgeManager fridgeMng;
    public CameraCtrl zoom;

    public GameObject[] directionArrows;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderFridge;
    //public GameObject inspectionTextGeneral;
    //public GameObject[] objsToZoom;
    //public GameObject fridge;
    //public GameObject fridgeButtons;
    //public GameObject activityText;
    //public GameObject fridgeRewindButton;

    public BoxCollider2D[] fridge;
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

                if (hit.collider.CompareTag("Fridge") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomFridge");
                    directionArrows[0].SetActive(true);
                    fridgeMng.lookAtFridge = true;
                    StartCoroutine(TimeToZoom());
                }

                if (hit.collider.CompareTag("Freezer") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomFreezer");
                    directionArrows[1].SetActive(true);
                    fridgeMng.lookAtFridge = false;
                    StartCoroutine(TimeToZoom());
                }
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //fridgeRewindButton.SetActive(true);
        fridge[0].enabled = false;
        fridge[1].enabled = false;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = true;
        returnArrow.SetActive(true);
        fridgeMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        fridge[0].enabled = true;
        fridge[1].enabled = true;
        directionArrows[0].SetActive(false);
        directionArrows[1].SetActive(false);
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
    }
}
