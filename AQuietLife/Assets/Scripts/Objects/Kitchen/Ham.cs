using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ham : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasHam;
    public bool hamUsed;

    private void Awake()
    {
        hasHam = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (hamUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingHam == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingHam = true;
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
