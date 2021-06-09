using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Handle : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasHandle;
    public bool handleUsed;

    private void Awake()
    {
        hasHandle = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (handleUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingHandle == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingHandle = true;
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

        select.usingButter = false;
        select.usingHam = false;
        select.usingMayo = false;
        select.usingLettuce = false;
        select.usingTomato = false;
        select.usingMilk = false;
        select.usingJuice = false;
        select.usingBottle = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
