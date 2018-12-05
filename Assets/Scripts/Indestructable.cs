using UnityEngine;
using System.Collections;


//This script is an object that keeps track of what scene the user is currently on.
//This is important for the leaderboard because it needs to know whether or not it should
//allow the user to add their name to the leaderboard. The leaderboard does this by checking
//the object made from this class to see where the user came from.
public class Indestructable : MonoBehaviour
{

    public static Indestructable instance = null;

    // or sake of example, assume -1 indicates first scene
    public int prevScene = -1;

    void Awake()
    {
        //If we don't have an instance set - set it now
        if (!instance)
            instance = this;
        //Otherwise, its a double, we dont need it - destroy
        else
        {
            Destroy(this.gameObject);
            return;
        }

        //Makes the object permanent so that we can always check to see what scene the user came from previously even though we changed the scene.
        DontDestroyOnLoad(this.gameObject);
    }
}