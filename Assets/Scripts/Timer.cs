using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int timeLeft = 60;
    public int frameCount = 0;
    public Text countdownText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        countdownText.text = ("Time Left = " + timeLeft);

        if (timeLeft <= 0)
        {
            //Calsl the Lose Time function
            StopCoroutine("LoseTime");
            countdownText.text = "Times Up!";

            //Sends the user to the leaderboard page
            Application.LoadLevel("Leaderboard");
        }
    }

    //Gets the number of the frame used to count time
    public int getFrameNumber()
    {
        return frameCount;
    }

    //Decrements time every second
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}