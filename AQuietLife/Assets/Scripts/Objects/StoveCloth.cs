using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoveCloth : MonoBehaviour, IPointerClickHandler
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

        if (select.usingStoveCloth == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingStoveCloth = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }
    
    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingGlove = false;
    }
}
