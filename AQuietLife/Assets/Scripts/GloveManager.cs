using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveManager : MonoBehaviour
{
    public InventoryManager inventory;
    public GameManager gameManager;

    public GameObject[] gloves;
    public GameObject currentGlove;
    public GameObject gloveTutorial;
    public GameObject interactionText;
    public GameObject returnArrow;

    public bool gloveTaken;
    public bool firstGlove;
    //public bool isLocked;

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

            if (hit.collider.CompareTag("Glove") && firstGlove == false
                && gameManager.isLocked == false)
            {
                //Debug.Log(hit.collider.gameObject.name);
                currentGlove.SetActive(false);
                inventory.GloveInInventory();
                gloveTaken = true;
                firstGlove = true;
                interactionText.SetActive(false);
                ShowTutorial();
            }

            if (hit.collider.CompareTag("Glove") && firstGlove == true)
            {
                //Debug.Log(hit.collider.gameObject.name);
                currentGlove.SetActive(false);
                inventory.GloveInInventory();
                gloveTaken = true;
                interactionText.SetActive(false);
            }
        }
    }

    public void ShowTutorial()
    {
        gloveTutorial.SetActive(true);
        Lock();
    }

    public void Lock()
    {
        returnArrow.SetActive(false);
        gameManager.isLocked = true;
    }

    private void OnMouseOver()
    {
        interactionText.SetActive(true);
    }

    private void OnMouseExit()
    {
        interactionText.SetActive(false);
    }
}
