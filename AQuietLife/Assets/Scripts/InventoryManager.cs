using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image gloveIcon;
    public Image plateIcon;
    public Image breadIcon;
    public Image knifeIcon;

    public bool hasObject;
    public bool hasGlove;

    public bool hasPlate;
    public bool hasBread;
    public bool hasKnife;

    public bool plateUsed;

    // Start is called before the first frame update
    void Start()
    {
        gloveIcon.enabled = false;
        plateIcon.enabled = false;
        breadIcon.enabled = false;
        knifeIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasObject == true)
        {

        }
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

    public void NoMoreObjects()
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
        plateUsed = true;
    }

    public void BreadInInventory()
    {
        breadIcon.enabled = true;
        hasBread = true;
        hasObject = true;
    }

    public void BreadOffInventory()
    {
        breadIcon.enabled = false;
        hasBread = false;
        hasObject = false;
    }

    public void KnifeInInventory()
    {
        knifeIcon.enabled = true;
        hasKnife = true;
        hasObject = true;
    }

    public void KnifeOffInventory()
    {
        knifeIcon.enabled = false;
        hasKnife = false;
        hasObject = false;
    }
}
