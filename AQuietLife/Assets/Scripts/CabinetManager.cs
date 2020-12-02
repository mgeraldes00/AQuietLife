using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetManager : MonoBehaviour
{
    public GameObject cabinetGeneral;
    public GameObject cabinet;
    public GameObject plate;

    public Animator cabinetAnim;
    public Animator plateAnim;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null)
            {
                cabinetGeneral.SetActive(true);
                cabinet.SetActive(false);
                //plate.SetActive(false);
            }

            else if (hit.collider != null)
            {
                cabinetAnim.SetBool("Open", true);
                //StartCoroutine(PlateToAppear());
                StartCoroutine(PlateToAnimate());
                StartCoroutine(CabinetToClose());
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
    IEnumerator PlateToAppear()
    {
        yield return new WaitForSeconds(1);
        plate.SetActive(true);        
    }

    IEnumerator PlateToAnimate()
    {
        yield return new WaitForSeconds(2);
        plateAnim.SetBool("Taken", true);
    }

    IEnumerator CabinetToClose()
    {
        yield return new WaitForSeconds(4);
        cabinetAnim.SetBool("Open", false);
        plateAnim.SetBool("Taken", false);
    }
}
