using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothePile : MonoBehaviour
{
    [SerializeField] private CameraCtrl zoom;

    [SerializeField] private Collider2D glove;

    [SerializeField] private bool isRemoved;

    private void Update()
    {
        if (GameObject.Find("ShirtPile") != null)
            if (GameObject.Find("ShirtPile").GetComponent<PickupBedroom>().isPicked == true
                && isRemoved == false)
                StartCoroutine(RemovePile());

        if (zoom.currentView == 0)
            if (GameObject.Find("Clothes").GetComponent<BedroomObjMng>().objects[0] == null
                && GameObject.Find("Clothes").GetComponent<BedroomObjMng>().objects[1] == null)
                GameObject.Find("Clothes").GetComponent<BoxCollider2D>().enabled = false;


    }

    public IEnumerator RemovePile()
    {
        yield return new WaitForSeconds(1.0f);
        if (zoom.currentView == 1)
            glove.enabled = true;
        isRemoved = true;
    }
}
