using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public GameManager gameMng;

    public Texture2D defaultTexture;
    public Texture2D interactTexture;
    public Texture2D pickUpTexture;
    public Texture2D examineTexture;
    public Texture2D doorTexture;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider == null)
        {
            //Nothing
        }

        else if (hit.collider.CompareTag("Table") || hit.collider.CompareTag("Drawers")
            || hit.collider.CompareTag("Cabinet") || hit.collider.CompareTag("BreadBox")
            || hit.collider.CompareTag("Fridge") || hit.collider.CompareTag("Microwave")
            || hit.collider.CompareTag("CabinetBreach"))
        {
            Debug.Log("Hovering");
            //Cursor.SetCursor(examineTexture, hotSpot, curMode);
        }

        else if (hit.collider.CompareTag("CabinetDoor1")
            || hit.collider.CompareTag("CabinetDoor2")
            || hit.collider.CompareTag("CabinetDoor3")
            || hit.collider.CompareTag("CabinetDoor4")
            || hit.collider.CompareTag("BreadBoxDoor")
            || hit.collider.CompareTag("DrawerDoor1")
            || hit.collider.CompareTag("DrawerDoor2")
            || hit.collider.CompareTag("DrawerDoor3")
            || hit.collider.CompareTag("FridgeDoor1")
            || hit.collider.CompareTag("FridgeDoor2"))
        {
           // Cursor.SetCursor(interactTexture, hotSpot, curMode);
        }

        else if (hit.collider.CompareTag("Plate")
            || hit.collider.CompareTag("Bread1")
            || hit.collider.CompareTag("Knife"))
        {
            //Cursor.SetCursor(pickUpTexture, hotSpot, curMode);
        }

        else if (hit.collider.CompareTag("Door"))
        {
            //Cursor.SetCursor(doorTexture, hotSpot, curMode);
            //if (gameMng.isLocked == true)
                //Cursor.SetCursor(defaultTexture, hotSpot, curMode);
        }

        else if (hit.collider.CompareTag("NoTag") || gameMng.isDead == true)
        {
            //Cursor.SetCursor(defaultTexture, hotSpot, curMode);
        }       
    }

    private void OnMouseEnter()
    {
        if (gameObject.tag == "Cabinet")
        {
            Debug.Log("Hovering");
            //Cursor.SetCursor(examineTexture, hotSpot, curMode);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }
}
