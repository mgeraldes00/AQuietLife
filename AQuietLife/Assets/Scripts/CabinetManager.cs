using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetManager : MonoBehaviour
{
    public ClockManager clock;
    public InventoryManager inventory;
    public GameOver gameOver;
    public CloseUpCabinet closeUp;

    public GameObject cabinetGeneral;
    public GameObject cabinetDoor2Open;
    public GameObject cabinetDoor4Open;
    public GameObject cabinetDoor24Open;
    public GameObject cabinetDoor24OpenNoPlate; 
    public GameObject cabinetDoor4OpenNoPlate;

    public GameObject returnArrow;
    public GameObject cabinetButtons;   
    public GameObject cabinet;
    public GameObject plate;
    public GameObject plateInteract;
    public GameObject glass;

    public Animator door1Anim;
    public Animator door2Anim;
    public Animator door3Anim;
    public Animator door4Anim;
    public Animator glassAnim;
    public Animator plateAnim;

    private Animator anim;

    public AudioSource doorSound;

    private bool isLocked;
    private bool door2Open;
    private bool door4Open;
    private bool plateTaken;

    public bool hasTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isLocked = false;
        hasTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isLocked == false)
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos = 
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("Nothing"))
            {
                if (door2Open == true && door4Open == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor2Open.SetActive(true);
                }
                if (door4Open == true && door2Open == false && plateTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor4Open.SetActive(true);
                }
                if (door2Open == true && door4Open == true && plateTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor24Open.SetActive(true);
                }
                if (door4Open == true && door2Open == false && plateTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor4OpenNoPlate.SetActive(true);
                }
                if (door2Open == true && door4Open == true && plateTaken == true)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetDoor24OpenNoPlate.SetActive(true);
                }
                if (door2Open == false && door4Open == false && plateTaken == false)
                {
                    for (int i = 0; i < closeUp.zoomableObjs.Length; i++)
                        closeUp.zoomableObjs[i].enabled = true;
                    cabinetGeneral.SetActive(true);
                } 
                
                cabinet.SetActive(false);
                returnArrow.SetActive(false);
                cabinetButtons.SetActive(false);
            }

            if (hit.collider.CompareTag("Cabinet"))
            {
                //door1Anim.SetTrigger("Door1OpenFull");
                //plate.SetActive(true);
                //glass.SetActive(true);
                //StartCoroutine(CabinetRewind());
                cabinetButtons.SetActive(true);
            }

            if (hit.collider.CompareTag("CabinetDoor1") ||
                hit.collider.CompareTag("CabinetDoor3"))
            {
                Debug.Log("Game Over");
                gameOver.Die();
            }

            if (hit.collider.CompareTag("CabinetDoor2"))
            {
                door2Anim.SetBool("Door2Open", true);
                door2Open = true;
                doorSound.Play();
            }

            if (hit.collider.CompareTag("CabinetDoor4"))
            {
                door4Anim.SetBool("Door4Open", true);
                door4Open = true;
                plateInteract.SetActive(true);
                doorSound.Play();
            }

            if (hit.collider.CompareTag("Plate"))
            {
                plateInteract.SetActive(false);
                inventory.PlateInInventory();
                plateTaken = true;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("Base Layer.cabinetOpen", 0, 0.25f);
            }
        }*/
    }

    IEnumerator CabinetRewind()
    {
        yield return new WaitForSeconds(2);
        glassAnim.SetTrigger("GlassTaken");

        yield return new WaitForSeconds(2);
        glass.SetActive(false);

        yield return new WaitForSeconds(2);
        door3Anim.SetTrigger("Door3OpenFull");

        yield return new WaitForSeconds(2);
        plateAnim.SetTrigger("PlateTaken");

        yield return new WaitForSeconds(2);
        plate.SetActive(false);
    }

    public void ButtonBehaviour(int i)
    {
        if (isLocked == false && hasTime == true)
        {
            switch (i)
            {
                case (0):
                default:
                    glass.SetActive(true);
                    door1Anim.SetTrigger("Door1Part1");
                    doorSound.Play();
                    clock.Drain();
                    break;
                case (1):
                    glass.SetActive(true);
                    door1Anim.SetTrigger("Door1Part2");
                    glassAnim.SetTrigger("GlassTaken");
                    clock.Drain();
                    break;
                case (2):
                    glass.SetActive(false);
                    door1Anim.SetTrigger("Door1Part3");
                    clock.Drain();
                    break;
                case (3):
                    plate.SetActive(true);
                    door3Anim.SetTrigger("Door3Part1");
                    doorSound.Play();
                    clock.Drain();
                    break;
                case (4):
                    plate.SetActive(true);
                    door3Anim.SetTrigger("Door3Part2");
                    plateAnim.SetTrigger("PlateTaken");
                    clock.Drain();
                    break;
                case (5):
                    plate.SetActive(false);
                    door3Anim.SetTrigger("Door3Part3");
                    clock.Drain();
                    break;
            }
        }
        
    }

    public void LockAndUnlock()
    {
        if (isLocked == false && hasTime == true)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock());
        }        
    }

    public void NoMoreTime()
    {
        hasTime = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        returnArrow.SetActive(true);
    }
}
