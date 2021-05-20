using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Juice : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasJuice;
    public bool juiceUsed;

    private void Awake()
    {
        hasJuice = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (juiceUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingJuice == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingJuice = true;
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
        select.usingTomato = false;
        select.usingHam = false;
        select.usingMayo = false;
        select.usingLettuce = false;
        select.usingBottle = false;
        select.usingMilk = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
