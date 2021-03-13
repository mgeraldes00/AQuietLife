using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introController : MonoBehaviour
{
    public ThoughtManager thought;

    public Text thoughtText;

    public Animator cameraAnim;

    public GameObject directionalButton;

    [SerializeField]
    private int currentPanel;

    private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&  isLocked == false)
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
                        thoughtText.text = "What's this doing here?";
                        break;
                }
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
                    currentPanel++;
                    isLocked = true;
                    StartCoroutine(EndTransition());
                    break;
            }
        }
    }

    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(1.0f);
        isLocked = false;
    }
}
