using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BedroomObjMng : MonoBehaviour
{
    [SerializeField] private CameraCtrl zoom;
    [SerializeField] private GameManager gameMng;
    [SerializeField] private BackToGeneral returnMng;

    public BoxCollider2D area;
    public Collider2D[] objects;

    [SerializeField] private string zoomTrigger;
    [SerializeField] private string zoomTrigger2;

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
            && gameMng.isLocked == false)
        {
            switch (zoom.currentView)
            {
                case 0:
                    zoom.ObjectTransition();
                    zoom.GetComponent<Animator>().SetTrigger(zoomTrigger);
                    break;
                case 1:
                    zoom.GetComponent<Animator>().SetTrigger(zoomTrigger2);
                    break;
            }
            StartCoroutine(Zoom());
        }    
    }

    IEnumerator Zoom()
    {
        yield return new WaitForEndOfFrame();
        switch (zoom.currentView)
        {
            case 0:
                switch (zoom.currentPanel)
                {
                    case 2:
                        for (int i = 2; i < objects.Length; i++)
                            objects[i].enabled = true;
                        area.offset = new Vector2(5.92f, -1.22f);
                        area.size = new Vector2(2.62f, 2.86f);
                        returnMng.currentArea = "Desk";
                        break;
                }  
                break;
            case 1:
                switch (zoom.currentPanel)
                {
                    case 2:
                        area.enabled = false;
                        for (int i = 0; i < 2; i++)
                            objects[i].enabled = true;
                        returnMng.currentArea = "Chair";
                        break;
                }
                break;
        }
        yield return new WaitForSeconds(0.1f);
        zoom.currentView++;
    }
}
