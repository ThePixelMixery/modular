using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallerScript : MonoBehaviour
{
    public string GameEvent; 
    public string tag1; 
    
    public void Call()
    {
        QuickAnalyticsManager.logEntry(GameEvent, QuickAnalyticsManager.CaptureDetail.LowRateEvent, tag1); 
    }
    public void CallDetails(string GameEvent, string tag1)
    {
        QuickAnalyticsManager.logEntry(GameEvent, QuickAnalyticsManager.CaptureDetail.LowRateEvent, tag1); 
    }
    /*
    public void ParticipantID()
    {
        GameObject IDInput = GameObject.Find("InputField_participantID");
        tag2 = IDInput.GetComponent<InputField>().text;
        GameEvent = "ParticipantID";
        tag1 = "Generic";
        Call();
    }
    */
}
