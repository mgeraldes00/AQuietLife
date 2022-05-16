using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpBreadBox : MonoBehaviour
{
    public CameraCtrl zoom;
    public GameManager gameMng;
    public BreadBoxManager breadBoxMng;

    public GameObject returnArrow;
    public GameObject breadBoxRewindButton;

    public BoxCollider2D breadBox;
    public BoxCollider2D[] zoomableObjs;

    public bool isBreadBox;

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

                else if (hit.collider.CompareTag("BreadBox") && gameMng.isLocked == false
                    && zoom.currentView == 0)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomBreadbox");
                    StartCoroutine(TimeToZoom());
                    isBreadBox = true;
                }         
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //breadBoxRewindButton.SetActive(true);
        //breadBoxRewindButton.GetComponent<Animator>().SetBool("Visible", true);
        breadBox.enabled = false;
        if (!breadBoxMng.doorOpen)
            for (int i = 0; i < zoomableObjs.Length; i++)
                zoomableObjs[i].enabled = true;
        //returnArrow.SetActive(true);
        breadBoxMng.EnableObjs();
        zoom.currentView++;
    }

    public void Normalize()
    {
        breadBox.enabled = true;
        for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;
        isBreadBox = false;
    }

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0)
            gameMng.cursors.ChangeCursor("Inspect", 1);
        else if (zoom.currentView == 1)
            gameMng.cursors.ChangeCursor("Grab", 1);
    }

    private void OnMouseExit()
    {
        gameMng.cursors.ChangeCursor("Inspect", 0);
    }
}
