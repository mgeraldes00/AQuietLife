using System.Collections;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] private Rigidbody2D[] rb;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 10f;

    public GameObject[] objects;

    public bool leftDoorCol;
    public bool rightDoorCol;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (Input.GetMouseButton(0) && FindObjectOfType<GameManager>().isLocked == false
            && FindObjectOfType<CameraCtrl>().currentView == 1)
        {
            if (hit.collider == null)
            {

            }
            else if (hit.collider.CompareTag("Direction"))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
                rb[0].velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            }
            else if (hit.collider.CompareTag("Direction2"))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
                rb[1].velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            }
            else
            {
                for (int i = 0; i < rb.Length; i++)
                    rb[i].velocity = Vector2.zero;
            }
        }
    }

    public void ButtonBehaviour()
    {
        FindObjectOfType<CloseUpStorage>().Normalize();
        StartCoroutine(TimeToTransition());
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        //returnArrow.SetActive(false);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
    }

    public IEnumerator EnableObjs()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;
    }
}
