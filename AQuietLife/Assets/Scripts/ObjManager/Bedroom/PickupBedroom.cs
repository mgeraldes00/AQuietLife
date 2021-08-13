using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBedroom : MonoBehaviour
{
    private InventorySimple inventory;
    [SerializeField] private ObjectSelection select;

    public GameObject itemButton;

    [SerializeField] private string currentObj;

    [SerializeField] private GameObject[] pickObjs;

    public bool isPicked;

    [SerializeField] private bool isTrapped;
    [SerializeField] private bool isLocked;



    private void Start()
    {
        inventory =
            GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
        select =
            GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false && isPicked == false
                && isLocked == false)
            {
                if (isTrapped == true)
                {
                    StartCoroutine(CheckForGlove());
                }
                else
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    FindObjectOfType<AudioCtrl>().Play(currentObj);
                    StartCoroutine(Disappear());
                    break;
                }
            }
        }
    }

    IEnumerator CheckForGlove()
    {
        if (select.selectedObject != "Glove")
        {
            //Game Over
            Debug.Log("GAME OVER");
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

    IEnumerator Disappear()
    {
        yield return new WaitForEndOfFrame();
        isPicked = true;
        switch (pickObjs.Length)
        {
            case 1:
                StartCoroutine(ObjectFade.FadeOut(pickObjs[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(1.0f);
                //gameObject.SetActive(false);
                Destroy(gameObject);
                break;
            case 2:
                StartCoroutine(ObjectFade.FadeOut(pickObjs[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(1.0f);
                //gameObject.SetActive(false);
                Destroy(gameObject);
                break;
            case 3:
                StartCoroutine(ObjectFade.FadeOut(pickObjs[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[2].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(1.0f);
                //gameObject.SetActive(false);
                Destroy(gameObject);
                break;
            case 4:
                StartCoroutine(ObjectFade.FadeOut(pickObjs[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[2].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[3].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(1.0f);
                Destroy(gameObject);
                break;
            case 5:
                StartCoroutine(ObjectFade.FadeOut(pickObjs[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[2].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[3].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[4].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(1.0f);
                Destroy(gameObject);
                break;
        }
    }
}
