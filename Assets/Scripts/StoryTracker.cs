using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryTracker : MonoBehaviour
{
    public GameObject textObject;

    private string caller = "Lacie: ";
    public string trackingText;

    public TextMeshProUGUI trackerOutput;


    private int score;
    private int count;

    public int stager;
    public bool sameTest;
    public int answer;
    public string reaction;
    public AudioSource Audioclip;
    public AudioSource Clip1;

    public TextMeshProUGUI text;

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
                IncorrectAnswer(accuracy);
                break;
            default:
                break;
        }
    count++;
    Debug.Log(score);
//        Debug.Log(tracker.Count);
    }

    private void IncorrectAnswer(int attempt)
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                reaction = "Luckily, I'm just a test caller! Let's try that again!";
                //set audio to backup
                OutputPrompt(caller, reaction);
                break;
            case 1:
                reaction = "Sorry, I didn't hear you, what did you just say?";
                break;
            case 2:
                reaction = "I have a bad connection, can you say that again?";
                break;
            default:
                break;
        }
        Stager(stager,attempt);        
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
                reaction = "(Here is were you'll find the history of what you've said)";
                OutputPrompt(caller, reaction);
                break;
            case (0,1):
                sameTest = true;
                reaction = "(The phone is ringing. What do you pick up and say?)";
                OutputPrompt(caller, reaction);
                Debug.Log(Clip1.name);
                Audioclip = Clip1;
                break;
            case (1,-1):
                break;
            case (1,0):
                reaction = "I'm having trouble with my internet speed";
//                Audioclip = Clip2;
                break;
            case (1,1):
                reaction = "Oh, thank you! I'm having trouble with my internet";
//                Audioclip = Clip2;
                break;
            case (2,-1):
//                reaction =
//                    "My name is Lacie Green and my number is 04 6281 1611";
//                Audioclip = Clip3;
                break;
            case (2,0):
                reaction =
                    "Sure, my name and number is Lacie Green, 04 6281 1611";
//                Audioclip = Clip3;
                break;
            case (2,1):
                reaction =
                    "I appreciate that and of course, my name is Lacie Green, and my number is 04 6281 1611";
//                Audioclip = Clip3;
                break;
            case (3,-1):
//                reaction = "When can I expect a solution?";
//                Audioclip = Clip4;
    break;
            case (3,0):
                reaction = "No, Green. Never mind";
//                Audioclip = Clip4;
break;
            case (3,1):
                reaction = "That's right! When can I expect a solution?";
//                Audioclip = Clip4;
          break;
            case (4,-1):
//                reaction = "That's right! When can I expect a solution?";
                break;
            case (4,0):
                reaction = "Okay, I'll keep an ear out";
                break;
            case (4,1):
                reaction = "Thank you for your help!";
                sameTest = false;
                break;
            default:
                break;
        }
        text.text=reaction;

        textObject.GetComponentInChildren<TextScript>().ToPlay = Audioclip;
        textObject.GetComponentInChildren<TextScript>().SameTest = sameTest;
        textObject.GetComponentInChildren<TextScript>().Stager();  
    }
}
