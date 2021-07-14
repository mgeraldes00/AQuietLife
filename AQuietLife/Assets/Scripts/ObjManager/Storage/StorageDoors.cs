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
        if (collision.gameObject.name == "LeftTrigger"
            || collision.gameObject.name == "RightTrigger")
        {
            if (currentDoor == "Door1")
            {
                storage.leftDoorCol = true;
                storage.moveSpeed = -10f;
                StartCoroutine(storage.EnableObjs(0));
            }
            else if (currentDoor == "Door2")
            {
                storage.rightDoorCol = true;
                storage.moveSpeed2 = -10f;
                StartCoroutine(storage.EnableObjs(1));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "LeftTrigger"
               || collision.gameObject.name == "RightTrigger")
        {
            if (currentDoor == "Door1")
            {
                storage.leftDoorCol = false;
                storage.moveSpeed = 10f;
                StartCoroutine(storage.DisableObjs(0));
            }
            else if (currentDoor == "Door2")
            {
                storage.rightDoorCol = false;
                storage.moveSpeed2 = 10f;
                StartCoroutine(storage.DisableObjs(1));
            }
        }
    }
}
