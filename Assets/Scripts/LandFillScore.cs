using UnityEngine;
using System.Collections;

/*
This class is used to trigger the correct sorting of a piece of trash.
When the user correctly sorts trash this class will update the player score
after verifying the user did infact sort trash correctly.
*/

public class LandFillScore : MonoBehaviour {

    /*
    This method is used when the object that this script is
    attatched to triggers another object. This method collects the data
    of the triggered object and saves it into a variable called "trashObject".
    using this variable we can check whether or not the user correctly sorted
    trash, if so call method from GameScore and update the score.
    */
    public string tagToCompare = "Land Fill T";

    //Sound effect that plays when the user scores a point
    public AudioClip successClip;

    //Called on load
    private void Start()
    {
        //Prevents the clip from playing automatically on load
        GetComponent<AudioSource>().playOnAwake = false;
        //Assigns the clip to the AudioSource object attached to the waste bin
        GetComponent<AudioSource>().clip = successClip;
    }

    //Called when a waste object enters the collider within a waste bin
    void OnTriggerEnter(Collider trashObject)
    {
        GameObject go = (trashObject.attachedRigidbody) ? trashObject.attachedRigidbody.gameObject : trashObject.gameObject;

        //If the object has been sorted correctly
        if (go.tag == tagToCompare)
        {
            //Increase the score
            GameScore.playerScore++;
            //Play the success sound
            GetComponent<AudioSource>().Play();
        }

    }
}