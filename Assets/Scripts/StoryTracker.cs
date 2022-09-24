using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryTracker : MonoBehaviour
{
    private class reply
    {
        private string speaker;
        private string output;
        public reply(string Speaker, string Output)
        {
            this.speaker = Speaker;
            this.output = Output;
        }
    }
    public GameObject textObject;
    private string caller = "Lacie";
    public TextMeshProUGUI text;
    private List<reply> tracker = new List<reply>();
    private int score;

    public void OutputPrompt(string caller, string answer)
    {
        tracker.Add(new reply(caller, answer));
        Debug.Log(tracker.Count);
    }

    public void OutputAnswer(string answer, int accuracy)
    {
        tracker.Add(new reply("You: ", answer));
        score += accuracy;
        switch (accuracy)
        {
            case 1:
                break;
            case 0:
                break;
            case -1:
                tracker.Add(new reply(caller, IncorrectAnswer()));
                break;
            default:
                break;
        }

        Debug.Log(tracker.Count);
    }
    private string IncorrectAnswer()
    {
        string windfall = "Wrong answer";
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                windfall = "Luckily, I'm just a test caller! Let's try that again!";
                break;
            case 1:
                windfall = "Sorry, I didn't hear you, what did you just say?";
                break;
            case 2:
                windfall = "I have a bad connection, can you say that again?";
                break;
            default:
                break;
        }
        textObject.GetComponent<TextScript>().Stage--;
        return windfall;

    }
}
