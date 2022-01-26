using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventorySimple inventory;
    public int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            //GameObject.Destroy(child.gameObject);
            //GameObject.FindObjectOfType<Drop>().Detect();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<UIFollowMouse>().RemoveCursorForUI();
        FindObjectOfType<UIFollowMouse>().ChangeCursor("Point", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<UIFollowMouse>().ChangeCursor("Point", 0);
    }
}
