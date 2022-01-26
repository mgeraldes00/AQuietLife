using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySimple : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public Image[] slotsSelected;

    public bool hasSelected;

    public bool plateInPossession;
    public bool knifeInPossession;
    public bool drinkInPossession;
    public bool keyInPossession;

    public void Select()
    {
        Debug.Log("Click");
    }

    public void SlotSelect(int i)
    {
        switch (i)
        {
            case 0:
                if (isFull[0] != false)
                {
                    slotsSelected[0].color = new Color32(255, 255, 255, 255);
                    slotsSelected[1].color = new Color32(255, 255, 255, 0);
                    slotsSelected[2].color = new Color32(255, 255, 255, 0);
                    slotsSelected[3].color = new Color32(255, 255, 255, 0);
                    hasSelected = true;
                }
                break;
            case 1:
                if (isFull[1] != false)
                {
                    slotsSelected[0].color = new Color32(255, 255, 255, 0);
                    slotsSelected[1].color = new Color32(255, 255, 255, 255);
                    slotsSelected[2].color = new Color32(255, 255, 255, 0);
                    slotsSelected[3].color = new Color32(255, 255, 255, 0);
                    hasSelected = true;
                }
                break;
            case 2:
                if (isFull[2] != false)
                {
                    slotsSelected[0].color = new Color32(255, 255, 255, 0);
                    slotsSelected[1].color = new Color32(255, 255, 255, 0);
                    slotsSelected[2].color = new Color32(255, 255, 255, 255);
                    slotsSelected[3].color = new Color32(255, 255, 255, 0);
                    hasSelected = true;
                }
                break;
            case 3:
                if (isFull[3] != false)
                {
                    slotsSelected[0].color = new Color32(255, 255, 255, 0);
                    slotsSelected[1].color = new Color32(255, 255, 255, 0);
                    slotsSelected[2].color = new Color32(255, 255, 255, 0);
                    slotsSelected[3].color = new Color32(255, 255, 255, 255);
                    hasSelected = true;
                }
                break;
        }
    }
}
