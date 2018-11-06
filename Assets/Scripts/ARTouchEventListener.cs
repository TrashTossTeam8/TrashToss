using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTouchEventListener : MonoBehaviour
{

    void OnMouseDown()
    {
        var spawner = FindObjectOfType<ARSpawnScript>();
        if (spawner != null && this.gameObject == spawner.currentObject)
        {
            spawner.OnMouseDownForThrow();
        }
    }

    void OnMouseUp()
    {
        var spawner = FindObjectOfType<ARSpawnScript>();
        if (spawner != null && this.gameObject == spawner.currentObject)
        {
            spawner.OnMouseUpForThrow();
        }
    }
}
