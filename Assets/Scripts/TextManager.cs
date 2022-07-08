using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//tick and cross created using loading.io

public class responseClass
{
    public string response;
    public int accuracy;
    public responseClass(string Response, int Accuracy)
    {
        response = Response;
        accuracy = Accuracy;
    }
    public void UpdateText(string NewResponse)
    {
        response = NewResponse;
    }
}

    

public class TextManager : MonoBehaviour
{
    
    public TextMeshProUGUI Situation;
    
    public Button Option1Button;
    public TextMeshProUGUI Option1;
    public Button Option2Button;
    public TextMeshProUGUI Option2;
    public Button Option3Button;
    public TextMeshProUGUI Option3;
    
    public TextMeshProUGUI TimerText;

    public Image Correct;
    public Image Semicorrect;
    public Image Incorrect;

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
    private bool timerRunning = false;
    private float TimeLeft;
    private bool SameTest = true;
    private int CorrectAnswer;
    private int SemicorrectAnswer;
    private int IncorrectAnswer;
    private float Cliplength;


    public responseClass[] responseArray;


    
    IEnumerator RunTimer()
    {
        yield return new WaitForSeconds(Cliplength);
        Debug.Log("Started timer!");
        Option1.gameObject.SetActive(true);
        Option2.gameObject.SetActive(true);
        Option3.gameObject.SetActive(true);

        TimeLeft = 5.0f;
        timerRunning= true;
    }

    public void Pathchanger(int newPath)
    {
        Path = newPath;
    }

    public void Resetter()
    {
        Correct.gameObject.SetActive(false);
        Semicorrect.gameObject.SetActive(false);
        Incorrect.gameObject.SetActive(false);
        Advance.interactable = false;
        Option1Button.interactable=true;
        Option1Button.interactable=true;
        Option1Button.interactable=true;
        int ResetAcc = 0;
        for (int i = 0; i < 3; i++)
        {
            responseArray[i].accuracy = ResetAcc;
            ResetAcc++;
        }        
        if(SameTest==true) 
        {Stage++;}
        else{Test++;Stage=0;}
        if (Test == 2)
        {
            Option1.gameObject.SetActive(false);
            Option2.gameObject.SetActive(false);
            Option3.gameObject.SetActive(false);
        }

        TestChanger();
    }

    public void AnswerOutput(int Answer)
    {
        Option1Button.interactable=false;
        Option2Button.interactable=false;
        Option3Button.interactable=false;
        if (Answer == CorrectAnswer)
        {
            if (Path <= 3)
            {
                //Control
                Correct.gameObject.SetActive(true);

            }
            else if (Path >= 4 || Path <= 6)
            {
               // Narrative Changes
            }
            else
            {
              //Point based
            }
        }
        else if(Answer == SemicorrectAnswer)
        {
            if (Path <= 3)
            {
                //Control
                Semicorrect.gameObject.SetActive(true);
            }
            else if (Path >= 4 || Path <= 6)
            {
               // Narrative Changes
            }
            else
            {
              //Point based
            }

        }
        else
        {
            if (Path <= 3)
            {
                //Control
                Incorrect.gameObject.SetActive(true);
            }
            else if (Path >= 4 || Path <= 6)
            {
               // Narrative Changes
            }
            else
            {
              //Point based
            }
        }
        Advance.interactable = true;
    }

    public void PlayAudio()
    {
        ToPlay.Play();
        if (Test == 2)
        {
            StartCoroutine(RunTimer());
        }
    }

    

    public void TestChanger()
    {
        switch (Path)
        {
            case(1):
            switch (Test)
            {
                case(0):
                TextCan.gameObject.SetActive(true);
                switch (Stage)
                    {
                    case 0:
                    Situation.text = "*Phone Rings*";
                    break;

                    case(1):
                    Situation.text = "I'm having trouble with my internet speed";
                    break;

                    case(2):
                    Situation.text = "My name is Lacie Green and my number is 04 6281 1611";
                    break;

                    case(3):
                    Situation.text = "When can I expect a solution?";
                    SameTest = false;
                    break;

                    default:
                    break;
                }
                break;
                
                case(1):
                TextCan.gameObject.SetActive(false);
                AudioCan.gameObject.SetActive(true);
                Option1.gameObject.SetActive(false);
                Option2.gameObject.SetActive(false);
                Option3.gameObject.SetActive(false);
                switch (Stage)
                    {
                    case(0):
                    SameTest=true;
                    Cliplength = Clip1.clip.length;
                    ToPlay = Clip1;
                    break;

                    case(1):
                    ToPlay = Clip2;
                    Cliplength = Clip2.clip.length;
                    break;

                    case(2):
                    ToPlay = Clip3;
                    Cliplength = Clip3.clip.length;
                    break;

                    case(3):
                    ToPlay = Clip4;
                    Cliplength = Clip4.clip.length;
                    SameTest = false;
                    break;

                    default:
                    break;
                }                                
                break;

                case(2):
                TimerCan.gameObject.SetActive(true);

                Option1.gameObject.SetActive(false);
                Option2.gameObject.SetActive(false);
                Option3.gameObject.SetActive(false);

                switch (Stage)
                    {
                    case(0):
                    SameTest=true;
                    Cliplength = Clip1.clip.length;
                    ToPlay = Clip1;
                    break;

                    case(1):
                    ToPlay = Clip2;
                    Cliplength = Clip2.clip.length;
                    break;

                    case(2):
                    ToPlay = Clip3;
                    Cliplength = Clip3.clip.length;
                    break;

                    case(3):
                    ToPlay = Clip4;
                    Cliplength = Clip4.clip.length;
                    SameTest = false;
                    break;

                    default:
                    break;
                }                          
                break;

                default:
                break;
            }
            break;
            default: 
            Situation.text = "Oops, something is wrong. Contact the researcher right away!";
            break;

        }
        ButtonUpdater();
    }

    void Start() 
    {
        responseArray = new responseClass[3];
        responseArray[0] = new responseClass("Incorrect", 0);
        responseArray[1] = new responseClass("Semicorrect", 1);
        responseArray[2] = new responseClass("Correct", 2); 
        
    }

    void Update()
    {
        if (timerRunning == true)
        {
            if (TimeLeft >= 0.5f)
            {
            TimeLeft -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(TimeLeft%60);
            TimerText.text = seconds.ToString();
            }
            else
            {
                Debug.Log("Timer ran out!");
                TimeLeft = 0f;
                timerRunning = false;
                AnswerOutput(3);
            }
        }
    }
    
    public void AnswerRandomised()
    {
    
        for (int i = 0; i < 10; i++)
        {
        int a = Random.Range (0,3);
        int b = Random.Range (0,3);
        responseClass temp = responseArray[a];
        responseArray[a] = responseArray[b];
        responseArray[b] = temp;
        }
        Option1.text = responseArray[0].response;
        Option2.text = responseArray[1].response;
        Option3.text = responseArray[2].response;
        AnswerChecker();
    }

    public void AnswerChecker()
    {
        int i = 0;
        for (i = 0; i < 3; i++)
        {
        switch(responseArray[i].accuracy)
            {
                case(0):
                IncorrectAnswer = i;
                break;

                case(1):
                SemicorrectAnswer = i;
                break;

                case(2):
                CorrectAnswer = i;
                break;

                default:
                Debug.Log("I don't feel so good");
                break;
            }   
        }
    }

    public void ButtonUpdater()
    {
        switch(Path, Stage)
        {
            case(1,0):
            responseArray[0].response ="Hi. What do you want?";
            responseArray[1].response = "Hello, what can I do for you today?";
            responseArray[2].response = "Hello, welcome to help desk. My name is X. How can I help you today?";
            break;
            
            case(1,1):
            responseArray[0].response = "Name and number, please";
            responseArray[1].response = "Can I grab your name and number?";
            responseArray[2].response = "I understand your frustration Can I have your name and number?";
            break;

            case(1,2):
            responseArray[0].response = "Lacie Green, 0462711611?";
            responseArray[1].response = "Lacie Bean, 0462811611?";
            responseArray[2].response = "Lacie Green, 0462811611?";
            break;

            case(1,3):
            responseArray[0].response = "We'll call you back when we can, bye";
            responseArray[1].response = "Someone from the correct department will call you back soon";
            responseArray[2].response = "I'm transferring you to the correct department, please hold";
            break;

            default:
            break;
        }
        AnswerRandomised();
            
    }


}



