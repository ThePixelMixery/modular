using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI TrainTimerText;
 
    private float timeLeft = 0.0f;
    private bool training = true;

    // Update is called once per frame
    public void SetTimer()
    {
        if(training==false){
        timeLeft = 10.0f;
        }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            timerEnded();
        }
    }
     void timerEnded()
    {
        if(training==false){
        TextManager.AnswerOutput(3);
        LogScript.WriteNewLogEntry("Answer", "TimerFail", "PlayerFeedback");
        }

    }
 
}

/*
IEnumerator RunTimer(float Cliplength)
    {
        yield return new WaitForSeconds(Cliplength);
        TimeLeft = 10.0f;
        while (TimeLeft >= 0.0f)
        {
        TimeLeft -= Time.deltaTime;
        TimerText.text = TimeLeft.ToString("#:00");        
        }
        AnswerOutput(3);
        LogScript.WriteNewLogEntry("Answer","TimerFail","PlayerFeedback"); 
    }
    
    private void RunTrainingTimer()
    {
        TimeLeft = 60.0f;
        while (TimeLeft >= 0.0f)
        {
        TimeLeft -= Time.deltaTime;
        TrainTimerText.text = TimeLeft.ToString("#:00");        
        }
        AnswerOutput(3);
        LogScript.WriteNewLogEntry("Training","TimerRanOut","PlayerFeedback"); 
    }
*/