﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Butter : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasButter;
    public bool butterUsed;

    private void Awake()
    {
        hasButter = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (butterUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingButter == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingButter = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
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

        select.usingTomato = false;
        select.usingHam = false;
        select.usingMayo = false;
        select.usingLettuce = false;
        select.usingBottle = false;
        select.usingMilk = false;
        select.usingJuice = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}