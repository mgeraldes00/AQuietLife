using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKnife : MonoBehaviour
{
    private InventorySimple inventory;
    private PointerManager pointer;
    [SerializeField] ThoughtManager thought;

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
                if (inventory.knifeInPossession == true)
                {
                    //Show Thought
                    thought.ShowThought();
                    thought.text = "Already have one of these..";
                }
                else if (inventory.knifeInPossession == false)
                {
                    Cursor.SetCursor(pointer.defaultTexture, pointer.hotSpot, pointer.curMode);
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    FindObjectOfType<AudioCtrl>().Play("Knife");
                    StartCoroutine(GotKnife());
                    break;
                }     
            }
        }
    }

    IEnumerator GotKnife()
    {
        yield return new WaitForEndOfFrame();
        inventory.knifeInPossession = true;
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
