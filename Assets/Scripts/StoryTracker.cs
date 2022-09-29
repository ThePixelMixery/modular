using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryTracker : MonoBehaviour
{
    private static string caller;
    public static string trackingText;

    private static TextMeshProUGUI trackerOutput;

    public static int score;
    private static int count;

    public static  string lastLine;

    public static void Starter()
    {
        trackerOutput = GameObject.FindWithTag("Tracker").GetComponent<TextMeshProUGUI>();
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
        LogScript.WriteNewLogEntry("New Prompt", "Narrative", output);
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
        LogScript.WriteNewLogEntry("Average", words, ((score/count)*100)+"%");
        
    }
}
