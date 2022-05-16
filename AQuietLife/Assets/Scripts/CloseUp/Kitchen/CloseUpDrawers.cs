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

    public BoxCollider2D[] drawers;

    [SerializeField] private Vector2[] originalOffset;
    [SerializeField] private Vector2[] originalSize;

    public int currentDrawer;

    public bool isOnDrawer;
    public bool isDrawer;

    private void Start()
    {
        originalOffset = new Vector2[drawers.Length];
        originalSize = new Vector2[drawers.Length];

        for (int i = 0; i < drawers.Length; i++)
        {
            originalOffset[i] = drawers[i].offset;
            originalSize[i] = drawers[i].size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom.currentView == 0)
        {
            Vector3 mousePos2 =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos22D = new Vector2(mousePos2.x, mousePos2.y);

            RaycastHit2D hit2 = Physics2D.Raycast(mousePos22D, Vector2.zero);

            if (hit2.collider == null)
            {
                if (isDrawer == true)
                {
                    gameMng.cursors.ChangeCursor("Inspect", 0);
                    isDrawer = false;
                }               
            }
            else if (hit2.collider.CompareTag("DrawerDoor1")
                || hit2.collider.CompareTag("DrawerDoor2")
                || hit2.collider.CompareTag("DrawerDoor3"))
            {
                gameMng.cursors.ChangeCursor("Inspect", 1);
                isDrawer = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {              
                Debug.Log("Mouse Clicked");
                Vector3 mousePos =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (gameMng.isLocked == false && zoom.currentView == 0)
                {
                    if (hit.collider == null)
                    {
                        //Nothing
                    }
                    else if (hit.collider.CompareTag("DrawerDoor1"))
                    {
                        zoom.cameraAnim.SetTrigger("ZoomDrawer");
                        StartCoroutine(TimeToZoom());
                        Debug.Log(hit.collider.gameObject.name);
                        zoom.ObjectTransition();
                        currentDrawer = 0;
                        drawers[0].offset = new Vector2(-0.06f, 0.13f);
                        drawers[0].size = new Vector2(0.62f, 0.27f);
                    }
                    else if (hit.collider.CompareTag("DrawerDoor2"))
                    {
                        zoom.cameraAnim.SetTrigger("ZoomDrawer2");
                        StartCoroutine(TimeToZoom());
                        Debug.Log(hit.collider.gameObject.name);
                        zoom.ObjectTransition();
                        currentDrawer = 1;
                        drawers[1].offset = new Vector2(0.04f, 0.17f);
                        drawers[1].size = new Vector2(0.72f, 0.19f);
                    }
                    else if (hit.collider.CompareTag("DrawerDoor3"))
                    {
                        zoom.cameraAnim.SetTrigger("ZoomDrawer3");
                        StartCoroutine(TimeToZoom());
                        Debug.Log(hit.collider.gameObject.name);
                        zoom.ObjectTransition();
                        currentDrawer = 2;
                        drawers[2].offset = new Vector2(0, 0.09f);
                        drawers[2].size = new Vector2(0.68f, 0.2f);
                    }
                }      
            }           
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //drawersRewindButton.SetActive(true);
        //drawers[currentDrawer].enabled = false;
        //returnArrow.SetActive(true);
        //drawerMng.EnableObjs();
        zoom.currentView++;
        isOnDrawer = true;
    }

    public void Normalize()
    {
        //drawers[currentDrawer].enabled = true;
        isOnDrawer = false;

        for (int i = 0; i < drawers.Length; i++)
        {
            drawers[i].offset = new Vector2(
                originalOffset[i].x, originalOffset[i].y);
            drawers[i].size = new Vector2(
                originalSize[i].x, originalSize[i].y);
        }
    }
}
