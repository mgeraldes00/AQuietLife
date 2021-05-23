using UnityEngine;
using UnityEngine.EventSystems;

public class Pickup : MonoBehaviour
{
    private InventorySimple inventory;
    private PointerManager pointer;

    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
        pointer = GameObject.FindGameObjectWithTag("Scene").GetComponent<PointerManager>();
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
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                Cursor.SetCursor(pointer.defaultTexture, pointer.hotSpot, pointer.curMode);
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                gameObject.SetActive(false);
                break;
            }
        }
    }
}
