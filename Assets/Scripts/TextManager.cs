using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//tick and cross created using loading.io

public class responseClass
{
    string response;
    int accuracy;
    public responseClass(string Response, int Accuracy)
    {
        response = Response;
        accuracy = Accuracy;
    }

}

    

public class TextManager : MonoBehaviour
{
    
    public TextMeshProUGUI Situation;
    public TextMeshProUGUI Option1;
    public TextMeshProUGUI Option2;
    public TextMeshProUGUI Option3;
    public TextMeshProUGUI TimerText;

    public Image Correct1;
    public Image Correct2;
    public Image Correct3;
    public Image Incorrect1;
    public Image Incorrect2;
    public Image Incorrect3;

    public Canvas TextCan;
    public Canvas AudioCan;
    public Canvas TimerCan;

    public Button Advance;

    public AudioSource Clip1;
    public AudioSource Clip2;
    public AudioSource Clip3;
    public AudioSource Clip4;

    private AudioSource ToPlay;

    public int Path;
    public int Test;
    public int Stage;
    private int Correct;
    private float TimeLeft=10.0f;

    public responseClass[] responseArray;


    
    void RunTimer()
    {
        TimeLeft = 10.0f;
        while (TimeLeft >= 0.0f)
        {
        TimeLeft -= Time.deltaTime;
        TimerText.text = TimeLeft.ToString("#.00");        
        }
        AnswerOutput(Correct);

    }

    public void Pathchanger(int newPath)
    {
        Path = newPath;
        PracProgressor(Path,Stage);
    }

    public void StageAdvancer()
    {
        Stage++;
        Correct1.gameObject.SetActive(false);
        Correct2.gameObject.SetActive(false);
        Correct3.gameObject.SetActive(false);
        Incorrect1.gameObject.SetActive(false);
        Incorrect2.gameObject.SetActive(false);
        Incorrect3.gameObject.SetActive(false);
        Advance.interactable = false;
        TimeLeft = 10.0f;


        PracProgressor(Path,Stage);
    }

    public void AnswerOutput(int Answer)
    {
        Advance.interactable = true;
        switch (Correct)
        {
            case(0):
            Correct1.gameObject.SetActive(true);
            Incorrect2.gameObject.SetActive(true);
            Incorrect3.gameObject.SetActive(true);
            break;
            case(1):
            Incorrect1.gameObject.SetActive(true);
            Correct2.gameObject.SetActive(true);
            Incorrect3.gameObject.SetActive(true);
            break;
            case(2):
            Incorrect1.gameObject.SetActive(true);
            Incorrect2.gameObject.SetActive(true);
            Correct3.gameObject.SetActive(true);
            break;

            default:
            Incorrect1.gameObject.SetActive(true);
            Incorrect2.gameObject.SetActive(true);
            Incorrect3.gameObject.SetActive(true);
            break;
        }

        if (Correct == Answer)
        {
            if (Path >= 4 || Path <= 6)
            {
                //Point based
            }
            else if (Path >= 7)
            {
                // Narrative Changes
            }
        }
        else
        {
        
        }
        

    }

    public void PlayAudio()
    {
        ToPlay.Play();
    }

    public void PracProgressor (int Path, int Test)
    {
        switch (Path, Test)
        {
            case(1,0):
            Situation.text = "*Phone Rings*";
            Option1.text = "Hi. What do you want?";
            Option2.text = "Hello, what can I do for you today?";
            Option3.text = "Hello, welcome to help desk. My name is X. How can I help you today?";

            break;

            case(1,1):
            Correct = 2;
            Situation.text = "I' can't get my mouse and keyboard to connect to my computer";
            Option1.text = "I understand your frustration Can I have your name and number?";
            Option2.text = "Can I grab your name and number?";
            Option3.text = "Name and number, please.";
            break;

            case(1,2):
            Correct = 1;
            Situation.text = "My name is Lacie Green and my number is 04 6281 1611";
            Option1.text = "Lacie Green, 0462811611";
            Option2.text = "Lacie Bean, 0462811611?";
            Option3.text = "Lacie Green 0462711611?";
            break;

            case(1,3):
            Correct = 3;
            Situation.text = "When can I expect a solution?";
            Option1.text = "My colleague will call you back shortly, thank you for using our service";
            Option2.text = "We'll call you back sometime today";
            Option3.text = "We'll call you back when we can, bye";
            break;

            case(1,4):
            TextCan.gameObject.SetActive(false);
            AudioCan.gameObject.SetActive(true);
            ToPlay = Clip1;
            Correct = 1;
            Option1.text = "Hello, welcome to help desk. My name is X. How can I help you today?";
            Option2.text = "Hello, what can I do for you today?";
            Option3.text = "Hi. What do you want?";
            break;

            case(1,5):
            ToPlay = Clip2;
            Correct = 2;
            Option1.text = "I understand your frustration Can I have your name and number?";
            Option2.text = "Can I grab your name and number?";
            Option3.text = "Name and number, please.";
            break;

            case(1,6):
            ToPlay = Clip3;
            Correct = 1;
            Option1.text = "Lacie Green, 0462811611";
            Option2.text = "Lacie Bean, 0462811611?";
            Option3.text = "Lacie Green 0462711611?";
            break;

            case(1,7):
            ToPlay = Clip4;
            Correct = 3;
            Option1.text = "My colleague will call you back shortly, thank you for using our service";
            Option2.text = "We'll call you back sometime today";
            Option3.text = "We'll call you back when we can, bye";
            break;

            case(1,8):
            TimerCan.gameObject.SetActive(true);
            RunTimer();
            ToPlay = Clip1;
            Correct = 1;
            Option1.text = "Hello, welcome to help desk. My name is X. How can I help you today?";
            Option2.text = "Hello, what can I do for you today?";
            Option3.text = "Hi. What do you want?";
            break;

            case(1,9):
            RunTimer();
            ToPlay = Clip2;
            Correct = 2;
            Option1.text = "I understand your frustration Can I have your name and number?";
            Option2.text = "Can I grab your name and number?";
            Option3.text = "Name and number, please.";
            break;

            case(1,10):
            RunTimer();
            ToPlay = Clip3;
            Correct = 1;
            Option1.text = "Lacie Green, 0462811611";
            Option2.text = "Lacie Bean, 0462811611?";
            Option3.text = "Lacie Green 0462711611?";
            break;

            case(1,11):
            RunTimer();
            ToPlay = Clip4;
            Correct = 3;
            Option1.text = "My colleague will call you back shortly, thank you for using our service";
            Option2.text = "We'll call you back sometime today";
            Option3.text = "We'll call you back when we can, bye";
            break;


            default: 
            Situation.text = "Oops, something is wrong. Contact the researcher right away!";
            break;


        }
        
    }

   

    void Start() 
    {

        responseClass[] responses = new responseClass[3];
        responses[0] = new responseClass("default 1", 0);
        responses[1] = new responseClass("default 2", 1);
        responses[2] = new responseClass("default 3", 2);
        
    
    }

/*    
    public void AnswerRandomised (responseClass[] responses)
    {
            int rnd = Random.Range(0,i);
            int temp = responses[i];
        foreach (responseClass[] i in responses )
        {
            responses[i] = responses[rnd];
            responses[rnd] = temp;
        }
        Option1.text = responses[0].response;
        Option2.text = responses[1].response;
        Option3.text = responses[2].response;
    }

    public void ButtonUpdater(int Path, int Stage, responseClass[] responses)
    {
        switch(Path, Stage)
        {
            case(1,0):
            responses[0].response = "Hi. What do you want?";
            responses[1].response = "Hello, what can I do for you today?";
            responses[2].response = "Hello, welcome to help desk. My name is X. How can I help you today?";
            
            AnswerRandomised(responses);
            break;

            default:
            break;
        }
    }
*/

}



