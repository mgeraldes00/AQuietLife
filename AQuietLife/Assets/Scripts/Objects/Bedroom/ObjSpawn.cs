using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSpawn : MonoBehaviour
{
    private GameManager gameMng;
    private ObjectSelection select;

    [SerializeField] private string currentArea;

    [SerializeField] private GameObject[] objsToSpawn;

    private void Start()
    {
        gameMng = GameObject.Find("GameManager").GetComponent<GameManager>();
        select = GameObject.Find("InventoryBox").GetComponent<ObjectSelection>();
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
            case "Wardrobe":
                if (select.selectedObject == "ShirtPile")
                    StartCoroutine(SpawnObjs());
                break;
        }
    }

    IEnumerator SpawnObjs()
    {
        select.slotSelect--;

        switch (objsToSpawn.Length)
        {
            case 1:
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
        }
    }
}
