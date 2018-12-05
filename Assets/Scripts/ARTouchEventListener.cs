using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class makes it possible for the user to interact with the waste objects.

public class ARTouchEventListener : MonoBehaviour
{
    //When user presses finger down
    void OnMouseDown()
    {
        //references the script that governs physics
        var spawner = FindObjectOfType<SpawnAndThrowScript>();
        if (spawner != null && this.gameObject == spawner.currentObject)
        {
            spawner.OnMouseDownForThrow();
        }
    }

    //When user releases finger
    void OnMouseUp()
    {
        //references the script that governs physics
        var spawner = FindObjectOfType<SpawnAndThrowScript>();
        if (spawner != null && this.gameObject == spawner.currentObject)
        {
            spawner.OnMouseUpForThrow();
        }
    }
}
