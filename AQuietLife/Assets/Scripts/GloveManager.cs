using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveManager : MonoBehaviour
{
    public InventoryManager inventory;

    public GameObject[] gloves;
    public GameObject currentGlove;

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

            if (hit.collider.CompareTag("Glove"))
            {
                //Debug.Log(hit.collider.gameObject.name);
                currentGlove.SetActive(false);
                inventory.GloveInInventory();
            }
        }
    }
}
