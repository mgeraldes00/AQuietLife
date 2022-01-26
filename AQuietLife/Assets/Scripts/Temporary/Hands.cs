using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hands : MonoBehaviour, IPointerClickHandler
{
    public Hands hands;

    public Image selectedHand;

    public Image[] selectedHands;

    public int numOfGloves;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hands.numOfGloves <= 2)
        {
            Debug.Log("Glove in hand");
            selectedHand.enabled = true;
            FindObjectOfType<Glove>().gloveUsed = true;
            hands.numOfGloves++;
        }
    }

    public void UseGlove()
    {
        if (selectedHands[0].enabled == true)
            selectedHands[0].enabled = false;
        if (selectedHands[1].enabled == true)
            selectedHands[1].enabled = false;
        StartCoroutine(RemoveGlove());
    }

    IEnumerator RemoveGlove()
    {
        yield return new WaitForSeconds(0.5f);
        numOfGloves--;
    }
}
