using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCtrl : MonoBehaviour
{
    public static string zoomActive = "n";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomActive == "n")
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnMouseUp()
    {
        zoomActive = "y";
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
