using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
This class is used to trigger the correct sorting of a piece of trash.
When the user correctly sorts trash this class will update the player score
after verifying the user did infact sort trash correctly
*/

public class CompostScore : MonoBehaviour {

    /* 
    This method is used when the object that this script is
    attatched to triggers another object. This method collects the data
    of the triggered object and saves it into a variable called "trashObject".
    using this variable we can check whether or not the user correctly sorted
    trash, if so call method from GameScore and update the score.
    */
    public string tagToCompare = "Compost T";

    //Sound effect that plays when the user scores a point
    public AudioClip successClip;

    public Text tip;

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
        //go is the current GameObject being thrown
        GameObject go = (trashObject.attachedRigidbody) ? trashObject.attachedRigidbody.gameObject : trashObject.gameObject;

        //If the object has been sorted correctly
        if (go.tag == tagToCompare)
        {
            //Increase the score
            GameScore.playerScore++;
            //Play the success sound
            GetComponent<AudioSource>().Play();
            tip.text = "";
            Destroy(go);
        }
        else
        {
            //If the object is a compost item
            if (go.tag.Contains("Compost"))
            {
                tip.text = "Food waste can go into the COMPOST bin in the Residential Dining Halls, University Student Union, and a limited number of other locations on campus. Ask in other areas on campus.";
            }
            //If the object is not a compost item
            else
            {
                tip.text = "Only food can potentially go in the compost bin at CSULB. No food containers can go in the compost bin.";
            }
            //Destroy waste object being thrown
            Destroy(go);
        }

    }
}