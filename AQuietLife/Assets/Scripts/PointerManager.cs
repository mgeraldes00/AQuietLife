using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public Texture2D defaultTexture;
    public Texture2D pickUpTexture;
    public Texture2D examineTexture;
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

        if (hit.collider.CompareTag("Cabinet"))
        {
            Debug.Log("Hovering");
            Cursor.SetCursor(examineTexture, hotSpot, curMode);
        }

        if (hit.collider.CompareTag("NoTag"))
        {
            Cursor.SetCursor(defaultTexture, hotSpot, curMode);
        }
    }

    private void OnMouseEnter()
    {
        if (gameObject.tag == "Cabinet")
        {
            Debug.Log("Hovering");
            Cursor.SetCursor(examineTexture, hotSpot, curMode);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);
    }
}
