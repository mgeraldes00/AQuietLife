﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowMouse : MonoBehaviour
{
    [SerializeField] private RectTransform movingObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private RectTransform basisObject;
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gm;

    [SerializeField] private Animator cursorAnim;

    [SerializeField] private Image[] cursorImg;

    private void Update()
    {
        MoveObject();
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = basisObject.position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public void RemoveCursorForUI()
    {
        cursorAnim.SetBool("OpenDoor", false);
        cursorAnim.SetBool("Grab", false);
        cursorAnim.SetBool("Inspect", false);

        cursorAnim.ResetTrigger("OpenR2L");
        cursorAnim.ResetTrigger("OpenL2R");
    }

    public void ChangeCursor(string curName, int i)
    {
        if (!gm.isCursorLocked)
        {
            if (curName == "OpenDoor")
            {
                switch (i)
                {
                    case 0:
                        cursorAnim.SetBool("OpenDoor", false);
                        cursorAnim.ResetTrigger("OpenR2L");
                        cursorAnim.ResetTrigger("OpenL2R");
                        break;
                    case 1:
                        cursorAnim.SetBool("OpenDoor", true);
                        cursorAnim.SetTrigger("OpenR2L");
                        break;
                    case 2:
                        cursorAnim.SetBool("OpenDoor", true);
                        cursorAnim.SetTrigger("OpenL2R");
                        break;
                    case 3:
                        cursorAnim.SetBool("OpenDoor", true);
                        cursorAnim.SetTrigger("OpenU2D");
                        break;
                    case 4:
                        cursorAnim.SetBool("OpenDoor", true);
                        cursorAnim.SetTrigger("OpenD2U");
                        break;
                }
            }
            else
                switch (i)
                {
                    case 0:
                        cursorAnim.SetBool(curName, false);
                        break;
                    case 1:
                        cursorAnim.SetBool(curName, true);
                        break;
                }
        }
        /*switch (curName)
        {
            case "Open":
                switch (i)
                {
                    case 0:
                        cursorAnim.SetBool("Open", false);
                        break;
                    case 1:
                        cursorAnim.SetBool("Open", true);
                        break;
                }
                break;
            case "Slide":
                switch (i)
                {
                    case 0:
                        cursorAnim.SetBool("Slide", false);
                        break;
                    case 1:
                        cursorAnim.SetBool("Slide", true);
                        break;
                }
                break;
            case "Point":
                switch (i)
                {
                    case 0:
                        //StartCoroutine(ObjectFade.FadeOutUI(cursorImg[1], 0.5f));
                        //StartCoroutine(ObjectFade.FadeInUI(cursorImg[0], 0.5f));
                        cursorAnim.SetBool("Point", false);
                        break;
                    case 1:
                        //StartCoroutine(ObjectFade.FadeInUI(cursorImg[1], 0.5f));
                        //StartCoroutine(ObjectFade.FadeOutUI(cursorImg[0], 0.5f));
                        cursorAnim.SetBool("Point", true);
                        break;
                }
                break;
            case "Grab":
                switch (i)
                {
                    case 0:
                        cursorAnim.SetBool("Grab", false);
                        break;
                    case 1:
                        cursorAnim.SetBool("Grab", true);
                        break;
                }
                break;
            case "Inspect":
                switch (i)
                {
                    case 0:
                        cursorAnim.SetBool("Inspect", false);
                        break;
                    case 1:
                        cursorAnim.SetBool("Inspect", true);
                        break;
                }
                break;
            case "OpenDoor":
                switch (i)
                {
                    case 0:
                        cursorAnim.SetBool("OpenDoor", false);
                        break;
                    case 1:
                        cursorAnim.SetBool("OpenDoor", true);
                        cursorAnim.SetTrigger("OpenR2L");
                        break;
                }
                break;
        }*/
    }
}
