using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Glove : MonoBehaviour, IPointerClickHandler
{
    public bool hasGlove;
    public bool gloveSelected;

    private void Update()
    {
        if (gloveSelected == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gloveSelected = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        FindObjectOfType<Plate>().plateSelected = false;
    }

    private void Awake()
    {
        hasGlove = true;
    }
}
