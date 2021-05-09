using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bread : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasBread;
    public bool breadUsed;

    private void Awake()
    {
        hasBread = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (breadUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingBread == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingBread = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateW1Ing = false;
        select.usingKnife = false;
        select.usingPlateWFrozenBread = false;
        select.usingFrozenBread = false;
        select.usingHam = false;
        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
