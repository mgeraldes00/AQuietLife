using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public bool usingNothing;
    public bool usingStruct;
    public bool usingIng;

    public bool usingPlate;
    public bool usingPlateWBread;
    public bool usingPlateWFrozenBread;
    public bool usingPlateW1Ing;

    public bool usingHandle;
    public bool usingKey;

    public bool usingGlass;
    public bool usingKnife;
    public bool usingBread;
    public bool usingFrozenBread;

    public bool usingButter;
    public bool usingTomato;
    public bool usingHam;
    public bool usingMayo;
    public bool usingLettuce;
    public bool usingCheese;
    public bool usingPickle;
    public bool usingBottle;
    public bool usingMilk;
    public bool usingJuice;

    public bool usingGlove;
    public bool usingStoveCloth;

    public int slotSelect;

    public bool gloveSelect;

    public string selectedObject;

    private void Update()
    {
        if (slotSelect > 1)
        {
            StartCoroutine(SlotReset());
        }
        if (slotSelect < 1)
        {
            selectedObject = null;
        }

        if (usingGlove == false && usingStoveCloth == false)
            usingNothing = true;
        else
            usingNothing = false;

        if (usingGlass == false && usingKnife == false && usingBread == false
            && usingFrozenBread == false)
            usingStruct = false;
        else
            usingStruct = true;

        if (usingButter == false && usingTomato == false && usingHam == false
            && usingMayo == false && usingLettuce == false && usingBottle == false
            && usingMilk == false && usingJuice == false && usingCheese == false
            && usingPickle == false)
            usingIng = false;
        else
            usingIng = true;
    }

    public void DeselectAll()
    {
        usingPlate = false;
        usingPlateWBread = false;
        usingPlateWFrozenBread = false;
        usingPlateW1Ing = false;

        usingGlass = false;
        usingKnife = false;
        usingBread = false;
        usingFrozenBread = false;

        usingHandle = false;
        usingKey = false;

        usingButter = false;
        usingTomato = false;
        usingHam = false;
        usingMayo = false;
        usingLettuce = false;
        usingCheese = false;
        usingPickle = false;
        usingBottle = false;
        usingMilk = false;
        usingJuice = false;

        usingGlove = false;
        usingStoveCloth = false;
    }

    IEnumerator SlotReset()
    {
        yield return new WaitForEndOfFrame();
        slotSelect--;
    }
}
