using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpBreadBox : MonoBehaviour
{
    public GameObject returnArrow;
    public GameObject noTextCollidersGeneral;
    public GameObject noTextColliderBreadBox;
    public GameObject inspectionTextGeneral;
    public GameObject[] objsToZoom;
    public GameObject breadBox;
    public GameObject breadBoxRewindButton;  

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

            if (hit.collider.CompareTag("BreadBox"))
            {
                for (int i = 0; i < zoomableObjs.Length; i++)
                    zoomableObjs[i].enabled = false;
                Debug.Log(hit.collider.gameObject.name);
                for (int i = 0; i < objsToZoom.Length; i++)
                    objsToZoom[i].SetActive(false);
                //objToZoom.SetActive(false);
                breadBox.SetActive(true);
                breadBoxRewindButton.SetActive(true);
                returnArrow.SetActive(true);
                noTextCollidersGeneral.SetActive(false);
                noTextColliderBreadBox.SetActive(true);
                inspectionTextGeneral.SetActive(false);
            }
        }
    }
}
