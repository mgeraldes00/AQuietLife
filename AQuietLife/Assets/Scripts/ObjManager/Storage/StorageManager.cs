using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageManager : MonoBehaviour
{
    public CloseUpStorage closeUp;
    public CameraCtrl zoom;
    public GameManager gameMng;
    public Lock lockMgn;

    private Vector3 mousePosition;
    [SerializeField] private Rigidbody2D[] rb;
    private Vector2 direction;
    public float moveSpeed = 10f;
    public float moveSpeed2 = 10.0f;

    public GameObject[] objects;

    public bool leftDoorCol;
    public bool rightDoorCol;

    public bool isLocked;

    private void Start()
    {
        isLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
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
                    if (moveSpeed >= 0)
                        FindObjectOfType<AudioCtrl>().Play("OpenStorage");
                    else if (moveSpeed <= 0)
                        FindObjectOfType<AudioCtrl>().Play("CloseStorage");
                }
                else if (hit.collider.CompareTag("Direction2"))
                {
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    direction = (mousePosition - transform.position).normalized;
                    rb[1].velocity = new Vector2(direction.x * moveSpeed2, direction.y * moveSpeed2);
                    if (moveSpeed2 >= 0)
                        FindObjectOfType<AudioCtrl>().Play("OpenStorage");
                    else if (moveSpeed2 <= 0)
                        FindObjectOfType<AudioCtrl>().Play("CloseStorage");
                }
                else
                {
                    for (int i = 0; i < rb.Length; i++)
                        rb[i].velocity = Vector2.zero;
                }
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1)
        {
            FindObjectOfType<CloseUpStorage>().Normalize();
            StartCoroutine(TimeToTransition());
        }
        else
        {

        }
    }

    IEnumerator TimeToTransition()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
    }

    public IEnumerator EnableObjs()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = true;
    }

    public IEnumerator DisableObjs()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<BoxCollider2D>().enabled = false;
    }
}
