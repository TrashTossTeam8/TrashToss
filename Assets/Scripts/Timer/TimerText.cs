using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public Timer timer;
    public Text textElement;

	void OnEnable ()
    {
		if(timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
        if(timer != null)
        {
            timer.OnTimerUpdate += TimerUpdate;
            timer.OnTimerComplete += TimerComplete;
        }
	}

    private void OnDisable()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
        if (timer != null)
        {
            timer.OnTimerUpdate -= TimerUpdate;
            timer.OnTimerComplete -= TimerComplete;
        }
    }

    void TimerUpdate (float time)
    {
        textElement.text = String.Format("{0:00}", time);
    }

    void TimerComplete()
    {
        textElement.text = "Times Up!";
    }
}
