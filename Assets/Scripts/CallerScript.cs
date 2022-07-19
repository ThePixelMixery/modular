using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CallerScript : MonoBehaviour
{
    public string GameEvent; 
    public string tag1; 
    public string tag2;
    private GameObject IDInput;

    public void Call()
    {
        QuickAnalyticsManager.logEntry(GameEvent, QuickAnalyticsManager.CaptureDetail.LowRateEvent, tag1); 
    }
    public void CallDetails(string GameEvent, string tag1)
    {
        QuickAnalyticsManager.logEntry(GameEvent, QuickAnalyticsManager.CaptureDetail.LowRateEvent, tag1); 
    }
    
    public void ParticipantID()
    {
        QuickAnalyticsManager.logEntry("Log-In", QuickAnalyticsManager.CaptureDetail.LowRateEvent, "Generic", tag2);
    }
    void Start()
    {
        if (GameObject.Find("InputField_ID") != null)
        {
            IDInput = GameObject.Find("InputField_ID");
            tag2 = IDInput.GetComponent<InputField>().text;
        }
        else{Debug.Log ("I didn't find it~");}
    }
}
