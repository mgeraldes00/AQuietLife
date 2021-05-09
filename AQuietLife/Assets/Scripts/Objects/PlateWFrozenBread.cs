using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlateWFrozenBread : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasFrozenBreadWPlate;
    public bool frozenBreadWPlateUsed;

    private void Awake()
    {
        hasFrozenBreadWPlate = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (frozenBreadWPlateUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingPlateWFrozenBread == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingPlateWFrozenBread = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateW1Ing = false;
        select.usingKnife = false;
        select.usingBread = false;
        select.usingPlateWBread = false;
        select.usingHam = false;
        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
