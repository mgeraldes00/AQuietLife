using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bottle : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasBottle;
    public bool bottleUsed;

    private void Awake()
    {
        hasBottle = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (bottleUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingBottle == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingBottle = true;
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

        select.usingHam = false;
        select.usingMayo = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
