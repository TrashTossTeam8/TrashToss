using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
This class is used to trigger the correct sorting of a piece of trash.
When the user correctly sorts trash this class will update the player score
after verifying the user did infact sort trash correctly.
*/

public class RecycleScore : MonoBehaviour {

    /* 
    This method is used when the object that this script is
    attatched to triggers another object. This method collects the data
    of the triggered object and saves it into a variable called "trashObject".
    using this variable we can check whether or not the user correctly sorted
    trash, if so call method from GameScore and update the score.
    */
    public string tagToCompare = "Recycle T";

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
            else
            {
                //Gets the name of the object
                string objectName = go.name;

                //Displays tip specific to the item if sorted into the wrong bin.
                switch (objectName)
                {
                    case "Straw_PREFAB(Clone)":
                        tip.text = "Straws cannot be composted because they are made out of inorganic materials like plastic. They also can't be recycled because they can damage recycling equipment.";
                        break;
                    case "Pizza_box_closed_PREFAB(Clone)":
                        tip.text = "Soiled paper products cannot be processed in Composting or Recycling centers.";
                        break;
                    case "Pencil_PREFAB(Clone)":
                        tip.text = "Pencils are not recyclable or compostable.";
                        break;
                    case "Pen_PREFAB(Clone)":
                        tip.text = "Pens are not recyclable or compostable.";
                        break;
                    case "Ketchup_PREFAB(Clone)":
                        tip.text = "Condiment packets cannot be composted because they are made out of inorganic materials like plastic. These items should be discarded in the LANDFILL bin. Refuse these items when ordering take-out food or take only as many packets as you actually need to help reduce waste.";
                        break;
                    case "Foil_PREFAB(Clone)":
                        tip.text = "Foil cannot be composted because it is not biodegradable. It also should not go in the landfill bin. These items should be discarded in the RECYCLING bin. ";
                        break;
                    case "Chip_Bag(Clone)":
                        tip.text = "Chip bags cannot be composted or recycled because they are made out of inorganic materials like plastic and aluminum. These items should be discarded in the LANDFILL bin.";
                        break;
                    case "Yogurt_PREFAB(Clone)":
                        tip.text = "Single-use plastic containers cannot be composted because they are made out of inorganic materials.These items should be discarded in the LANDFILL bin. ";
                        break;
                }
            }
            //Destroy waste object being thrown
            Destroy(go);
        }

    }
}