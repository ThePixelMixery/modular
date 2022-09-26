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

    public bool running = false;

    private int score;
    private int count;

    public int stager;
    public bool sameTest;
    public int answer;
    bool retry = false;
    public string lastAnswer = "(The phone is ringing. What do you pick up and say?)";
    public string lastLine;
    public string reaction;
    public AudioSource Audioclip;
    public AudioSource Clip1;

    public TextMeshProUGUI text;

    public void OutputPrompt(string caller, string answer)
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

    public void OutputAnswer(string words, int accuracy)
    {
        Debug.Log("Output words have " + accuracy);
        trackingText += "You: "+words+"\n";
        trackerOutput.text = trackingText;
        if (accuracy > -1){
                answer = accuracy;
                lastAnswer = reaction;
                retry = false;
                Debug.Log("Was " + stager);
                stager++;
                Debug.Log("Restaged at "+ stager+", accuracy:" +accuracy);
                
        }
        else
        {Debug.Log("Incorrect answer?");
        reaction = "Luckily, I'm just a test caller! Let's try that again!";
        OutputPrompt(caller, reaction);
        retry = true;
        text.text = reaction; 
        Debug.Log("Text.text changed");
                        Debug.Log("Restaged at "+ stager+", accuracy:" +accuracy);
}

        count++;
        Debug.Log(score);
        score += accuracy;
        textObject.GetComponentInChildren<TextScript>().Stage = stager;
    }

 //   private void IncorrectAnswer()
//    {
//    Debug.Log("Incoorect");
 //       int rand = Random.Range(0, 3);
   //     switch (rand)
 //       {
 //           case 0:
 //               reaction = "Luckily, I'm just a test caller! Let's try that again!";
                //set audio to backup
            //     break;
            // case 1:
            //     reaction = "Sorry, I didn't hear you, what did you just say?";
            //     break;
            // case 2:
            //     reaction = "I have a bad connection, can you say that again?";
            //     break;
            // default:
            //     break;
 //       }
//        OutputPrompt(caller, reaction);
//        retry = true;
//        text.text = reaction; 
//    }

    public void StoryStarter(){
    if (running == true){StoryStager (0,1);}
    }

    public void StoryButton(){
    StoryStager(stager, answer);
    }

    public void StoryStager(int stage,int answer)
    {  
    Debug.Log("Reaction is cunrrently: "+ reaction);
    if (retry == true){reaction = lastAnswer;}
    else{
        switch (stager,answer)
        {
            case (0,0):
                reaction = "(Here is were you'll find the history of what you've said)";
                break;
            case (0,1):
                sameTest = true;
                reaction = "(The phone is ringing. What do you pick up and say?)";
                Audioclip = Clip1;
                Debug.Log(Clip1.name);
                break;
            case (1,0):
                reaction = "I'm having trouble with my internet speed";
//                Audioclip = Clip2;
                break;
            case (1,1):
                reaction = "Oh, thank you! I'm having trouble with my internet";
//                Audioclip = Clip2;
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
            case (3,0):
                reaction = "No, Green. Never mind";
//                Audioclip = Clip4;
                break;
            case (3,1):
                reaction = "That's right! When can I expect a solution?";
//                Audioclip = Clip4;
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
        }
        OutputPrompt(caller, reaction);
        text.text = reaction;

        textObject.GetComponentInChildren<TextScript>().ToPlay = Audioclip;
        textObject.GetComponentInChildren<TextScript>().SameTest = sameTest;
    }
}
