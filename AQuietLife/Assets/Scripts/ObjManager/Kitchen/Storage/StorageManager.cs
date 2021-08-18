using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageManager : MonoBehaviour
{
    [SerializeField] private ObjectSelection inventory;
    [SerializeField] private ThoughtManager thought;
    public CloseUpStorage closeUp;
    public CameraCtrl zoom;
    public GameManager gameMng;

    private Vector3 mousePosition;
    [SerializeField] private Rigidbody2D[] rb;
    private Vector2 direction;
    public float moveSpeed = 10.0f;
    public float moveSpeed2 = 10.0f;

    public GameObject[] objectsLeft;
    public GameObject[] objectsRight;
    public GameObject[] doors;

    public bool leftDoorCol;
    public bool rightDoorCol;

    public bool isLocked;
    public bool isOnDoor;

    [SerializeField] private bool isOnLock;

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

            if (Input.GetMouseButtonUp(0) && FindObjectOfType<GameManager>().isLocked == false
                )//Fix currentView
            {
                if (hit.collider == null)
                {

                }
                else if (hit.collider.name == "keyHole")
                {
                    if (zoom.currentView == 1)
                    {
                        StartCoroutine(TimeToZoom());
                    }
                    else if (zoom.currentView == 2)
                    {
                        if (FindObjectOfType<InventorySimple>().keyInPossession == false)
                        {
                            thought.ShowThought();
                            thought.text = "Now, where did I put the key? Should be well hidden....";
                        }
                        else if (inventory.usingKey == true && isOnLock == true)
                        {
                            FindObjectOfType<Key>().keyUsed = true;
                            StartCoroutine(UnlockDoor());
                        }
                    }
                }
                else if (hit.collider.CompareTag("Direction") && zoom.currentView == 1)
                {
                    if (isLocked == false)
                    {
                        StartCoroutine(QuickUnlock());
                        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        //direction = (mousePosition - transform.position).normalized;
                        //rb[0].velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
                        if (moveSpeed >= 0)
                        {
                            doors[0].GetComponent<BoxCollider2D>().enabled = false;
                            doors[2].GetComponent<BoxCollider2D>().enabled = true;
                            StartCoroutine
                                (ObjectFade.FadeOut(doors[0].GetComponent<SpriteRenderer>(), 0));
                            StartCoroutine
                                (ObjectFade.FadeOut(doors[1].GetComponent<SpriteRenderer>(), 0));
                            StartCoroutine
                                (ObjectFade.FadeIn(doors[2].GetComponent<SpriteRenderer>()));
                            StartCoroutine
                                (ObjectFade.FadeIn(doors[3].GetComponent<SpriteRenderer>()));
                            FindObjectOfType<AudioCtrl>().Play("OpenStorage");
                            moveSpeed = -10;
                            leftDoorCol = true;
                            StartCoroutine(EnableObjs(0));
                        }
                        else if (moveSpeed <= 0)
                        {
                            doors[0].GetComponent<BoxCollider2D>().enabled = true;
                            doors[2].GetComponent<BoxCollider2D>().enabled = false;
                            StartCoroutine
                                (ObjectFade.FadeIn(doors[0].GetComponent<SpriteRenderer>()));
                            StartCoroutine
                                (ObjectFade.FadeIn(doors[1].GetComponent<SpriteRenderer>()));
                            StartCoroutine
                                (ObjectFade.FadeOut(doors[2].GetComponent<SpriteRenderer>(), 0));
                            StartCoroutine
                                (ObjectFade.FadeOut(doors[3].GetComponent<SpriteRenderer>(), 0));
                            FindObjectOfType<AudioCtrl>().Play("CloseStorage");
                            moveSpeed = 10;
                            leftDoorCol = false;
                            StartCoroutine(DisableObjs(0));
                        }
                    }
                }
                else if (hit.collider.CompareTag("Direction2") && zoom.currentView == 1)
                {
                    if (isLocked == false)
                    {
                        StartCoroutine(QuickUnlock());
                        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        //direction = (mousePosition - transform.position).normalized;
                        //rb[1].velocity = new Vector2(direction.x * moveSpeed2, direction.y * moveSpeed2);
                        if (moveSpeed2 >= 0)
                        {
                            doors[4].GetComponent<BoxCollider2D>().enabled = false;
                            doors[5].GetComponent<BoxCollider2D>().enabled = true;
                            StartCoroutine
                                (ObjectFade.FadeOut(doors[4].GetComponent<SpriteRenderer>(), 0));
                            StartCoroutine
                                (ObjectFade.FadeIn(doors[5].GetComponent<SpriteRenderer>()));
                            FindObjectOfType<AudioCtrl>().Play("OpenStorage");
                            moveSpeed2 = -10;
                            rightDoorCol = true;
                            StartCoroutine(EnableObjs(1));
                        }
                        else if (moveSpeed2 <= 0)
                        {
                            doors[4].GetComponent<BoxCollider2D>().enabled = true;
                            doors[5].GetComponent<BoxCollider2D>().enabled = false;
                            StartCoroutine
                                (ObjectFade.FadeIn(doors[4].GetComponent<SpriteRenderer>()));
                            StartCoroutine
                                (ObjectFade.FadeOut(doors[5].GetComponent<SpriteRenderer>(), 0));
                            FindObjectOfType<AudioCtrl>().Play("CloseStorage");
                            moveSpeed2 = 10;
                            rightDoorCol = false;
                            StartCoroutine(DisableObjs(1));
                        }
                    }
                }
                /*else
                {
                    for (int i = 0; i < rb.Length; i++)
                        rb[i].velocity = Vector2.zero;
                }*/
            }
        }
    }

    public void ButtonBehaviour()
    {
        if (zoom.currentView == 1)
        {
            if (isOnDoor == true)
            {
                FindObjectOfType<CloseUpStorage>().Normalize();
                StartCoroutine(DisableObjs(2));
            }
        }
        else if (zoom.currentView == 2 && isOnLock == true)
        {
            StartCoroutine(BackZoom());
        }
    }

    public IEnumerator EnableObjs(int thisObjs)
    {
        yield return new WaitForSeconds(0.3f);
        if (thisObjs == 0)
            for (int i = 0; i < objectsLeft.Length; i++)
                objectsLeft[i].GetComponent<BoxCollider2D>().enabled = true;
        else if (thisObjs == 1)
            for (int i = 0; i < objectsRight.Length; i++)
                objectsRight[i].GetComponent<BoxCollider2D>().enabled = true;
    }

    public IEnumerator DisableObjs(int thisObjs)
    {
        yield return new WaitForSeconds(0.3f);
        if (thisObjs == 0 || thisObjs > 1)
            for (int i = 0; i < objectsLeft.Length; i++)
                objectsLeft[i].GetComponent<BoxCollider2D>().enabled = false;
        if (thisObjs == 1 || thisObjs > 1)
            for (int i = 0; i < objectsRight.Length; i++)
                objectsRight[i].GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForEndOfFrame();
        zoom.currentView++;
        zoom.cameraAnim.SetTrigger("ZoomLock");
        yield return new WaitForSeconds(0.1f);
        isOnLock = true;
    }

    IEnumerator BackZoom()
    {
        yield return new WaitForSeconds(0.1f);
        //zoom.currentView--;
        isOnLock = false;
    }

    IEnumerator UnlockDoor()
    {
        yield return new WaitForSeconds(0.5f);
        isLocked = false;
        closeUp.lockedDoor.enabled = false;
        closeUp.doors[0].enabled = true;
        closeUp.keyHole.enabled = false;
    }

    IEnumerator QuickUnlock()
    {
        isLocked = true;
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
    }
}
