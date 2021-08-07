using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BedroomObjMng : MonoBehaviour
{
    [SerializeField] private CameraCtrl zoom;
    [SerializeField] private GameManager gameMng;
    [SerializeField] private BackToGeneral returnMng;
    [SerializeField] private ThoughtManager thought;
    [SerializeField] private TaskCounter tasks;

    public BoxCollider2D area;
    public Collider2D[] objects;

    [SerializeField] private string zoomTrigger;
    [SerializeField] private string zoomTrigger2;
    [SerializeField] private string objName;

    private void Start()
    {
        thought = GameObject.Find("ThoughtBalloon").GetComponent<ThoughtManager>();
        tasks = GameObject.Find("Scene").GetComponent<TaskCounter>();
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
            && gameMng.isLocked == false)
        {
            switch (zoom.currentView)
            {
                case 0:
                    if (objName == "Door" && tasks.completedTasks < 1)
                    {
                        thought.ShowThought();
                        thought.text = "Should tidy up the room a bit before leaving..";
                    }
                    else
                    {
                        zoom.ObjectTransition();
                        zoom.GetComponent<Animator>().SetTrigger(zoomTrigger);
                        StartCoroutine(Zoom());
                    }
                    break;
                case 1:
                    if (zoomTrigger2 != null)
                        zoom.GetComponent<Animator>().SetTrigger(zoomTrigger2);
                    gameMng.isLocked = true;
                    StartCoroutine(gameMng.QuickUnlock());
                    StartCoroutine(Zoom());
                    break;
            }
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
                    case -1:
                        switch (objName)
                        {
                            case "Backpack":
                                area.enabled = false;
                                for (int i = 0; i < 1; i++)
                                    objects[i].enabled = true;
                                returnMng.currentArea = "Backpack";
                                break;
                            case "ClothePile":
                                area.enabled = false;
                                for (int i = 0; i < objects.Length; i++)
                                    objects[i].enabled = true;
                                returnMng.currentArea = "ClothePile";
                                break;
                        }
                        break;
                    case 0:
                        area.enabled = false;
                        for (int i = 0; i < 2; i++)
                            objects[i].enabled = true;
                        returnMng.currentArea = "Wardrobe";
                        break;
                    case 1:
                        switch (objName)
                        {
                            case "TV":
                                area.enabled = false;
                                for (int i = 0; i < objects.Length; i++)
                                    objects[i].enabled = true;
                                returnMng.currentArea = "TV";
                                break;
                            case "Door":
                                area.offset = new Vector2(4.92f, 0.79f);
                                area.size = new Vector2(1f, 1f);
                                for (int i = 0; i < objects.Length; i++)
                                    objects[i].enabled = true;
                                returnMng.currentArea = "Door";
                                thought.ShowThought();
                                thought.text = "Did I take care of everything?.";
                                break;
                        }
                        break;
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
                    case 1:
                        switch (objName)
                        {
                            case "Door":
                                //End level
                                break;
                        }
                        break;
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
