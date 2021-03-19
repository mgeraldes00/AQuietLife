using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class introController : MonoBehaviour
{
    public ThoughtManager thought;

    public Text thoughtText;

    public Animator cameraAnim;
    public Animator shutterAnim;
    public Animator deathAnim;

    public GameObject directionalButton;
    public GameObject sceneCloseUp;

    public GameObject[] deathScreen;

    [SerializeField]
    private int currentPanel;

    private bool isLocked;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&  isLocked == false 
            && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("NoTag"))
            {
                thought.ShowThought();

                switch (currentPanel)
                {
                    case 0:
                        thoughtText.text = "Just gonna put something on before leaving.";
                        break;
                    case 1:
                        thoughtText.text = "Need some light in here.";
                        break;
                    case 2:
                        thoughtText.text = "What's this doing here?";
                        cameraAnim.SetTrigger("ZoomIn");
                        isLocked = true;
                        StartCoroutine(EndTransition());
                        break;
                    case 3:
                        Die();
                        break;
                }
            }
            
            if (hit.collider.CompareTag("Nothing"))
            {
                shutterAnim.SetTrigger("Open");
                isLocked = true;
                StartCoroutine(EndTransition());
            }
        }
    }

    public void ButtonBehaviour(int i)
    {
        if (isLocked == false)
        {
            switch (i)
            {
                case 1:
                    if (currentPanel == 0)
                    {
                        cameraAnim.SetTrigger("turn");
                        directionalButton.SetActive(false);
                    }
                    //currentPanel++;
                    isLocked = true;
                    StartCoroutine(EndTransition());
                    break;
            }
        }
    }

    public void Die()
    {
        isLocked = true;
        isDead = true;
        deathScreen[0].SetActive(true);
        //Destroy(scene);
        Destroy(sceneCloseUp);
        StartCoroutine(DeathProcess());
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
        currentPanel++;
    }

    IEnumerator DeathProcess()
    {
        yield return new WaitForSeconds(2);
        /*for (int i = 0; i < 4; i++)
            deathScreen[i].SetActive(true);
        deathAnim.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        deathScreen[4].SetActive(false);*/
        Application.Quit();
    }
}
