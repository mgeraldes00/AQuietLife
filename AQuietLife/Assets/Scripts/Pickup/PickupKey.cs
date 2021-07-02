using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupKey : MonoBehaviour
{
    private InventorySimple inventory;
    private PointerManager pointer;
    private CameraCtrl zoom;

    public GameObject itemButton;

    [SerializeField] private string currentObj;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
        pointer = GameObject.FindGameObjectWithTag("Scene").GetComponent<PointerManager>();
        zoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCtrl>();

        gameObject.GetComponent<Animator>().SetBool("Glowing", true);
    }

    private void Update()
    {
        if (zoom.currentView > 0)
            gameObject.GetComponent<Animator>().SetBool("Glowing", false);
        else if (zoom.currentView == 0)
            gameObject.GetComponent<Animator>().SetBool("Glowing", true);
    }

    private void OnMouseOver()
    {
        Cursor.SetCursor(pointer.interactTexture, pointer.hotSpot, pointer.curMode);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(pointer.defaultTexture, pointer.hotSpot, pointer.curMode);
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    Cursor.SetCursor(pointer.defaultTexture, pointer.hotSpot, pointer.curMode);
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    FindObjectOfType<AudioCtrl>().Play(currentObj);
                    gameObject.SetActive(false);
                    break;
                }
            }
        }

    }
}
