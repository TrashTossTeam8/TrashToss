using UnityEngine;
using System.Collections;

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
    public string tagL = "Land Fill T";
    public string tagR = "Recycle T ";
    public TipScript tScript;
    //public string nameL;
    //public string nameR;

    void OnTriggerEnter(Collider trashObject)
	{
        GameObject go = (trashObject.attachedRigidbody) ? trashObject.attachedRigidbody.gameObject : trashObject.gameObject;

		if (go.tag == tagToCompare) 
		{
			GameScore.playerScore++;
		}
        if (go.tag == tagL)
        {
            //string namel = go.ToString();
            //get and display the tip for soring the trash wrong to the LandFill Trash
            tScript.GetTipL();
        }
        if (go.tag == tagR)
        {
            //string nameR = go.ToString();
            //get and display the tip for soring the trash wrong to the Recycle Trash
            tScript.GetTipR();
        }

    }
}