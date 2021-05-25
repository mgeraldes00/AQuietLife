using UnityEngine;
using UnityEngine.EventSystems;

public class PickupFrozenBread : MonoBehaviour
{
    private InventorySimple inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
    }

    public void PickBread()
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
