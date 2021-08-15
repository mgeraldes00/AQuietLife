using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private ObjectSelection select;

    [SerializeField] private GameObject cover;
    [SerializeField] private GameObject[] notes;
    [SerializeField] private GameObject[] pencilCase;

    public bool isLocked;
    public bool pencilPlaced;
    public bool notesPlaced;

    [SerializeField] private float timeToWait;

    public int objectsPlaced;

    private void Update()
    {
        if (isLocked != true)
        {
            if (select.selectedObject == "Notes")
            {
                timeToWait = 3.0f;
                GameObject.Find("backpack_prop (1)").GetComponent<ObjSpawn>().objsToSpawn = notes;
            }
            else if (select.selectedObject == "Pencil")
            {
                timeToWait = 1.0f;
                GameObject.Find("backpack_prop (1)").GetComponent<ObjSpawn>().objsToSpawn = pencilCase;
            }
        }
    }

    IEnumerator Unlock()
    {
        isLocked = true;
        if (objectsPlaced == 0)
            yield return new WaitForSeconds(1.0f);
        yield return new WaitForSeconds(timeToWait);
        isLocked = false;

    }

    public IEnumerator PlaceObject()
    {
        StartCoroutine(Unlock());
        if (objectsPlaced == 0)
            StartCoroutine(ObjectFade.FadeIn(cover.GetComponent<SpriteRenderer>()));
        yield return new WaitForEndOfFrame();
        objectsPlaced++;
    }
}
