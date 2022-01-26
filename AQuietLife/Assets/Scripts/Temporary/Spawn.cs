using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public CameraCtrl zoom;
    public ObjectSelection select;
    public GameManager gameMng;

    public Animator returnArrow;

    public GameObject[] objects;

    [SerializeField] private bool platePlaced;
    [SerializeField] private bool glassPlaced;
    [SerializeField] private bool breadPlaced;
    [SerializeField] private bool hamPlaced;

    public bool frozenBreadPlaced;

    [SerializeField] private bool drinkUsed;
    [SerializeField] private bool allIngredients;

    public BoxCollider2D table;

    public bool[] isFull;
    public GameObject[] slots;

    public GameObject[] ingredients;

    [SerializeField] private int test;

    private void OnMouseEnter()
    {
        if (zoom.currentView == 0 && gameMng.isLocked == false)
            FindObjectOfType<PointerManager>().ChangeCursor(5);
        else if (zoom.currentView == 1 && gameMng.isLocked == false)
            FindObjectOfType<PointerManager>().ChangeCursor(2);
    }

    private void OnMouseExit()
    {
        FindObjectOfType<PointerManager>().ChangeCursor(1);
    }

    private void OnMouseDown()
    {
        FindObjectOfType<PointerManager>().ChangeCursor(2);

        Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (FindObjectOfType<GameManager>().isLocked != true)
        {
            if (hit.collider == null)
            {
                //Nothing
            }

            else if (hit.collider.CompareTag("TableClose"))
            {
                if (zoom.currentView == 0)
                {
                    zoom.ObjectTransition();
                    zoom.cameraAnim.SetTrigger("ZoomTable");
                    StartCoroutine(TimeToZoom());
                    if (platePlaced == true)
                    {
                        table.size = new Vector2(3.48f, 1.89f);
                        table.offset = new Vector2(-21.83f, 0.56f);
                    }
                }
                else if (zoom.currentView == 1)
                {
                    if (select.usingIng == false && select.usingStruct == false)
                    {
                        if (frozenBreadPlaced == true)
                        {
                            FindObjectOfType<PickupFrozenBread>().PickBread();
                            platePlaced = false;
                            frozenBreadPlaced = false;
                            FindObjectOfType<AudioCtrl>().Play("PickPlate");
                        }
                    }

                    if (select.usingPlate == true)
                    {
                        objects[0].SetActive(true);
                        FindObjectOfType<Plate>().plateUsed = true;
                        platePlaced = true;
                        table.size = new Vector2(3.48f, 1.89f);
                        table.offset = new Vector2(-21.83f, 0.56f);
                        FindObjectOfType<AudioCtrl>().Play("PlacePlate");
                    }

                    if (select.usingGlass == true)
                    {
                        objects[2].SetActive(true);
                        FindObjectOfType<Glass>().glassUsed = true;
                        glassPlaced = true;
                        FindObjectOfType<AudioCtrl>().Play("PlacePlate");
                    }

                    if (glassPlaced == true)
                    {
                        if (select.usingMilk)
                        {
                            objects[2].SetActive(false);
                            objects[3].SetActive(true);
                            objects[10].SetActive(true);
                            FindObjectOfType<Milk>().milkUsed = true;
                            drinkUsed = true;
                            FindObjectOfType<AudioCtrl>().Play("DropDrink");
                        }
                        if (select.usingJuice)
                        {
                            objects[2].SetActive(false);
                            objects[4].SetActive(true);
                            FindObjectOfType<Juice>().juiceUsed = true;
                            drinkUsed = true;
                            FindObjectOfType<AudioCtrl>().Play("DropDrink");
                        }
                        if (select.usingBottle)
                        {
                            objects[2].SetActive(false);
                            objects[5].SetActive(true);
                            FindObjectOfType<Bottle>().bottleUsed = true;
                            drinkUsed = true;
                            FindObjectOfType<AudioCtrl>().Play("DropDrink");
                        }
                    }

                    if (select.usingBread == true && platePlaced == true)
                    {
                        breadPlaced = true;
                        objects[1].SetActive(true);
                        FindObjectOfType<Bread>().breadUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("BreadPlate");
                    }

                    if (select.usingFrozenBread == true && platePlaced == true)
                    {
                        frozenBreadPlaced = true;
                        objects[1].SetActive(true);
                        FindObjectOfType<FrozenBread>().frozenBreadUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("BreadPlate");
                    }

                    if (select.usingPlateWBread == true)
                    {
                        platePlaced = true;
                        breadPlaced = true;
                        objects[0].SetActive(true);
                        objects[1].SetActive(true);
                        FindObjectOfType<PlateWBread>().breadWPlateUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("PlacePlate");
                    }

                    if (select.usingHam == true && breadPlaced == true && hamPlaced == false)
                    {
                        hamPlaced = true;
                        //objects[2].SetActive(true);
                        for (test = 0; test < slots.Length; test++)
                        {
                            if (isFull[test] == false)
                            {
                                isFull[test] = true;
                                Instantiate(ingredients[0], slots[test].transform, false);
                                OrderInLayer(test, 0);
                                break;
                            }
                        }
                        FindObjectOfType<Ham>().hamUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("PlaceHam");
                        CheckIng();
                    }

                    if (select.usingLettuce == true && breadPlaced == true)
                    {
                        for (test = 0; test < slots.Length; test++)
                        {
                            if (isFull[test] == false)
                            {
                                isFull[test] = true;
                                Instantiate(ingredients[1], slots[test].transform, false);
                                OrderInLayer(test, 1);
                                break;
                            }
                        }
                        FindObjectOfType<Lettuce>().lettuceUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("PlaceLettuce");
                        CheckIng();
                    }

                    if (select.usingCheese == true && breadPlaced == true)
                    {
                        for (test = 0; test < slots.Length; test++)
                        {
                            if (isFull[test] == false)
                            {
                                isFull[test] = true;
                                Instantiate(ingredients[3], slots[test].transform, false);
                                OrderInLayer(test, 3);
                                break;
                            }
                        }
                        FindObjectOfType<Cheese>().cheeseUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("PlaceHam");
                        CheckIng();
                    }

                    if (select.usingPickle == true && breadPlaced == true)
                    {
                        for (test = 0; test < slots.Length; test++)
                        {
                            if (isFull[test] == false)
                            {
                                isFull[test] = true;
                                Instantiate(ingredients[4], slots[test].transform, false);
                                objects[6].SetActive(true);
                                OrderInLayer(test, 4);
                                break;
                            }
                        }
                        FindObjectOfType<PickleJar>().pickleUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("PlacePickle");
                        CheckIng();
                    }

                    if (select.usingTomato == true && breadPlaced == true)
                    {
                        for (test = 0; test < slots.Length; test++)
                        {
                            if (isFull[test] == false)
                            {
                                isFull[test] = true;
                                Instantiate(ingredients[2], slots[test].transform, false);
                                objects[9].SetActive(true);
                                OrderInLayer(test, 2);
                                break;
                            }
                        }
                        FindObjectOfType<Tomato>().tomatoUsed = true;
                        FindObjectOfType<AudioCtrl>().Play("PlacePlate");
                        CheckIng();
                    }

                    if (allIngredients == true && drinkUsed == true)
                    {
                        objects[7].SetActive(false);
                        objects[8].SetActive(true);
                        StartCoroutine(gameMng.EndGame());
                    }
                }
            }
        }  
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //returnArrow.SetTrigger("Show");
        /*for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;*/
        zoom.currentView++;
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1)
        {
            Normalize();
            StartCoroutine(TimeToTransition());
        }
        else
        {

        }
    }

    public void Normalize()
    {
        table.size = new Vector2(5.98f, 1.9f);
        table.offset = new Vector2(-23f, 0);
        /*for (int i = 0; i < zoomableObjs.Length; i++)
            zoomableObjs[i].enabled = false;*/
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        /*for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;*/
    }

    public void OrderInLayer(int b, int i)
    {
        switch (b)
        {
            case 0:
                ingredients[i].GetComponent<SpriteRenderer>().sortingOrder = 10;
                break;
            case 1:
                ingredients[i].GetComponent<SpriteRenderer>().sortingOrder = 11;
                break;
            case 2:
                ingredients[i].GetComponent<SpriteRenderer>().sortingOrder = 12;
                break;
            case 3:
                ingredients[i].GetComponent<SpriteRenderer>().sortingOrder = 13;
                break;
            case 4:
                ingredients[i].GetComponent<SpriteRenderer>().sortingOrder = 14;
                break;
        }
    }

    void CheckIng()
    {
        if (test >= 4)
            allIngredients = true;
    }
}
