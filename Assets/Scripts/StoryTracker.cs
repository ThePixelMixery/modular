using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryTracker : MonoBehaviour
{
    private static string caller;
    public static string trackingText;

    private static TMP_Text trackerOutput;

    private static int score;
    private static int count;

    public static  string lastLine;

    void Start()
    {
        trackerOutput = GameObject.Find("Content_Story").GetComponent<TMP_Text>();
        Debug.Log("Found " + trackerOutput);
        
    }

    public static void OutputPrompt(string caller, string answer)
    {
        string output = caller+answer;
        if (output != lastLine && answer != "")
        {
        Debug.Log("Last line is not considered the same");
        trackingText += output+"\n";
        trackerOutput.text = trackingText;
        lastLine = output;
        }
    }

    public static  void OutputAnswer(string words, int accuracy)
    {
        Debug.Log("Output words have " + accuracy);
        trackingText += "You: "+words+"\n";
        trackerOutput.text = trackingText;
        count++;
        Debug.Log(score);
        score += accuracy;
    }
}
