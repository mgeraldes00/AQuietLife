using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameManager gameMng;

    public Image[] gloveIcons;
    public Image plateIcon;
    public Image breadIcon;
    public Image knifeIcon;
    public Image hamIcon;
    public Image[] breadPlateUncutIcon;
    public Image[] breadCutIcon;
    public Image[] hamBreadIcon;

    public bool hasObject;
    public bool hasGlove;

    [SerializeField]
    private int numOfGloves;

    public bool hasPlate;
    public bool hasBread;
    public bool hasFrozenBread;
    public bool hasFrozenBreadInPlate;
    public bool hasBreadInPlate;
    public bool hasKnife;
    public bool hasHam;
    public bool hasPlateWBread;

    public bool plateUsed;
    public bool breadUsed;
    public bool frozenBreadUsed;
    public bool frozenBreadPlateUsed;
    public bool breadPlateUsed;
    public bool knifeUsed;
    public bool hamUsed;

    // Start is called before the first frame update
    void Start()
    {
        numOfGloves = 0;

        //gloveIcon.enabled = false;
        /*for (int i = 0; i < gloveIcons.Length; i++)
            gloveIcons[i].enabled = false;
        plateIcon.enabled = false;
        breadIcon.enabled = false;
        knifeIcon.enabled = false;
        hamIcon.enabled = false;
        for (int i = 0; i < breadPlateUncutIcon.Length; i++)
            breadPlateUncutIcon[i].enabled = false;
        for (int i = 0; i < breadCutIcon.Length; i++)
            breadCutIcon[i].enabled = false;
        for (int i = 0; i < hamBreadIcon.Length; i++)
            hamBreadIcon[i].enabled = false;*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GloveInInventory()
    {
        numOfGloves++;
        //gloveIcon.enabled = true;
        //hasGlove = true;

        switch (numOfGloves)
        {
            case 2:
                gloveIcons[0].enabled = true;
                hasGlove = true;
                break;
            case 3:
                gloveIcons[1].enabled = true;
                break;
        }
    }

    public void GloveOffInventory()
    {
        numOfGloves--;
        //gameMng.glovesUsed++;
        //gloveIcon.enabled = false;
        //hasGlove = false;

        switch (numOfGloves)
        {
            case 2:
                gloveIcons[1].enabled = false;             
                break;
            case 1:
                gloveIcons[0].enabled = false;
                hasGlove = false;
                break;
        }
    }

    public void NoMoreObjects()
    {
        
    }

    public void PlateInInventory()
    {
        //plateIcon.enabled = true;
        hasPlate = true;
        hasObject = true;
    }

    public void PlateOffInventory()
    {
        //plateIcon.enabled = false;
        hasPlate = false;
        hasObject = false;
        plateUsed = true;
    }

    public void BreadInInventory()
    {
        //breadIcon.enabled = true;
        hasBread = true;
        hasObject = true;
    }

    public void BreadOffInventory()
    {
        //breadIcon.enabled = false;
        hasBread = false;
        hasObject = false;
        breadUsed = true;
    }

    public void FrozenBreadInInventory()
    {
        //breadIcon.enabled = true;
        hasFrozenBread = true;
        hasObject = true;
    }

    public void FrozenBreadOffInventory()
    {
        //breadIcon.enabled = false;
        hasFrozenBread = false;
        hasObject = false;
        frozenBreadUsed = true;
    }

    public void PlateBreadUncutInInventory()
    {
        /*for (int i = 0; i < breadPlateUncutIcon.Length; i++)
            breadPlateUncutIcon[i].enabled = true;**/
        hasFrozenBreadInPlate = true;
        plateUsed = false;
        frozenBreadUsed = false;
        hasObject = true;
    }

    public void PlateBreadUncutOffInventory()
    {
        /*for (int i = 0; i < breadPlateUncutIcon.Length; i++)
            breadPlateUncutIcon[i].enabled = false;*/
        hasFrozenBreadInPlate = false;
        frozenBreadPlateUsed = true;
        hasObject = false;
    }

    public void PlateBreadUncutUnfrozenInInventory()
    {
        /*for (int i = 0; i < breadPlateUncutIcon.Length; i++)
            breadPlateUncutIcon[i].enabled = true;*/
        hasBreadInPlate = true;
        hasObject = true;
    }

    public void PlateBreadUncutUnfrozenOffInventory()
    {
        /*for (int i = 0; i < breadPlateUncutIcon.Length; i++)
            breadPlateUncutIcon[i].enabled = false;*/
        hasBreadInPlate = false;
        plateUsed = true;
        breadUsed = true;
        hasObject = false;
    }

    public void KnifeInInventory()
    {
        //knifeIcon.enabled = true;
        hasKnife = true;
        hasObject = true;
    }

    public void KnifeOffInventory()
    {
        //knifeIcon.enabled = false;
        hasKnife = false;
        hasObject = false;
        knifeUsed = true;
    }

    public void BreadCutInInventory()
    {
        /*for (int i = 0; i < hamBreadIcon.Length; i++)
            hamBreadIcon[i].enabled = true;*/
        hasPlateWBread = true;
        hasObject = true;
    }

    public void HamInInventory()
    {
        //hamIcon.enabled = true;
        hasHam = true;
        hasObject = true;
    }

    public void HamOffInventory()
    {
        //hamIcon.enabled = false;
        hasHam = false;
        hasObject = false;
        hamUsed = true;
    }
}
