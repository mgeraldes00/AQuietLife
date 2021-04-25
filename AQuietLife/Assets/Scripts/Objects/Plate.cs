using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plate : MonoBehaviour, IPointerClickHandler
{
    public bool hasPlate;
    public bool plateSelected;
    public bool plateUsed;

    private void Update()
    {
        if (plateUsed == true)
        {
            Destroy(gameObject);
        }

        if (plateSelected == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        plateSelected = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        FindObjectOfType<Glove>().gloveSelected = false;
    }

    private void Awake()
    {
        hasPlate = true;
    }
}
