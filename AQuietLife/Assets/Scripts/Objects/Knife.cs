using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Knife : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasKnife;
    public bool knifeUsed;

    private void Awake()
    {
        hasKnife = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (knifeUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingKnife == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingKnife = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateW1Ing = false;
        select.usingPlateWBread = false;
        select.usingPlateWFrozenBread = false;

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

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
