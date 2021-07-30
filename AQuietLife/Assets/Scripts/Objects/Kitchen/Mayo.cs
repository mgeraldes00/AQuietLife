using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mayo : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasMayo;
    public bool mayoUsed;

    private void Awake()
    {
        hasMayo = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (mayoUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingMayo == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingMayo = true;
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
        select.usingLettuce = false;
        select.usingTomato = false;
        select.usingBottle = false;
        select.usingMilk = false;
        select.usingJuice = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
