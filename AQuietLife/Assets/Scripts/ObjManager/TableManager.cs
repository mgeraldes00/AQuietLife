using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    //public ClockManager clock;
    public InventoryManager inventory;
    public ThoughtManager thought;
    public GameManager gameMng;
    //public CloseUpTable closeUp;
    public ObjectiveManager objective;

    public Text thoughtText;

    /*public GameObject tableGeneral;
    public GameObject tableGeneralWPlate;
    public GameObject tableGeneralWKnife;
    public GameObject tableGeneralWPlateKnife;
    public GameObject tableGeneralWPlateBread;
    public GameObject tableGeneralWPlateKnifeBread;
    public GameObject tableGeneralWPlateKnifeBreadUncut;
    public GameObject tableGeneralWPlateKnifeBreadHam;*/

    public BoxCollider2D tableCollider;

    public GameObject returnArrow;
    //public GameObject noTextCollidersGeneral;
    //public GameObject noTextColliderTable;
    //public GameObject interactionText;
    public GameObject table;
    public GameObject plate;
    public GameObject bread;
    public GameObject knife;
    public GameObject breadCut;
    public GameObject ham;
    
    [SerializeField]
    private bool allOnTable;
    [SerializeField]
    private bool plateTaken;

    public bool plateOnTable;
    public bool breadOnTable;
    public bool frozenBreadOnTable;
    public bool knifeOnTable;
    public bool hamOnTable;

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

            /*if (hit.collider.CompareTag("Nothing"))
            {
                if (plateOnTable == true && breadOnTable == false 
                    && frozenBreadOnTable == false && knifeOnTable == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWPlate.SetActive(true);
                }

                if (plateOnTable == false && breadOnTable == false 
                    && frozenBreadOnTable == false && knifeOnTable == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWKnife.SetActive(true);
                }

                if (plateOnTable == true && breadOnTable == false 
                    && frozenBreadOnTable == false && knifeOnTable == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWPlateKnife.SetActive(true);
                }

                if (plateOnTable == true && breadOnTable == true && knifeOnTable == false
                    || plateOnTable == true && frozenBreadOnTable == true && knifeOnTable == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWPlateBread.SetActive(true);
                }

                if (plateOnTable == true && breadOnTable == true 
                    && knifeOnTable == true && plateTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWPlateKnifeBread.SetActive(true);
                }

                if (plateOnTable == true && frozenBreadOnTable == true
                    && knifeOnTable == true && plateTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWPlateKnifeBreadUncut.SetActive(true);
                }

                if (plateOnTable == false && breadOnTable == false && knifeOnTable == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneral.SetActive(true);
                }

                if (plateTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    tableGeneralWKnife.SetActive(true);
                }

                table.SetActive(false);
                returnArrow.SetActive(false);
                noTextCollidersGeneral.SetActive(true);
                noTextColliderTable.SetActive(false);
            }*/

            if (hit.collider.CompareTag("TableClose") && inventory.hasPlate == true)
            {
                plate.SetActive(true);
                inventory.PlateOffInventory();
                plateOnTable = true;
                //interactionText.SetActive(false);
            }

            if (hit.collider.CompareTag("TableClose") && inventory.hasBread == true)
            {
                bread.SetActive(true);
                inventory.BreadOffInventory();
                breadOnTable = true;
                //interactionText.SetActive(false);
            }

            if (hit.collider.CompareTag("TableClose") && inventory.hasFrozenBread == true)
            {
                bread.SetActive(true);
                inventory.FrozenBreadOffInventory();
                frozenBreadOnTable = true;
                //interactionText.SetActive(false);
            }

            if (hit.collider.CompareTag("TableClose") && inventory.hasKnife == true)
            {
                knife.SetActive(true);
                inventory.KnifeOffInventory();
                knifeOnTable = true;
                //interactionText.SetActive(false);
            }

            if (hit.collider.CompareTag("TableClose") && inventory.hasBreadInPlate == true)
            {
                plate.SetActive(true);
                bread.SetActive(true);
                inventory.PlateBreadUncutUnfrozenOffInventory();
                plateOnTable = true;
                breadOnTable = true;
                plateTaken = false;
                //interactionText.SetActive(false);
            }

            if (hit.collider.CompareTag("TableClose") && inventory.hasHam == true)
            {
                ham.SetActive(true);
                inventory.HamOffInventory();
                hamOnTable = true;
                allOnTable = true;
                tableCollider.enabled = false;
            }

            if (breadOnTable == true && knifeOnTable == true && plateTaken == false)
            {
                bread.SetActive(false);
                breadCut.SetActive(true);
                objective.part1Complete = true;
                //allOnTable = true;
                //tableCollider.enabled = false;
            }

            if (hit.collider.CompareTag("Plate"))
            {
                if (frozenBreadOnTable == true)
                {
                    thought.ShowThought();
                    thoughtText.text = "Can't cut into the bread in this state...";
                }
            }

            if (hit.collider.CompareTag("Plate")
                && frozenBreadOnTable == true)
            {
                plate.SetActive(false);
                bread.SetActive(false);
                plateOnTable = false;
                frozenBreadOnTable = false;
                inventory.PlateBreadUncutInInventory();
                plateTaken = true;
            }

            if (hit.collider.CompareTag("Plate") 
                && allOnTable == true)
            {
                plate.SetActive(false);
                breadCut.SetActive(false);
                ham.SetActive(false);
                inventory.BreadCutInInventory();
                plateTaken = true;
                gameMng.pickUpTextFinal.SetActive(false);
            }
        }
    }
}
