﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoveCloth : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasGlove;
    public bool gloveUsed;

    private void Awake()
    {
        hasGlove = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (gloveUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingStoveCloth == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!select.usingStoveCloth)
        {
            select.usingStoveCloth = true;
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            select.usingStoveCloth = false;
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        
        DeselectRest();
    }
    
    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateW1Ing = false;
        select.usingPlateWBread = false;
        select.usingPlateWFrozenBread = false;

        select.usingKnife = false;
        select.usingBread = false;
        select.usingGlass = false;
        select.usingFrozenBread = false;

        select.usingButter = false;
        select.usingHam = false;
        select.usingMayo = false;
        select.usingLettuce = false;
        select.usingTomato = false;
        select.usingBottle = false;
        select.usingMilk = false;
        select.usingJuice = false;

        select.usingGlove = false;
    }
}
