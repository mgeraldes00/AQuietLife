using UnityEngine;

public class StorageManager : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] private Rigidbody2D[] rb;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 10f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (Input.GetMouseButton(0))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Slide");
    }
}
