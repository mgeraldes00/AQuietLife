using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticObj : MonoBehaviour
{
    private GameManager gameMng;

    [SerializeField] private GameObject[] objectVar;

    [SerializeField] private string doorType;

    [SerializeField] private bool isDoor;
    [SerializeField] private bool isEndDoor;
    [SerializeField] private bool isLocked;

    [SerializeField] private int useState;

    private void Start()
    {
        gameMng = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
            && isLocked == false)
        {
            if (isDoor)
            {
                switch (useState)
                {
                    case 0:
                        StartCoroutine
                            (ObjectFade.FadeOut(objectVar[0].GetComponent<SpriteRenderer>(), 0));
                        StartCoroutine
                            (ObjectFade.FadeIn(objectVar[1].GetComponent<SpriteRenderer>()));
                        StartCoroutine(SwitchCollider(0, doorType));
                        break;
                    case 1:
                        StartCoroutine
                            (ObjectFade.FadeOut(objectVar[1].GetComponent<SpriteRenderer>(), 0));
                        StartCoroutine
                            (ObjectFade.FadeIn(objectVar[0].GetComponent<SpriteRenderer>()));
                        StartCoroutine(SwitchCollider(1, doorType));
                        break;
                }
            }
            else if (isEndDoor)
            {
                if (FindObjectOfType<CameraCtrl>().currentView > 0)
                    StartCoroutine
                        (ObjectFade.FadeOut(objectVar[0].GetComponent<SpriteRenderer>(), 0));
            }
            isLocked = true;
            StartCoroutine(gameMng.QuickUnlock(1.0f));
        }
    }

    IEnumerator SwitchCollider(int i, string doorType)
    {
        yield return new WaitForEndOfFrame();
        switch (useState)
        {
            case 0:
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-8.92f, 0.46f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 9.44f);
                if (doorType == "WardrobeRightDoor")
                {
                    FindObjectOfType<Wardrobe>().objects[0].enabled = true;
                    FindObjectOfType<Wardrobe>().rightDoorOpen = true;
                }
                break;
            case 1:
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-7.09f, 0.46f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3.84f, 9.44f);
                if (doorType == "WardrobeRightDoor")
                {
                    FindObjectOfType<Wardrobe>().objects[0].enabled = false;
                    FindObjectOfType<Wardrobe>().rightDoorOpen = false;
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
}
