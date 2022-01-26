using UnityEngine;

public class IngredientSlot : MonoBehaviour
{
    private Spawn table;
    public int i;

    private void Start()
    {
        table = GameObject.FindGameObjectWithTag("TableClose").GetComponent<Spawn>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            table.isFull[i] = false;
        }
    }
}
