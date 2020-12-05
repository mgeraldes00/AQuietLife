using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image plateIcon;

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
    }
}
