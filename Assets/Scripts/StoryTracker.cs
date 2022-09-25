using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryTracker : MonoBehaviour
{
    private class reply
    {
        string speaker;

        string output;

        public reply(string Speaker, string Output)
        {
            this.speaker = Speaker;
            this.output = Output;
        }
    }

    public GameObject textObject;

    private string caller = "Lacie: ";
    public string trackingText;

    public TextMeshProUGUI text;
    public TextMeshProUGUI trackerOutput;

    private List<reply> tracker = new List<reply>();

    private int score;
    private int count;

    public int stager;
    public int answer;
    public string reaction;
    public void OutputPrompt(string caller, string answer)
    {
//        tracker.Add(new reply(caller, answer));
        trackingText += caller+answer+"\n";
        trackerOutput.text = trackingText;
        Debug.Log(caller+answer);
//        Debug.Log(tracker.Count);
    }

    public void OutputAnswer(string answer, int accuracy)
    {
//        tracker.Add(new reply("You: ", answer));
        trackingText += "You: "+answer+"\n";
        trackerOutput.text = trackingText;
        score += accuracy;
        switch (accuracy)
        {
            case 1:
                Stager(stager+1,accuracy);
                break;
            case 0:
                Stager(stager+1, accuracy);
                break;
            case -1:
                Stager(stager, accuracy);
//                tracker.Add(new reply(caller, IncorrectAnswer()));
                break;
            default:
                break;
        }
    count++;
    Debug.Log(score);
//        Debug.Log(tracker.Count);
    }

    private string IncorrectAnswer()
    {
        string windfall = "Wrong answer";
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                windfall =
                    "Luckily, I'm just a test caller! Let's try that again!";
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
        return windfall;
        
    }

    // private void TrackerUpdate(){ 
    // string output;
    // foreach(reply line in tracker){
    // String.Concat(line.speaker, line.output);
    // }
    // //trackerOutput.text = 
    // }

    public void StoryStarter(){
    Stager (0,1);
    }

    public void Stager(int stage,int answer)
    {
        switch (stager,answer)
        {
            case (0,0):
//                SameTest = true;
                reaction = "(Here is were you'll find the history of what you've said)";
                OutputPrompt(caller, reaction);
//                ToPlay = Clip1;
//                Debug.Log(Clip1.name);
//                Cliplength = Clip1.clip.length;
                break;
            case (0,1):
//                SameTest = true;
                reaction = "(The phone is ringing. What do you pick up and say?)";
                OutputPrompt(caller, reaction);
//                ToPlay = Clip1;
//                Debug.Log(Clip1.name);
//                Cliplength = Clip1.clip.length;
                break;
            case (1,-1):
                reaction = "I'm having trouble with my internet speed";
//                ToPlay = Clip2;
//                Cliplength = Clip2.clip.length;
                break;
            case (1,0):
//                reaction = "I'm having trouble with my internet speed";
//                ToPlay = Clip2;
//                Cliplength = Clip2.clip.length;
                break;
            case (1,1):
//                reaction = "I'm having trouble with my internet speed";
//                ToPlay = Clip2;
//                Cliplength = Clip2.clip.length;
                break;
            case (2,-1):
//                reaction =
//                    "My name is Lacie Green and my number is 04 6281 1611";
//                ToPlay = Clip3;
//                Cliplength = Clip3.clip.length;
                break;
            case (2,0):
//                reaction =
//                    "My name is Lacie Green and my number is 04 6281 1611";
//                ToPlay = Clip3;
//                Cliplength = Clip3.clip.length;
                break;
            case (2,1):
//                reaction =
//                    "My name is Lacie Green and my number is 04 6281 1611";
//                ToPlay = Clip3;
//                Cliplength = Clip3.clip.length;
                break;
            case (3,-1):
//                reaction = "When can I expect a solution?";
//                ToPlay = Clip4;
//                Cliplength = Clip4.clip.length;
//                SameTest = false;
            case (3,0):
//                reaction = "When can I expect a solution?";
//                ToPlay = Clip4;
//                Cliplength = Clip4.clip.length;
//                SameTest = false;
            case (3,1):
//                reaction = "When can I expect a solution?";
//                ToPlay = Clip4;
//                Cliplength = Clip4.clip.length;
//                SameTest = false;
                break;
            default:
                break;
        }
        text.text=reaction;
    }
}
