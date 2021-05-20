using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lettuce : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasLettuce;
    public bool lettuceUsed;

    private void Awake()
    {
        hasLettuce = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (lettuceUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingLettuce == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingLettuce = true;
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
        select.usingBottle = false;
        select.usingMilk = false;
        select.usingJuice = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
