using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCursor : MonoBehaviour
{
    public IntroController gameMng;

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

        if (hit.collider.CompareTag("NoTag"))
        {
            switch (gameMng.currentPanel)
            {
                case -1:
                    Cursor.SetCursor(interactTexture, hotSpot, curMode);
                    break;
                case 0:

                    break;
            }          
        }

        if (hit.collider.CompareTag("Nothing"))
        {
            switch (gameMng.currentPanel)
            {
                case 0:

                    break;
                case 1:

                    break;
            }
        }

        if (hit.collider.CompareTag("Ham") || gameMng.isDead == true)
        {
            Cursor.SetCursor(defaultTexture, hotSpot, curMode);
        }
    }
}
