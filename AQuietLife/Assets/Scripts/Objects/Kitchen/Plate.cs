using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plate : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasPlate;
    public bool plateUsed;

    private void Awake()
    {
        hasPlate = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (plateUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingPlate == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingPlate = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
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
        select.usingBottle = false;
        select.usingMilk = false;
        select.usingJuice = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
