using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothePile : MonoBehaviour
{
    [SerializeField] private Collider2D glove;

    [SerializeField] private bool isRemoved;

    private void Update()
    {
        if (GameObject.Find("ShirtPile") != null)
            if (GameObject.Find("ShirtPile").GetComponent<PickupBedroom>().isPicked == true
                && isRemoved == false)
                StartCoroutine(RemovePile());
    }

    public IEnumerator RemovePile()
    {
        yield return new WaitForSeconds(1.0f);
        glove.enabled = true;
        isRemoved = true;
    }
}
