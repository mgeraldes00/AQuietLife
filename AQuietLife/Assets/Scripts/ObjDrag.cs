using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDrag : MonoBehaviour
{
    public IntroController intro;

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 700f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (Input.GetMouseButton(0) && hit.collider.CompareTag("NoTag"))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Slide");
        intro.PhoneOff();
    }
}
