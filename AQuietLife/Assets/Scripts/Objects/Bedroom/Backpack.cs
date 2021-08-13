using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private ObjectSelection select;

    [SerializeField] private GameObject cover;
    [SerializeField] private GameObject[] notes;
    [SerializeField] private GameObject[] pencilCase;

    public bool pencilPlaced;
    public bool notesPlaced;

    public int objectsPlaced;

    private void Update()
    {
        if (select.selectedObject == "Notes")
            GameObject.Find("backpack_prop (1)").GetComponent<ObjSpawn>().objsToSpawn = notes;
        else if (select.selectedObject == "Pencil")
            GameObject.Find("backpack_prop (1)").GetComponent<ObjSpawn>().objsToSpawn = pencilCase;
    }

    public IEnumerator PlaceObject()
    {
        if (objectsPlaced == 0)
            StartCoroutine(ObjectFade.FadeIn(cover.GetComponent<SpriteRenderer>()));
        yield return new WaitForEndOfFrame();
        objectsPlaced++;
    }
}
