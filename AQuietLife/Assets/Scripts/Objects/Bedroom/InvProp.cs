using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvProp : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    [SerializeField] private Image itemIcon;

    public string propName;

    public bool propUsed;

    [SerializeField] private bool isSelected;

    private void Awake()
    {
        select = 
            GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
        StartCoroutine(ObjectFade.FadeInUI(itemIcon));
    }

    private void Update()
    {
        if (select.slotSelect == 0 && isSelected == true)
        {
            StartCoroutine(DestroyObj());
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }

        if (select.slotSelect == 2 && isSelected == true)
        {
            isSelected = false;
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }   
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Select());
        select.slotSelect++;
    }

    IEnumerator Select()
    {
        yield return new WaitForEndOfFrame();
        isSelected = true;
        select.selectedObject = propName;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    IEnumerator DestroyObj()
    {
        StartCoroutine(ObjectFade.FadeOutUI(itemIcon));
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
