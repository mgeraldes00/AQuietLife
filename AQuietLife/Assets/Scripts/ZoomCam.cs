using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjCtrl.zoomActive == "y")
        {
            GetComponent<Camera>().enabled = true;
        }

        if (Input.GetKeyDown("mouse 0"))
        {
            GetComponent<Camera>().enabled = false;
            ObjCtrl.zoomActive = "n";
        }
    }
}
