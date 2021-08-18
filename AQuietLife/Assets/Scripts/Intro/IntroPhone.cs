using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPhone : MonoBehaviour
{
    private void OnMouseUp()
    {
        FindObjectOfType<IntroController>().PhoneOff();
    }
}
