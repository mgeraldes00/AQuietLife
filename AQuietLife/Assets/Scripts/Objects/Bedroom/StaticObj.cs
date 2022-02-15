using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticObj : MonoBehaviour
{
    private GameManager gameMng;
    private ObjectSelection select;

    [SerializeField] private GameObject[] objectVar;

    [SerializeField] private string doorType;

    [SerializeField] private bool isDoor;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTrapped;

    public int useState;

    private void Start()
    {
        gameMng = GameObject.Find("GameManager").GetComponent<GameManager>();
        select = GameObject.Find("InventoryBox").GetComponent<ObjectSelection>();
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
            && isLocked == false)
        {
            if (isDoor)
            {
                if (isTrapped == true)
                {
                    StartCoroutine(CheckForGlove());
                }
                else
                    switch (useState)
                    {
                        case 0:
                            StartCoroutine
                                (ObjectFade.FadeOut(objectVar[0], 0, 0));
                            StartCoroutine
                                (ObjectFade.FadeIn(objectVar[1].GetComponent<SpriteRenderer>()));
                            StartCoroutine(SwitchCollider(0, doorType));
                            isLocked = true;
                            break;
                        case 1:
                            StartCoroutine
                                (ObjectFade.FadeOut(objectVar[1], 0, 0));
                            StartCoroutine
                                (ObjectFade.FadeIn(objectVar[0].GetComponent<SpriteRenderer>()));
                            StartCoroutine(SwitchCollider(1, doorType));
                            isLocked = true;
                            break;
                        case 2:
                            StartCoroutine
                                (ObjectFade.FadeOut(objectVar[0], 0, 0));
                            isLocked = true;
                            gameMng.isLocked = true;
                            FindObjectOfType<CameraCtrl>().GetComponent<Animator>().SetTrigger("EndLevel");
                            break;
                    }
            }
            StartCoroutine(gameMng.QuickUnlock(1.0f));
        }
    }

    IEnumerator SwitchCollider(int i, string doorType)
    {
        yield return new WaitForEndOfFrame();
        switch (useState)
        {
            case 0:
                if (doorType == "WardrobeRightDoor")
                {
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-8.92f, 0.46f);
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 9.44f);
                    FindObjectOfType<Wardrobe>().objects[0].enabled = true;
                    FindObjectOfType<Wardrobe>().rightDoorOpen = true;
                }
                else if (doorType == "WardrobeLeftDoor")
                {
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-1.29f, 0.5f);
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.36f, 9.39f);
                    //FindObjectOfType<Wardrobe>().objects[1].enabled = true;
                    FindObjectOfType<Wardrobe>().leftDoorOpen = true;
                }
                break;
            case 1:
                if (doorType == "WardrobeRightDoor")
                {
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-7.09f, 0.46f);
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3.84f, 9.44f);
                    FindObjectOfType<Wardrobe>().objects[0].enabled = false;
                    FindObjectOfType<Wardrobe>().rightDoorOpen = false;
                }
                else if (doorType == "WardrobeLeftDoor")
                {
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-3.14f, 0.5f);
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(4.04f, 9.39f);
                    //FindObjectOfType<Wardrobe>().objects[1].enabled = true;
                    FindObjectOfType<Wardrobe>().leftDoorOpen = false;
                }
                break;
        }
        yield return new WaitForSeconds(1.0f);
        if (i == 0)
            useState = 1;
        else if (i == 1)
            useState = 0;
        isLocked = false;
    }

    IEnumerator CheckForGlove()
    {
        if (select.selectedObject != "Glove")
        {
            //Game Over
            Debug.Log("GAME OVER");
            gameMng.Die();
        }
        else
        {
            yield return new WaitForEndOfFrame();
            isLocked = true;
            isTrapped = false;
            select.slotSelect = 0;
            yield return new WaitForSeconds(0.5f);
            isLocked = false;
        }
    }
}
