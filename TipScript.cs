using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipScript : MonoBehaviour
{

    //Text variable that holds the tip
    public Text tip;

    public LandFillScore land;
    public RecycleScore recycle;
    public CompostScore compost;
    public ARSpawnScript currentObject;

    //Text that is currently displayed
    String currentTip;

    //Array that holds all possible tips
    //String[] tips = { "Remember! Plastic is recyclable and goes in the Recycle (blue) bin.", "Remember! Metal is recyclable and goes in the Recycle (blue) bin.", "Organic material goes in the compost bin.", "Chip bags cannot be recycled and go in the landfill waste (Unfortunately)", "If it's not organic, plastic, paper, or metal, it's probably landfill waste.", "Remember to dump out liquids before putting cups in bins." };

    // Use this for initialization
    void Start()
    {

        //Default blank tip value
        tip.text = "";
        currentTip = "";
    }

    //public void GetTip()
    //{
        //Generate an rng variable to randomly pick a tip
        //System.Random r = new System.Random();


        //While loop makes sure the same tip doesn't get selected twice.
        //while (tip.text == currentTip)
        //{
            //Randomly select between the six tips
            //int selectTip = r.Next(6);

            //Set the text to be the tip
            //tip.text = "Tip: " + "Wrong ones, Nice Try";
        //}

        //currentTip = tip.text;

    //}

    public void GetTipL()
    {

        tip.text = "Tip: Wrong, but nice try. This object belong to Landfill trash can";
        //currentTip = tip.text;
    }

    public void GetTipR()
    {
        tip.text = "Tip: Wrong, but nice try. This object belong to Recyle trash can";
        //currentTip = tip.text;
    }

    public void GetTipC()
    {
        tip.text = "Tip: Wrong, but nice try. This object belong to Compost trash can";
        //currentTip = tip.text;
    }
}
