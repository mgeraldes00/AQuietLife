using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKnife : MonoBehaviour
{
    private InventorySimple inventory;
    private GameManager pointer;
    [SerializeField] ThoughtManager thought;

    public GameObject itemButton;

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

    private void OnMouseDown()
    {
        if (!pointer.isLocked)
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
                        pointer.cursors.ChangeCursor("Grab", 0);
                        inventory.isFull[i] = true;
                        Instantiate(itemButton, inventory.slots[i].transform, false);
                        FindObjectOfType<AudioCtrl>().Play("Knife");
                        StartCoroutine(GotKnife());
                        gameObject.SetActive(false);
                        break;
                    }     
                }
            }
        }
    }

    IEnumerator GotKnife()
    {
        yield return new WaitForEndOfFrame();
        inventory.knifeInPossession = true;
    }
}
