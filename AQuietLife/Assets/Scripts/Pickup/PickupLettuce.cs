using UnityEngine;
using UnityEngine.EventSystems;

public class PickupLettuce : MonoBehaviour
{
    private InventorySimple inventory;
    private GameManager pointer;

    public GameObject itemButton;

    [SerializeField] private string currentObj;

    [SerializeField] private bool isUnmovable;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
        pointer = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnMouseOver()
    {
        pointer.cursors.ChangeCursor("Grab", 1);
    }

    private void OnMouseExit()
    {
        pointer.cursors.ChangeCursor("Grab", 0);
    }

    public void ExternalTrigger()
    {
        OnMouseDown();
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                pointer.cursors.ChangeCursor("Grab", 0);
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                FindObjectOfType<AudioCtrl>().Play(currentObj);
                FindObjectOfType<FridgeManager>().lettuceTaken = true;

                if (isUnmovable != true)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                break;
            }
        }
    }
}
