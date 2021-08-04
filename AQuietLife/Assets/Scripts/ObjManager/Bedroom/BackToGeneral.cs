using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGeneral : MonoBehaviour
{
    [SerializeField] private CameraCtrl cam;

    [SerializeField] private BoxCollider2D area;
    [SerializeField] private Collider2D[] objects;

    public void AreaEffect(int currArea)
    {
        currArea = cam.currentPanel;

        switch (currArea)
        {
            case 2:
                area = GameObject.Find("Desk_Wall").GetComponent<BedroomObjMng>().area;
                objects = GameObject.Find("Desk_Wall").GetComponent<BedroomObjMng>().objects;
                break;
        }
        StartCoroutine(Normalize());
    }

    IEnumerator Normalize()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < objects.Length; i++)
            objects[i].enabled = false;
        area.enabled = true;

    }
}
