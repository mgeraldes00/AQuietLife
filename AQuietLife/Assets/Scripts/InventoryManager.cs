using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image plateIcon;
    public Image gloveIcon;

    public bool hasObject;
    public bool hasGlove;

    public bool hasPlate;

    // Start is called before the first frame update
    void Start()
    {
        plateIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlateInInventory()
    {
        plateIcon.enabled = true;
        hasPlate = true;
        hasObject = true;
    }

    public void PlateOffInventory()
    {
        plateIcon.enabled = false;
        hasPlate = false;
        hasObject = false;
    }

    public void GloveInInventory()
    {
        gloveIcon.enabled = true;
        hasGlove = true;
    }

    public void GloveOffInventory()
    {
        gloveIcon.enabled = false;
        hasGlove = false;
    }
}
