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
            //If time runs out
            if(!enabled)
            {
                yield return null;
            }

            //Counter
            frameCount++;
            if (currentTime < 0f)
            {
                done = true;
            }
            currentTime -= Time.deltaTime;

            OnTimerUpdate(currentTime);
            
            
            yield return null;
        }

        if (debugDontFinish == false)
        {
            //Calsl the Lose Time function
            StopCoroutine("LoseTime");
            OnTimerComplete();
            OnTimeFinished.Invoke();

            //Makes note of the scene that is loading before we go to the leaderboard. This is important to be sure that users only see certain
            //UI elements if they came from the game and not the main menu.
            Indestructable.instance.prevScene = Application.loadedLevel;
            //Sends the user to the leaderboard page
            Application.LoadLevel("Leaderboard");
        }


    }
}