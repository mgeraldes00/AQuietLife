using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Glove : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasGlove;
    public bool gloveUsed;

    private void Awake()
    {
        hasGlove = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (gloveUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingGlove == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingGlove = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateW1Ing = false;
        select.usingKnife = false;
        select.usingBread = false;
        select.usingFrozenBread = false;
        select.usingHam = false;
        select.usingStoveCloth = false;
        select.usingPlateWFrozenBread = false;
    }
}
