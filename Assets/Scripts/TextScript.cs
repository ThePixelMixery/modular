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


public class TextScript : MonoBehaviour
{

    public TextMeshProUGUI Situation;
    public Button Option1;
    public Button Option2;
    public Button Option3;

    public TMP_InputField Login;
    public TextMeshProUGUI ErrorText;

    public TextMeshProUGUI TrainingText;


    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI TrainTimerText;
    public TextMeshProUGUI Feedback;


    public Image Correct;
    public Image Semicorrect;
    public Image Incorrect;

    public Canvas LoginCan;
    public Canvas IntroCan;
    public Canvas MenuCan;
    public Canvas TextCan;
    public Canvas AudioCan;
    public Canvas TimerCan;
    public Canvas PracCan;
    public Canvas SubmitCan;

    public Button Game;
    public Button Advance;

    public GameObject UserPoints;
    public TextMeshProUGUI PointsFeedback;

    public AudioSource Clip1;
    public AudioSource Clip2;
    public AudioSource Clip3;
    public AudioSource Clip4;

    private AudioSource ToPlay;

    private float timeLeft = 60.0f;
    private bool training = true;
    private bool timerIsRunning=false;

    public int Path;
    public int Test;
    public int Stage;

    private bool SameTest = true;
    private int CorrectAnswer;
    private int SemicorrectAnswer;
    private int IncorrectAnswer;
    private float Cliplength;


    //Week 2 is diagnostic
    //Week 3 is Validation

    private responseClass[] responseArray;

    private string[] ParticipantIDs = {
        //1-6
        "ERNADR", "AXXAMD", "NMZNNO", "XZCUEV", "IWOIUU", "ASSALI",
        //7-12
        "YKMCIM", "EYUCIK", "NLYACP", "OPBOTN", "VTYKBX", "ACCAJY",
        //13-18
        "USYMZW", "IOGWFC", "HEXFLK", "WIMEOK", "NFSAXV", "LFUMCR",
        //19-24
        "EUQMLO", "QBLUKF", "UGMGRI", "UGMGSI", "VAFBFQ", "NZMAGB",
        //25-30
        "HUNNQU", "RLUCUX", "QMWOEU", "CLJSYT", "IZRGCR", "XDGISR",
        //31-36
        "UXDIFR", "JBSGMR", "IUMSIW", "BRQQBL", "PKVDIU", "JHYIPL",
        //testing
        "test"
        };



    void Update(){
    if (timerIsRunning){
            if (timeLeft > 0){
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else{
                Debug.Log("Time has run out!");
                timeLeft = 0;
                timerIsRunning = false;
                timerEnded();
            }
        }
        else{
            TimerText.text="Ready";
        }
    }

    void DisplayTime(float timeDisplay)
    {
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        float milliSeconds = (timeDisplay % 1) * 1000;
        TrainTimerText.text = string.Format("{0:00}", seconds)+ " seconds left to memorise or notate script";
        TimerText.text = string.Format("{0:00}:{1:000}", seconds, milliSeconds);
    }

    void timerEnded()
    {
        if(training)
        {
            TrainingText.text = "Time to move on!";
        }
        else
        {
            if (Path==2){
                AnswerOutput(3);
            }
            TimerText.text="Ready";
        }

    }


    public void Pathchanger(int newPath)
    {
        timeLeft= 60.0f;
        timerIsRunning=true;
        Path = newPath;
        switch (Path)
        {
            case 1:
                TrainingText.text = "In this exercise you will be learning a script for collecting information for a help ticket. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Hello, welcome to Help Desk. My name is (your name). How can I help you today? \n\n 2) I'm having issues with my (problem) -> \n I understand your frustration. Can I have your name and number? \n\n 3) *Listen for and record details* -> \n My colleague will call you back shortly, what is a good time for a call? \n\n 4) Thank you for choosing Help Desk. We'll call at (time).";
                break;
            case 7:
                TrainingText.text = "In this exercise you will be learning a script for collecting information for a help ticket while competing with other players. You will hear a sound when you have moved up and down the leaderboard. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Hello, welcome to Help Desk. My name is (your name). How can I help you today? \n\n 2) I'm having issues with my (problem) -> \n I understand your frustration. Can I have your name and number? \n\n 3) *Listen for and record details* -> \n My colleague will call you back shortly, what is a good time for a call? \n\n 4) Thank you for choosing Help Desk. We'll call at (time).";
                break;
            default:
            break;
        }
    }


    public void AllowAnswer()
    {
        Option1.interactable = true;
        Option2.interactable = true;
        Option3.interactable = true;

    }

    public void Resetter()
    {
        if(Test>1){
        ToPlay.Stop();
        timeLeft=0.0f;
        }
        Feedback.text=" ";
        PointsFeedback.text = " ";
        Correct.gameObject.SetActive(false);
        Semicorrect.gameObject.SetActive(false);
        Incorrect.gameObject.SetActive(false);
        Advance.interactable = false;
        timeLeft = 10.0f;
        if(SameTest==true)
        {Stage++;}
        else{Test++;Stage=0;}
        int ResetAcc = 0;
        for (int i = 0; i < 3; i++)
        {
            responseArray[i].accuracy = ResetAcc;
            ResetAcc++;
        }
        TestChanger();
    }



    public void AnswerOutput(int Answer)
    {
        Feedback.text = "Option " + (CorrectAnswer+1) + " was correct";
        timeLeft=0.0f;
        timerIsRunning = false;
        Advance.interactable = true;
        Option1.interactable = false;
        Option2.interactable = false;
        Option3.interactable = false;
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
                Debug.Log("Points Worked");
                UserPoints.GetComponentInChildren<LeaderboardEntry>().UpdateScore(15);
                PointsFeedback.text = "+15";
                PointsFeedback.color = new Color(132, 155, 134, 255);
                LogScript.WriteNewLogEntry("Points", "Score", UserPoints.GetComponentInChildren<LeaderboardEntry>().score.ToString());
            }
            LogScript.WriteNewLogEntry("Answer","Correct","PlayerFeedback");
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
                UserPoints.GetComponentInChildren<LeaderboardEntry>().UpdateScore(5);
                PointsFeedback.text = "+5";
                PointsFeedback.color = new Color(242, 215, 93, 255);
                LogScript.WriteNewLogEntry("Points", "Score", UserPoints.GetComponentInChildren<LeaderboardEntry>().score.ToString());
            }
            LogScript.WriteNewLogEntry("Answer","Semicorrect","PlayerFeedback");
        }
        else if(Answer == IncorrectAnswer)
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
                UserPoints.GetComponentInChildren<LeaderboardEntry>().UpdateScore(-10);
                PointsFeedback.text = "-10";
                PointsFeedback.color = new Color(225, 92, 99, 255);
                LogScript.WriteNewLogEntry("Points", "Score", UserPoints.GetComponentInChildren<LeaderboardEntry>().score.ToString());
            }
            LogScript.WriteNewLogEntry("Answer","Incorrect","PlayerFeedback");
        }
        else if (Answer == 3)
        {
            if (Path <= 3)
            {
                //Control
                Incorrect.gameObject.SetActive(true);
            }
            else if (Path >= 4 || Path <= 6)
            {
               // Narrative Changes
               // Refresh narrative or refresh situation
            }
            else
            {
                //Point based
                //Achievement response
                UserPoints.GetComponentInChildren<LeaderboardEntry>().UpdateScore(-10);
                PointsFeedback.text = "-10";
                PointsFeedback.color = new Color(225, 92, 99, 255);
                LogScript.WriteNewLogEntry("Points", "Score", UserPoints.GetComponentInChildren<LeaderboardEntry>().score.ToString());
            }
            LogScript.WriteNewLogEntry("Answer","TimerRanOut","PlayerFeedback");
        }

    }

    public void PlayAudio()
    {
        ToPlay.Play();
        AllowAnswer();
        StartCoroutine(EndAudio());
        LogScript.WriteNewLogEntry("Sound", "Started", "PlayerFeedback");
    }

    IEnumerator EndAudio()
    {

        yield return new WaitForSeconds(Cliplength);
        timeLeft=3.0f;
        timerIsRunning=true;
        LogScript.WriteNewLogEntry("Sound", "Ended", "PlayerFeedback");
    }

    public void TestChanger()
    {
        timerIsRunning=false;
        switch (Path)
        {
            case (1 | 7):
            switch (Test)
            {
                case(0):
                TextCan.gameObject.SetActive(true);
                switch (Stage)
                    {
                    case (0):
                    Situation.text = "(The phone is ringing. What do you pick up and say?)";
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
                AllowAnswer();
                break;

                case(1):
                TextCan.gameObject.SetActive(false);
                AudioCan.gameObject.SetActive(true);
                switch (Stage)
                    {
                    case(0):
                    SameTest=true;
                    ToPlay = Clip1;
                    Debug.Log(Clip1.name);
                    Cliplength = Clip1.clip.length;
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
                AudioCan.gameObject.SetActive(true);
                TimerCan.gameObject.SetActive(true);
                timeLeft=5.0f;
                switch (Stage)
                    {
                    case(0):
                    SameTest = true;
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
                    SameTest = false;
                    break;

                    default:
                    break;
                }
                break;

                case(3):

                TextCan.gameObject.SetActive(false);
                AudioCan.gameObject.SetActive(false);
                TimerCan.gameObject.SetActive(false);
                PracCan.gameObject.SetActive(false);
                SubmitCan.gameObject.SetActive(true);
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
        Debug.Log(responseArray[0] + ", " + responseArray[1] + ", " + responseArray[2]);
    }

    public void AnswerRandomised()
    {

        for (int i = 0; i < 3; i++)
        {
        int a = Random.Range (0,3);
        int b = Random.Range (0,3);
        responseClass temp = responseArray[a];
        responseArray[a] = responseArray[b];
        responseArray[b] = temp;
        }
        Option1.GetComponentInChildren<TextMeshProUGUI>().text = responseArray[0].response;
        Option2.GetComponentInChildren<TextMeshProUGUI>().text = responseArray[1].response;
        Option3.GetComponentInChildren<TextMeshProUGUI>().text = responseArray[2].response;
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

    public void IDChecker()
    {
        string LoginAttempt = Login.text;
        for(int i=0; i<ParticipantIDs.Length; i++ )
        {
            if (ParticipantIDs[i] == LoginAttempt)
            {
                successfulLogin();
                ErrorText.text=" ";
            }
            else {
                ErrorText.text = "That doesn't look right, please try again. Remember to use CAPS";
            }
        }

    }

    public void successfulLogin()
    {
        LogScript.ParticipantID();
        LoginCan.gameObject.SetActive(false);
        IntroCan.gameObject.SetActive(true);
        Button Control1button = GameObject.Find("Button_1C").GetComponent<Button>();
        Control1button.interactable = true;
        LogScript.WriteNewLogEntry("Login", "Sessions", "Start");
    }
}