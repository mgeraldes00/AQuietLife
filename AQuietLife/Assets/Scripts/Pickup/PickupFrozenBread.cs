using UnityEngine;
using UnityEngine.EventSystems;

public class PickupFrozenBread : MonoBehaviour
{
    private InventorySimple inventory;
    private Spawn spawn;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
        spawn = GameObject.FindGameObjectWithTag("TableClose").GetComponent<Spawn>();
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                gameObject.SetActive(false);
                break;
            }
        }
    }
}
