using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public InventoryManager inventory;

    public GameObject[] scratchListPart1;
    public GameObject[] scratchListPart2;

    public GameObject[] list1;
    public GameObject[] list2;

    public Animator list1Ctrl;
    public Animator list2Ctrl;
    public Animator background;

    public GameObject listScratch;

    public bool hasPlate;
    public bool hasBread;
    public bool hasKnife;
    public bool hasHam;
    public bool part1Complete;

    [SerializeField]
    private bool isScratching;


    // Start is called before the first frame update
    void Start()
    {
        isScratching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlate == true)
        {
            scratchListPart1[0].SetActive(true);
            listScratch.SetActive(true);
            StartCoroutine(resetScratch());
        }

        if (hasBread == true)
        {
            scratchListPart1[1].SetActive(true);
            listScratch.SetActive(true);
            StartCoroutine(resetScratch());
        }

        if (hasKnife == true)
        {
            scratchListPart1[2].SetActive(true);
            listScratch.SetActive(true);
            StartCoroutine(resetScratch());

        }

        if (inventory.hamUsed == true && hasHam == true)
        {
            scratchListPart2[0].SetActive(true);
            listScratch.SetActive(true);
            StartCoroutine(resetScratch());
        }

        if (part1Complete == true)
        {
            list1Ctrl.SetTrigger("ObjectivesDone");
            StartCoroutine(newObjectives());          
            for (int i = 0; i < list2.Length; i++)
                list2[i].SetActive(true);
        }
    }

    IEnumerator newObjectives()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < list1.Length; i++)
            list1[i].SetActive(true);
        list2Ctrl.SetTrigger("ObjectivesDone");
        background.SetTrigger("ObjectivesDone");
    }

    IEnumerator resetScratch()
    {
        yield return new WaitForSeconds(1);
        listScratch.SetActive(false);
        hasPlate = false;
        hasBread = false;
        hasKnife = false;
        hasHam = false;
    }
}
