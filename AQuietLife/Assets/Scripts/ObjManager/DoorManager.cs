using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class DoorManager : MonoBehaviour
{
    public ObjectiveManager objective;
    public InventoryManager inventory;
    public ThoughtManager thought;

    public TextMeshProUGUI thoughtText;

    public RatingManager rating;

    public AudioSource doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("Mouse Clicked");
                Vector3 mousePos =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider.CompareTag("Door") && inventory.hasPlateWBread == true)
                {
                    //FindObjectOfType<AudioSource>().Play();
                    doorOpen.Play();
                    rating.EndLevel();
                    //SceneManager.LoadScene("MainMenu");
                }

                if (hit.collider.CompareTag("Door") && inventory.hasPlateWBread == false)
                {
                    //FindObjectOfType<AudioSource>().Play();
                    thought.ShowThought();
                    thoughtText.text = "Can't leave now, gotta make something to eat.";
                    //SceneManager.LoadScene("MainMenu");
                }
            }     
        }
    }
}
