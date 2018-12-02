﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    //Text that displays the waste tip if waste object was sorted incorrectly
    public static TipScript tipScript;
    
    //Text variable that holds the tip
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
            if(go.tag.Contains("Compost"))
            {
                tip.text = "Food waste can go into the COMPOST bin in the Residential Dining Halls, University Student Union, and a limited number of other locations on campus. Ask in other areas on campus.";
            }
            else
            {
                //Gets the name of the object
                string objectName = go.name;

                //Displays tip specific to the item if sorted into the wrong bin.
                switch (objectName)
                {
                    case "Water Bottle PREFAB(Clone)":
                        tip.text = "Plastic water bottles are not biodegradable nor do they belong in landfills. After emptying contents, these items should be discarded in the MIXED RECYCLING bin. Switch to a reusable water bottle to reduce your waste!";
                        break;
                    case "SoupCan_PREFAB(Clone)":
                        tip.text = "Metal containers are recyclable. Just make sure they are washed first if they were in contact with food.";
                        break;
                    case "PlasticSpoon_PREFAB(Clone)":
                        tip.text = "Single-use plastic utensils cannot be composted because they are made out of inorganic materials like plastic. And don't be fooled: utensils made from \"bio - plastics\" are typically not compostable either. Bring your own reusable utensils to help reduce your waste!";
                        break;
                    case "Notepad_PREFAB(Clone)":
                        tip.text = "Paper should be recycled.";
                        break;
                    case "coca_can(Clone)":
                        tip.text = "Metal cannot be composted because it is made out of inorganic materials nor should they go in the landfill bin.These items should be discarded in the RECYCLING bin. ";
                        break;
                    case "Cardboardbox_PREFAB(Clone)":
                        tip.text = "Cardboard is recyclable.";
                        break;
                }
            }

            //Destroy waste object being thrown
            Destroy(go);
        }

    }
}