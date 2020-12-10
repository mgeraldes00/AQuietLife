using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public ClockManager clock;
    public InventoryManager inventory;
    public CloseUpTable closeUp;

    public GameObject tableGeneral;
    public GameObject tableGeneralWPlate;

    public GameObject returnArrow;
    public GameObject table;
    public GameObject plate;
    public GameObject bread;
    public GameObject knife;

    private bool plateOnTable;
    private bool breadOnTable;
    private bool knifeOnTable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("Nothing"))
            {
                switch (plateOnTable)
                {
                    case (true):
                        for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                            closeUp.zoomableObjs[i].enabled = true;
                        tableGeneralWPlate.SetActive(true);
                        table.SetActive(false);
                        returnArrow.SetActive(false);
                        return;
                    case (false):
                        for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                            closeUp.zoomableObjs[i].enabled = true;
                        tableGeneral.SetActive(true);
                        table.SetActive(false);
                        returnArrow.SetActive(false);
                        return;
                }

                /*if (plateOnTable == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWPlate.SetActive(true);
                }

                if (plateOnTable == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneral.SetActive(true);
                }*/

                //table.SetActive(false);
                //returnArrow.SetActive(false);
            }

            if (hit.collider.CompareTag("Table") && inventory.hasPlate == true)
            {
                plate.SetActive(true);
                inventory.PlateOffInventory();
                plateOnTable = true;
            }

            if (hit.collider.CompareTag("Table") && inventory.hasBread == true)
            {
                bread.SetActive(true);
                inventory.BreadOffInventory();
                breadOnTable = true;
            }

            if (hit.collider.CompareTag("Table") && inventory.hasKnife == true)
            {
                knife.SetActive(true);
                inventory.KnifeOffInventory();
                knifeOnTable = true;
            }

            if (knifeOnTable == true && breadOnTable == true)
            {
                //Bread is cut
            }
        }
    }
}
