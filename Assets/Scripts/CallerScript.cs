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
    private TMP_InputField IDInput;

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
        tag2 = IDInput.text;
        QuickAnalyticsManager.logEntry("Log-In", QuickAnalyticsManager.CaptureDetail.LowRateEvent, "Generic", tag2);
    }
    void Start()
    {
        IDInput = gameObject.GetComponent<TMP_InputField>();
        Debug.Log(IDInput.name + "Found");
    }
}
