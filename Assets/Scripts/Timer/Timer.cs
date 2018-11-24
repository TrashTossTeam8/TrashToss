using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    //Controls the amount of time on the timer and the time it starts at
    public float timerDuration = 60f;
    public float currentTime = 60f;

    //Counts the frames as a way to tell time
    public int frameCount = 0;

    //Debug mode boolean that allows the developer to play the game with infinite time for testing
    public bool debugDontFinish;

    //Calls OnTimerUpdate each time it changes
    public Action<float> OnTimerUpdate = delegate(float timer) { };
    public Action OnTimerComplete = delegate { };

    public UnityEvent OnTimeFinished;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoseTime());
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
    }

    //Gets the number of the frame used to count time
    public int getFrameNumber()
    {
        return frameCount;
    }


    //Decrements time every second
    IEnumerator LoseTime()
    {
        bool done = false;
        while (!done)
        {
            if(!enabled)
            {
                yield return null;
            }

            frameCount++;
            if (currentTime < 0f)
            {
                done = true;
            }
            currentTime -= Time.deltaTime;

            OnTimerUpdate(currentTime);

            //Tell the TipScript to display a new tip every ten seconds
            if (timerDuration % 10 == 0)
            {
                //tScript.GetTip();
            }
            //yield return new WaitForSeconds(1);
            
            yield return null;
        }

        if (debugDontFinish == false)
        {
            //Calsl the Lose Time function
            StopCoroutine("LoseTime");
            //countdownText.text = "Times Up!";
            OnTimerComplete();
            OnTimeFinished.Invoke();

            //Sends the user to the leaderboard page
            Application.LoadLevel("Leaderboard");
        }


    }
}