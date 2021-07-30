using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] private CloseUpStorage closeUp;
    [SerializeField] private StorageManager storage;
    [SerializeField] private CameraCtrl zoom;
    [SerializeField] private ObjectSelection inventory;
    [SerializeField] private ThoughtManager thought;

    public BoxCollider2D keyBox;

    public bool isOnLock;

    private void OnMouseDown()
    {
        zoom.cameraAnim.SetTrigger("ZoomLock");
        for (int i = 0; i < closeUp.doors.Length; i++)
            closeUp.doors[i].enabled = false;
        StartCoroutine(TimeToZoom());
        if (zoom.currentView == 1)
        {
            
        }
        else if (zoom.currentView == 2)
        {
            if (inventory.usingKey == false)
            {
                thought.ShowThought();
                thought.text = "Now, where did I put the key? Should be well hidden....";
            }
            else
            {
                
            }
        }
    }

    IEnumerator TimeToZoom()
    {
        yield return new WaitForSeconds(0.5f);
        zoom.currentView++;
        isOnLock = true;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForEndOfFrame();
        storage.isLocked = false;
    }
}
