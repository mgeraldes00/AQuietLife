using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        plateSelected = true;
        FindObjectOfType<Glove>().gloveSelected = false;
    }

    private void Awake()
    {
        hasPlate = true;
    }
}
