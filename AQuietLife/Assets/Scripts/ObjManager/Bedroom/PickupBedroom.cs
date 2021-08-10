using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBedroom : MonoBehaviour
{
    private InventorySimple inventory;

    public GameObject itemButton;

    [SerializeField] private string currentObj;

    [SerializeField] private GameObject[] pickObjs;

    [SerializeField] private bool isPicked;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySimple>();
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false && isPicked == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                FindObjectOfType<AudioCtrl>().Play(currentObj);
                StartCoroutine(Disappear());
                break;
            }
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
                gameObject.SetActive(false);
                break;
            case 2:
                StartCoroutine(ObjectFade.FadeOut(pickObjs[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(1.0f);
                gameObject.SetActive(false);
                break;
            case 3:
                StartCoroutine(ObjectFade.FadeOut(pickObjs[0].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[1].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ObjectFade.FadeOut(pickObjs[2].GetComponent<SpriteRenderer>()));
                yield return new WaitForSeconds(1.0f);
                gameObject.SetActive(false);
                break;
        }
    }
}
