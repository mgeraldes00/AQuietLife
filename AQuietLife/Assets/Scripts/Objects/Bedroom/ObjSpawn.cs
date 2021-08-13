using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSpawn : MonoBehaviour
{
    private GameManager gameMng;
    private ObjectSelection select;
    private TaskCounter tasks;

    [SerializeField] private string currentArea;

    public GameObject[] objsToSpawn;

    private void Start()
    {
        gameMng = GameObject.Find("GameManager").GetComponent<GameManager>();
        select = GameObject.Find("InventoryBox").GetComponent<ObjectSelection>();
        tasks = GameObject.Find("Scene").GetComponent<TaskCounter>();
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
            && gameMng.isLocked == false)
        {
            CheckCurrentArea(currentArea);
        }
    }

    void CheckCurrentArea(string i)
    {
        switch (i)
        {
            case "Backpack":
                if (select.selectedObject == "Notes"
                    || select.selectedObject == "Pencil")
                {
                    StartCoroutine(SpawnObjs());
                    StartCoroutine(FindObjectOfType<Backpack>().PlaceObject());
                }
                break;
            case "Wardrobe":
                if (select.selectedObject == "ShirtPile")
                    StartCoroutine(SpawnObjs());
                break;
            case "TV":
                if (select.selectedObject == "Games")
                    StartCoroutine(SpawnObjs());
                break;
        }
    }

    IEnumerator SpawnObjs()
    {
        select.slotSelect--;
        if (currentArea != "Backpack" || FindObjectOfType<Backpack>().objectsPlaced == 2)
            tasks.completedTasks++;

        switch (objsToSpawn.Length)
        {
            case 1:
                if (currentArea == "Backpack" && FindObjectOfType<Backpack>().objectsPlaced == 0)
                    yield return new WaitForSeconds(1.0f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[0].GetComponent<SpriteRenderer>()));
                break;
            case 2:
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[1].GetComponent<SpriteRenderer>()));
                break;
            case 3:
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[2].GetComponent<SpriteRenderer>()));
                break;
            case 4:
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[2].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[3].GetComponent<SpriteRenderer>()));
                break;
            case 5:
                if (currentArea == "Backpack" && FindObjectOfType<Backpack>().objectsPlaced == 0)
                    yield return new WaitForSeconds(1.0f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[2].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[3].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeIn(objsToSpawn[4].GetComponent<SpriteRenderer>()));
                break;
        }
    }
}
