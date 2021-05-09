using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlateWBread : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasBreadWPlate;
    public bool breadWPlateUsed;

    private void Awake()
    {
        hasBreadWPlate = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (breadWPlateUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingPlateWBread == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingPlateWBread = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateW1Ing = false;
        select.usingKnife = false;
        select.usingBread = false;
        select.usingFrozenBread = false;
        select.usingHam = false;
        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
