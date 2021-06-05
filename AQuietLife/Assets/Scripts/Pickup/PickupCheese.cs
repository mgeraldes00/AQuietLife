using UnityEngine;
using UnityEngine.EventSystems;

public class PickupCheese : MonoBehaviour
{
    private InventorySimple inventory;
    public GameObject itemButton;

    [SerializeField] private bool hasCheese;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
    }

    public void PickCheese()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false && hasCheese == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                hasCheese = true;
                break;
            }
        }
    }
}
