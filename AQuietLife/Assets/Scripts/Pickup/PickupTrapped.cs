using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupTrapped : MonoBehaviour
{
    private InventorySimple inventory;
    private PointerManager pointer;
    private ObjectSelection select;
    private GameManager gameMng;

    public GameObject itemButton;

    [SerializeField] private bool isTrapped;

    private void Start()
    {
        isTrapped = true;

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
        pointer = GameObject.FindGameObjectWithTag("Scene").GetComponent<PointerManager>();
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
        gameMng = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
                if (isTrapped == true)
                {
                    if (select.usingNothing == true)
                    {
                        Debug.Log("Game Over");
                        gameMng.Die();
                    }

                    if (select.usingGlove == true)
                    {
                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        FindObjectOfType<Glove>().gloveUsed = true;
                        StartCoroutine(Untrap());
                    }

                    if (select.usingStoveCloth == true)
                    {
                        FindObjectOfType<AudioCtrl>().Play("Disarm");
                        FindObjectOfType<StoveCloth>().gloveUsed = true;
                        StartCoroutine(Untrap());
                    }
                }
                
                if (isTrapped == false)
                {
                    Cursor.SetCursor(pointer.defaultTexture, pointer.hotSpot, pointer.curMode);
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    gameObject.SetActive(false);
                }

                break;
            }
        }
    }

    IEnumerator Untrap()
    {
        yield return new WaitForSeconds(0.1f);
        isTrapped = false;
    }
}
