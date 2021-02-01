using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RatingManager : MonoBehaviour
{
    public GameManager gameMng;
    public InventoryManager inventory;
    public DoorManager door;

    public GameObject rating;
    public GameObject gloveTextObj;
    public GameObject[] buttons;

    public Animator doorAnim;
    public Animator cameraAnim;
    public Animator ratingAnim;

    private float delay = 0.1f;

    [SerializeField]
    private string gloveText;

    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonBehaviour(int i)
    {
        switch (i)
        {
            case (0):
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case (1):
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    public void EndLevel()
    {
        gameMng.isLocked = true;
        doorAnim.SetTrigger("Open");
        cameraAnim.SetTrigger("LevelEnd");
        rating.SetActive(true);
        StartCoroutine(ShowRating());
        StartCoroutine(ShowGloveText());
    }

    IEnumerator ShowRating()
    {
        yield return new WaitForSeconds(2);
        ratingAnim.SetTrigger("LevelEnd");
        yield return new WaitForSeconds(1);
        if (gameMng.numOfIngredients == 1 && gameMng.glovesUsed == 2)
        {
            ratingAnim.SetTrigger("Has1Star");
        }           
        if (gameMng.glovesUsed == 1)
        {
            ratingAnim.SetTrigger("Has1HalfStar");
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].SetActive(true);
    }

    IEnumerator ShowGloveText()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < gloveText.Length; i++)
        {
            currentText = gloveText.Substring(0, i);
            gloveTextObj.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
