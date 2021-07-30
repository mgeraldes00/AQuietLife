using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tomato : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasTomato;
    public bool tomatoUsed;

    private void Awake()
    {
        hasTomato = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (tomatoUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingTomato == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingTomato = true;
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
        select.usingBottle = false;
        select.usingMilk = false;
        select.usingJuice = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
