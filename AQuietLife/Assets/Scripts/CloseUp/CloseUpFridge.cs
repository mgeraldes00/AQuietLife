using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpFridge : MonoBehaviour
{
    public GameManager gameMng;
    public FridgeManager fridgeMng;

    public GameObject returnArrow;
    public GameObject noTextCollidersGeneral;
    public GameObject noTextColliderFridge;
    public GameObject inspectionTextGeneral;
    public GameObject[] objsToZoom;
    public GameObject fridge;
    public GameObject fridgeButtons;
    public GameObject activityText;
    public GameObject fridgeRewindButton;

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

            if (hit.collider.CompareTag("Fridge") && gameMng.isLocked == false)
            {
                if (fridgeMng.rewindApplied == true)
                {
                    fridgeButtons.SetActive(true);
                    activityText.SetActive(true);
                }
                else if (fridgeMng.rewindApplied == false)
                {
                    fridgeRewindButton.SetActive(true);
                }

                for (int i = 0; i < zoomableObjs.Length; i++)
                    zoomableObjs[i].enabled = false;
                Debug.Log(hit.collider.gameObject.name);
                for (int i = 0; i < objsToZoom.Length; i++)
                    objsToZoom[i].SetActive(false);
                //objToZoom.SetActive(false);
                fridge.SetActive(true);
                returnArrow.SetActive(true);
                noTextCollidersGeneral.SetActive(false);
                noTextColliderFridge.SetActive(true);
                inspectionTextGeneral.SetActive(false);
            }
        }
    }
}
