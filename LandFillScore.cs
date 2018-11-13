﻿using UnityEngine;
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
    public string tagC = "Compost T";
    public string tagR = "Recycle T";

    public TipScript tScript;
    public ARSpawnScript ArObject;
    

    public string nameC;
    public string nameR;


    void OnTriggerEnter(Collider trashObject)
    {
        GameObject go = (trashObject.attachedRigidbody) ? trashObject.attachedRigidbody.gameObject : trashObject.gameObject;

        if (go.tag == tagToCompare)
        {
            GameScore.playerScore++;
        }
        if (go.tag == tagR)
        {
            //string nameR = go.ToString();
            tScript.GetTipR();
        }
        if (go.tag == tagC)
        {
            //string nameC = go.ToString();
            tScript.GetTipC();
        }

    }

}
