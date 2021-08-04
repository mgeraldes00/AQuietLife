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
        switch (cam.currentPanel)
        {
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
