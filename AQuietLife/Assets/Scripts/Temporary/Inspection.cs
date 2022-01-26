using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection : MonoBehaviour
{
    public ThoughtManager thought;

    [SerializeField] private string inspectionText;

    private void OnMouseDown()
    {
        thought.ShowThought();
        thought.text = inspectionText;
    }
}
