using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGeneral : MonoBehaviour
{
    [SerializeField] private CameraCtrl cam;

    [SerializeField] private BoxCollider2D area;
    [SerializeField] private Collider2D[] objects;

    public string currentArea;

    public void AreaEffect()
    {
        switch (cam.currentPanel)
        {
            case -1:
                switch (currentArea)
                {
                    case "Backpack":
                        area = GameObject.Find("BedLeft_Wall").GetComponent<BedroomObjMng>().area;
                        objects = GameObject.Find("BedLeft_Wall").GetComponent<BedroomObjMng>().objects;
                        break;
                    case "ClothePile":
                        area = GameObject.Find("Clothes").GetComponent<BedroomObjMng>().area;
                        objects = GameObject.Find("Clothes").GetComponent<BedroomObjMng>().objects;
                        break;
                }
                break;
            case 0:
                area = GameObject.Find("Wardrobe_Wall").GetComponent<BedroomObjMng>().area;
                objects = GameObject.Find("Wardrobe_Wall").GetComponent<BedroomObjMng>().objects;
                break;
            case 1:
                switch (currentArea)
                {
                    case "TV":
                        area = GameObject.Find("TV_Wall").GetComponent<BedroomObjMng>().area;
                        objects = GameObject.Find("TV_Wall").GetComponent<BedroomObjMng>().objects;
                        break;
                    case "Door":
                        area = GameObject.Find("Door").GetComponent<BedroomObjMng>().area;
                        objects = null;
                        break;
                }
                break;
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
        switch (cam.currentPanel)
        {
            case -1:
                area.enabled = true;
                for (int i = 0; i < objects.Length; i++)
                    objects[i].enabled = false;
                currentArea = null;
                break;
            case 0:
                switch (currentArea)
                {
                    case "Wardrobe":
                        area.enabled = true;
                        for (int i = 0; i < objects.Length; i++)
                            objects[i].enabled = false;
                        currentArea = null;
                        break;
                }
                break;
            case 1:
                switch (currentArea)
                {
                    case "TV":
                        area.enabled = true;
                        for (int i = 0; i < objects.Length; i++)
                            objects[i].enabled = false;
                        currentArea = null;
                        break;
                    case "Door":
                        area.offset = new Vector2(6.72f, 0.91f);
                        area.size = new Vector2(5.43f, 8.1f);
                        currentArea = null;
                        break;
                }
                break;
            case 2:
                switch (currentArea)
                {
                    case "Desk":
                        area.offset = new Vector2(4.85f, -1.64f);
                        area.size = new Vector2(9.24f, 6.09f);
                        yield return new WaitForSeconds(0.1f);
                        currentArea = null;
                        break;
                    case "Chair":
                        area.enabled = true;
                        area.offset = new Vector2(5.92f, -1.22f);
                        area.size = new Vector2(2.62f, 2.86f);
                        for (int i = 0; i < 2; i++)
                            objects[i].enabled = false;
                        yield return new WaitForSeconds(0.1f);
                        currentArea = "Desk";
                        break;
                }
                break;
        }
    }
}
