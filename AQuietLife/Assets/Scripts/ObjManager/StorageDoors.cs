using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDoors : MonoBehaviour
{
    private StorageManager storage;

    [SerializeField] private string currentDoor;

    private void Start()
    {
        storage = FindObjectOfType<StorageManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentDoor == "Door1")
        {
            storage.leftDoorCol = true;
        }
        else if (currentDoor == "Door2")
        {
            storage.rightDoorCol = true;
        }

        if (storage.leftDoorCol && storage.rightDoorCol)
        {
            StartCoroutine(storage.EnableObjs());
        }
    }
}
