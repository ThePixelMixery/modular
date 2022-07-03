using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//tick and cross created using loading.io

public class responseClass
{
    public string response;
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
        PracProgressor(Path,Test,Stage);
    }

    public void Resetter()
    {
        Correct1.gameObject.SetActive(false);
        Correct2.gameObject.SetActive(false);
        Correct3.gameObject.SetActive(false);
        Incorrect1.gameObject.SetActive(false);
        Incorrect2.gameObject.SetActive(false);
        Incorrect3.gameObject.SetActive(false);
        Advance.interactable = false;
        TimeLeft = 10.0f;


        PracProgressor(Path,Test,Stage);
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

    public void PracProgressor (int Path, int Test, int Stage)
    {
        switch (Path)
        {
            case(1):
            switch  (Test)
            {
                case(0):
                switch (Stage)
                    {
                    case 0:
                    Situation.text = "*Phone Rings*";
                    break;

                    case(1):
                    Situation.text = "I'm having trouble with my internet speed";
                    Stage++;
                    break;

                    case(2):
                    Situation.text = "My name is Lacie Green and my number is 04 6281 1611";
                    break;

                    case(3):
                    Situation.text = "When can I expect a solution?";
                    break;

                    default:
                    break;
                }
                break;
                
                case(1);
                TextCan.gameObject.SetActive(false);
                AudioCan.gameObject.SetActive(true);
                switch (Stage)
                    {
                    case(0):
                    ToPlay = Clip1;
                    break;

                    case(1):
                    ToPlay = Clip2;
                    break;

                    case(2):
                    ToPlay = Clip3;
                    break;

                    case(3):
                    ToPlay = Clip4;
                    break;

                    default:
                    break;
                }                                
                break;

                case(2);
                AudioCan.gameObject.SetActive(false);
                TimerCan.gameObject.SetActive(true);

                switch (Stage)
                    {
                    case(0):
                    ToPlay = Clip1;
                    break;

                    case(1):
                    ToPlay = Clip2;
                    break;

                    case(2):
                    ToPlay = Clip3;
                    break;

                    case(3):
                    ToPlay = Clip4;
                    break;

                    default:
                    break;
                }
                RunTimer();                            
                break;

                default:
                break;
            }
            default: 
            Situation.text = "Oops, something is wrong. Contact the researcher right away!";
            break;
        }
        ButtonUpdater(Path, Stage);
    }

   

    void Start() 
    {

        responseClass[] responses = new responseClass[3];
        responses[0] = new responseClass("default 1", 0);
        responses[1] = new responseClass("default 2", 1);
        responses[2] = new responseClass("default 3", 2);
        
    
    }
    
    public void AnswerRandomised (responseClass[] responses)
    {
    for (int i = 0; i < 10; i++)

        {
        int a = Random.Range (0, 3);
        int b = Random.Range (0,3);
        responseClass temp = responses[a];
        responses[a] = responses[b];
        responses[b] = temp;
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
            
            break;
            
            case(1,1):
            responses[0].response = "Name and number, please";
            responses[1].response = "Can I grab your name and number?";
            responses[2].response = "I understand your frustration Can I have your name and number?";
            break;

            case(1,2):
            responses[0].response = "Lacie Bean, 0462811611?";
            responses[1].response = "Lacie Green, 0462711611?";
            responses[2].response = "Lacie Green, 0462811611?";
            break;

            case(1,3):
            responses[0].response = "We'll call you back when we can, bye"";
            responses[1].response = "Lacie Bean, 0462811611?";
            responses[2].response = "Lacie Green 0462711611?";
            break;


            default:
            break;
        }
        AnswerRandomised(responses);
            
    }


}



