using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Glove : MonoBehaviour, IPointerClickHandler
{
    public bool hasGlove;
    public bool gloveSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        gloveSelected = true;
        FindObjectOfType<Plate>().plateSelected = false;
    }

    private void Awake()
    {
        hasGlove = true;
    }
}
