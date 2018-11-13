using UnityEngine;
using System.Collections;

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
    public string tagC = "Compost T";
    public string tagL = "Land Fill T";
    public TipScript tScript;
    

    void OnTriggerEnter(Collider trashObject)
    {
        GameObject go = (trashObject.attachedRigidbody) ? trashObject.attachedRigidbody.gameObject : trashObject.gameObject;

        if (go.tag == tagToCompare)
        {
            GameScore.playerScore++;
        }
        if (go.tag == tagL)
        {
            tScript.GetTipL();
        }
        if (go.tag == tagC)
        {
            tScript.GetTipC();
        }

    }

}
