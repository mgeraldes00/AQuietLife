using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Glass : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasGlass;
    public bool glassUsed;

    private void Awake()
    {
        hasGlass = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (glassUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingGlass == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingGlass = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateWBread = false;
        select.usingPlateW1Ing = false;
        select.usingKnife = false;
        select.usingBread = false;
        select.usingFrozenBread = false;
        select.usingHam = false;
        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
