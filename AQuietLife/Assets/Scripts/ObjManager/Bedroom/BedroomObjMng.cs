using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BedroomObjMng : MonoBehaviour
{
    [SerializeField] private CameraCtrl zoom;
    [SerializeField] private GameManager gameMng;

    public BoxCollider2D area;
    public Collider2D[] objects;

    [SerializeField] private string zoomTrigger;

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            switch (zoom.currentView)
            {
                case 0:
                    zoom.ObjectTransition();
                    zoom.GetComponent<Animator>().SetTrigger(zoomTrigger);
                    StartCoroutine(Zoom());
                    break;
                case 1:

                    break;
                case 2:

                    break;
            }
        }    
    }

    IEnumerator Zoom()
    {
        yield return new WaitForSeconds(0.1f);
        area.enabled = false;
        for (int i = 0; i < objects.Length; i++)
            objects[i].enabled = true;
        zoom.currentView++;
    }
}
