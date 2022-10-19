using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public Button Advance;

    public GameObject PointsObject;

    public GameObject UserPoints;

    public TextMeshProUGUI PointsFeedback;

    public GameObject StoryTrackerObj;
    private string situation;
    private string caller;

    private bool Story = false;

    public AudioSource ClipFail1;
    public AudioSource ClipFail2;
    public AudioSource ClipFail3;
    public AudioSource ClipTimeOut;

    public AudioSource Clip1;

    public AudioSource Clip2;
    public AudioSource Clip2a;

    public AudioSource Clip3;
    public AudioSource Clip3a;

    public AudioSource Clip4;
    public AudioSource Clip4a;

    public AudioSource Clip5;
    public AudioSource Clip5a;

    public AudioSource Clip6;
    public AudioSource Clip6a;

    public AudioSource Clip7;
    public AudioSource Clip7a;

    public AudioSource Clip8;
    public AudioSource Clip8a;

    public AudioSource Clip9;
    public AudioSource Clip9a;

    public AudioSource Clip10;
    public AudioSource Clip10a;

    public AudioSource ToPlay;

    private float timeLeft = 60.0f;

    public bool training = true;

    private bool timerIsRunning = false;

    public int Path;

    public int Test;

    public int Stage;

    public bool SameTest = true;

    private int CorrectAnswer;

    private int SemicorrectAnswer;

    private int IncorrectAnswer;

    private float Cliplength;

    //Week 2 is diagnostic
    //Week 3 is Validation
    private responseClass[] responseArray;

    private string[] ParticipantIDs =
            {
                //1-6
                "ERNADR",
                "AXXAMD",
                "NMZNNO",
                "XZCUEV",
                "IWOIUU",
                "ASSALI",
                //7-12
                "YKMCIM",
                "EYUCIK",
                "NLYACP",
                "OPBOTN",
                "VTYKBX",
                "ACCAJY",
                //13-18
                "USYMZW",
                "IOGWFC",
                "HEXFLK",
                "WIMEOK",
                "NFSAXV",
                "LFUMCR",
                //19-24
                "EUQMLO",
                "QBLUKF",
                "UGMGRI",
                "UGMGSI",
                "VAFBFQ",
                "NZMAGB",
                //25-30
                "HUNNQU",
                "RLUCUX",
                "QMWOEU",
                "CLJSYT",
                "IZRGCR",
                "XDGISR",
                //31-36
                "UXDIFR",
                "JBSGMR",
                "IUMSIW",
                "BRQQBL",
                "PKVDIU",
                "JHYIPL",
                //testing
                "test",
                "Shaun",
                "Sophie"
            };

    void Start()
    {
        responseArray = new responseClass[3];
        responseArray[0] = new responseClass("Incorrect", 0);
        responseArray[1] = new responseClass("Semicorrect", 1);
        responseArray[2] = new responseClass("Correct", 2);
        Debug
            .Log(responseArray[0].response +
            ", " +
            responseArray[1].response +
            ", " +
            responseArray[2].response);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime (timeLeft);
            }
            else
            {
                timerEnded();
                Debug.Log("Time has run out!");
                timeLeft = 0;
                timerIsRunning = false;
            }
        }
        else
        {
            TimerText.text = "Ready";
        }
    }

    public void IDChecker()
    {
        string LoginAttempt = Login.text;
        for (int i = 0; i < ParticipantIDs.Length; i++)
        {
            if (ParticipantIDs[i] == LoginAttempt)
            {
                successfulLogin();
                ErrorText.text = " ";
            }
            else
            {
                ErrorText.text =
                    "That doesn't look right, please try again. Remember to use CAPS";
            }
        }
    }

    public void successfulLogin()
    {
        LogScript.ParticipantID();
        LoginCan.gameObject.SetActive(false);
        IntroCan.gameObject.SetActive(true);
        Button Control1button =
            GameObject.Find("Button_1C").GetComponent<Button>();
        LogScript.WriteNewLogEntry("Login", "Sessions", "Start");
    }

    void DisplayTime(float timeDisplay)
    {
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        float milliSeconds = (timeDisplay % 1) * 1000;
        TrainTimerText.text =
            string.Format("{0:00}", seconds) +
            " seconds left to memorise or notate script";
        TimerText.text = string.Format("{0:00}:{1:000}", seconds, milliSeconds);
    }

    void timerEnded()
    {
        if (training)
        {
            TrainingText.text = "Time to move on!";
        }
        else
        {
            if (Test == 2)
            {
                Debug.Log("Before Answer 3");
                AnswerOutput(3);
                Debug.Log("After Answer 3");
            }
            TimerText.text = "Ready";
        }
    }

    public void PlayAudio()
    {
        Cliplength = ToPlay.clip.length;
        ToPlay.Play();
        StartCoroutine(EndAudio());
        LogScript.WriteNewLogEntry("Sound", "Started", "PlayerFeedback");
    }

    IEnumerator EndAudio()
    {
        yield return new WaitForSeconds(Cliplength);
        timeLeft = 3.0f;
        if (ToPlay != ClipTimeOut){timerIsRunning = true;}
        ShowAnswers();
        AllowAnswer();
        if (Path == 4){
        StoryTracker.OutputPrompt(caller, situation);}
        LogScript.WriteNewLogEntry("Sound", "Ended", "PlayerFeedback");
    }

    public void Resetter()
    {
        if (Test > 1)
        {
            ToPlay.Stop();
            timeLeft = 0.0f;
        }
        Option1.GetComponentInChildren<TextMeshProUGUI>().text = " ";
            Option2.GetComponentInChildren<TextMeshProUGUI>().text ="";
            Option3.GetComponentInChildren<TextMeshProUGUI>().text ="";
        Feedback.text = " ";
        PointsFeedback.text = " ";
        Correct.gameObject.SetActive(false);
        Semicorrect.gameObject.SetActive(false);
        Incorrect.gameObject.SetActive(false);
        Advance.interactable = false;
        timeLeft = 10.0f;
        int ResetAcc = 0;
        for (int i = 0; i < 3; i++)
        {
            responseArray[i].accuracy = ResetAcc;
            ResetAcc++;
        }
            if (SameTest == true)
            {
                Stage++;
            }
            else
            {
                Test++;
                Story = false;
                Stage = 0;
            }
        TestChanger();
        
    }

    public void Pathchanger(int newPath)
    {
        timeLeft = 60.0f;
        timerIsRunning = true;
        Path = newPath;
        switch (Path)
        {
            case 1:
                TrainingText.text =
                    "In this exercise you will be learning a script for collecting information for a help ticket. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Hello, welcome to Help Desk. My name is (your name). How can I help you today? \n\n 2) I'm having issues with my (problem) -> \n I understand your frustration. Can I have your name and number? \n\n 3) *Listen for and record details* -> \n I'm directing you to the relevant department now \n\n";
            break;
            case 2:
                TrainingText.text =
                    "In this exercise you will be learning a script for addressing the problem on a help ticket. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Thank you for holding, Lacie. I believe your problem is to do with your internet \n\n 2) (Problem clarified) -> \n I'm going to run through a list of troubleshooting steps with you, is that okay? \n\n 3) *Presumed agreement* -> \n Have you tried turning the device off and back on again? \n\n 4)Was it really that simple? Thanks anyway! -> Not a problem, call us back anytime you need help. \n\n Bear in mind we also handle issues to do with device failure, a common solution is to unplug the usb device and plug it back in. Another common issue is making sure the device is powered on at both the outlet and on the device as power outlets don't always have power switches";                    
                break;
            case 3:
                TrainingText.text =
                    "In this exercise you will be learning a script for authenticating a user who needs access into their account. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Thank you for holding, (name). I believe your problem is to do with your account? Do you mind if I ask you a few questions to make sure I have the right person \n\n 2) *Persumed agreement* -> \n (Check their details one at a time, according to example accounts below) \n\n 3) *Presumed validation* -> \n Thank you for letting me check! How can I help? \n\n 4) My identity just needed to be verified, I think. I'll call back if I have any other issues -> Great, thank you for your call! \n\n Example Accounts: \n Name: Amy Lee - Birthdate: 19/6/1996 - Suburb: Box Hill - Driver's License number: 194027495 \nName: Jerry Khan - Birthdate: 24/9/1985 - Suburb: Lalor - Driver's License number: 659731685 \nName: Tony Banks - Birthdate: 14/9/2001 - Suburb: Ferngully - Driver's License number: 684231895";                    
                break;
            case 4:
                TrainingText.text =
                    "Hi! Can you help me learn a script for collecting information for a help ticket? I'll write down your answers on the right so you can remember how things turned out. Let's read it together: \n \n 1) *Phone rings* -> \n Hello, welcome to Help Desk. My name is (your name). How can I help you today? \n\n 2) I'm having issues with my (problem) -> \n I understand your frustration. Can I have your name and number? \n\n 3) *Listen for and record details* -> \n I'm directing you to the relevant department now \n\n";
                StoryTracker.Starter();
                StoryTracker.OutputPrompt("", "(Here is were you'll find the history of what you've said)");
                break;
            case 5:
                TrainingText.text =
                    "I passed the first test! Can you help me again? This time we are learning a script for addressing the problem on a help ticket? I'll write down your answers on the right so you can remember how things turned out. Let's read it together: \n \n 1) *Phone rings* -> \n Thank you for holding, Lacie, I believe your problem is to do with your internet. \n\n 2) (Problem clarified) -> \n I'm going to run through a list of troubleshooting steps with you, is that okay? \n\n 3) *Presumed agreement* -> \n Have you tried turning the device off and back on again? \n\n 4)Was it really that simple? Thanks anyway! -> Not a problem, call us back anytime you need help. \n\n Bear in mind we also handle issues to do with device failure, a common solution is to unplug the usb device and plug it back in. Another common issue is making sure the device is powered on at both the outlet and on the device as power outlets don't always have power switches \n\n I'm ready. Are you?";
                StoryTracker.Starter();
                StoryTracker.OutputPrompt("", "(Here is were you'll find the history of what you've said)");
                break;
            case 6:
                TrainingText.text =
                    "I'm almost hired, I can feel it! Can you help me one last time? This time we are learning a script for authenticating a user who needs access into their account. I'll write down your answers on the right so you can remember how things turned out. Let's read it together: \n 1) *Phone rings* -> \n Thank you for holding, (name). I believe your problem is to do with your account? Do you mind if I ask you a few questions to make sure I have the right person \n\n 2) *Persumed agreement* -> \n (Check their details one at a time, according to example accounts below) \n\n 3) *Presumed validation* -> \n Thank you for letting me check! How can I help? \n\n 4) My identity just needed to be verified, I think. I'll call back if I have any other issues -> Great, thank you for your call! \n\n Example Accounts: \nName: Amy Lee - Birthdate: 19/6/1996 - Suburb: Box Hill - Driver's License number: 194027495 \nName: Jerry Khan - Birthdate: 24/9/1985 - Suburb: Lalor - Driver's License number: 659731685 \nName: Tony Banks - Birthdate: 14/9/2001 - Suburb: Ferngully - Driver's License number: 684231895 \n\n I'm ready. Are you?";
                StoryTracker.Starter();
                StoryTracker.OutputPrompt("", "(Here is were you'll find the history of what you've said)");
                break;

            case 7:
            TrainingText.text=
                    "In this exercise you will be learning a script for collecting information for a help ticket while competing with other players. You will hear a sound when you have moved up and down the leaderboard. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Hello, welcome to Help Desk. My name is (your name). How can I help you today? \n\n 2) I'm having issues with my (problem) -> \n I understand your frustration. Can I have your name and number? \n\n 3) *Listen for and record details* -> \n I'm directing you to the relevant department now";
                break;
            case 8:
                TrainingText.text =
                    "In this exercise you will be learning a script for addressing the problem on a help ticket while competing with other players. You will hear a sound when you have moved up and down the leaderboard. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Thank you for holding, Lacie. I believe your problem is to do with your internet \n\n 2) (Problem clarified) -> \n I'm going to run through a list of troubleshooting steps with you, is that okay? \n\n 3) *Presumed agreement* -> \n Have you tried turning the device off and back on again? \n\n 4)Was it really that simple? Thanks anyway! -> Not a problem, call us back anytime you need help. \n\n Bear in mind we also handle issues to do with device failure, a common solution is to unplug the usb device and plug it back in. Another common issue is making sure the device is powered on at both the outlet and on the device as power outlets don't always have power switches";
                break;

            case 9:
                TrainingText.text =
                    "In this exercise you will be learning a script for authenticating a user who needs access into their account while competing with other players. You will hear a sound when you have moved up and down the leaderboard. Please make a note of the following interation: \n \n 1) *Phone rings* -> \n Thank you for holding, (name). I believe your problem is to do with your account? Do you mind if I ask you a few questions to make sure I have the right person \n\n 2) *Persumed agreement* -> \n (Check their details one at a time, according to example accounts below) \n\n 3) *Presumed validation* -> \n Thank you for letting me check! How can I help? \n\n 4) My identity just needed to be verified, I think. I'll call back if I have any other issues -> Great, thank you for your call! \n\n Example Accounts: Name: Amy Lee - Birthdate: 19/6/1996 - Suburb: Box Hill - Driver's License number: 194027495 \nName: Jerry Khan - Birthdate: 24/9/1985 - Suburb: Lalor - Driver's License number: 659731685 \nName: Tony Banks - Birthdate: 14/9/2001 - Suburb: Ferngully - Driver's License number: 684231895";                    
                break;
            default:
                break;
        }
    }

    public void Trainfalser(){
    training = false;}

    public void TestChanger()
    {
        
        //Debug.Log("Text Test Changed");
        timerIsRunning = false;
        switch (Test)
        {
            case (0):
                TextCan.gameObject.SetActive(true);
                break;
            case (1):
                TextCan.gameObject.SetActive(false);
                AudioCan.gameObject.SetActive(true);
                break;
            case (2):
                AudioCan.gameObject.SetActive(true);
                TimerCan.gameObject.SetActive(true);
                timeLeft = 5.0f;
                break;
            case (3):
                TextCan.gameObject.SetActive(false);
                AudioCan.gameObject.SetActive(false);
                TimerCan.gameObject.SetActive(false);
                PracCan.gameObject.SetActive(false);
                SubmitCan.gameObject.SetActive(true);
                break;
            default:
                Situation.text =
                    "Oops, something is wrong. Contact the researcher right away!";
                break;
        }
        ButtonUpdater();
    }


    public void ButtonUpdater()
    {
        if (Path == 1 | Path == 4 | Path == 7)
        {
            switch (Stage)
            {
                case (0):
                    responseArray[0].response = "Hi. What do you want?";
                    responseArray[1].response =
                        "Hello, what can I do for you today?";
                    responseArray[2].response =
                        "Hello, welcome to help desk. My name is X. How can I help you today?";
                    break;
                case (1):
                    responseArray[0].response = "Name and number, please";
                    responseArray[1].response =
                        "Can I grab your name and number?";
                    responseArray[2].response =
                        "I understand your frustration Can I have your name and number?";
                    break;
                case (2):
                    responseArray[0].response = "Lacie Green, 0462711611?";
                    responseArray[1].response = "Lacie Bean, 0462811611?";
                    responseArray[2].response = "Lacie Green, 0462811611?";
                    break;
                case (3):
                    responseArray[0].response =
                        "We'll call you back when we can, bye";
                    responseArray[1].response =
                        "Someone from the correct department will call you back soon";
                    responseArray[2].response =
                        "I'm transferring you to the correct department, please hold";
                    break;
                default:
                    break;
            }

        }
        //Debug.Log("Text Buttons updated");
        if (Path == 2 | Path == 5 | Path == 8)
        {
            switch (Stage)
            {
                case (0):
                    responseArray[0].response = "Is this the chick with the internet issue?";
                    responseArray[1].response =
                        "Thanks for holding, what's up?";
                    responseArray[2].response =
                        "Thank you for holding, Lacie. I believe your issue is with your internet.";
                    break;
                case (1):
                    responseArray[0].response = "Oh, that's so easy, just google it.";
                    responseArray[1].response =
                        "Cool, this is how you fix it, are you ready?";
                    responseArray[2].response =
                        "I'm going to run through a list of troubleshooting steps with you, is that okay?";
                    break;
                case (2):
                    responseArray[0].response = "Is the USB cable plugged in?";
                    responseArray[1].response = "Did you check the power is on at both the outlet and on the device?";
                    responseArray[2].response = "Have you tried turning the device off and back on again?";
                    break;
                case (3):
                    responseArray[0].response =
                        "I don't know why you didn't just google it.";
                    responseArray[1].response =
                        "Happy to help, have a nice day.";
                    responseArray[2].response =
                        "Not a problem, call us back anytime you need help.";
                    break;
                default:
                    break;
            }
        }
        if (Path == 3 | Path == 6 | Path == 9)
        {
            switch (Stage)
            {
                case (0):
                    responseArray[0].response = "What's your account issue?";
                    responseArray[1].response =
                        "Thanks for holding, how can I help?";
                    responseArray[2].response =
                        "Thank you for holding, Amy. May I ask you a few questions to make sure I have the right person?";
                    break;
                case (1):
                    responseArray[0].response = "Name, birthdate, suburb, driver's license.";
                    responseArray[1].response =
                        "I willl need the following in this order: Your name, birthdate, suburb, driver's license";
                    responseArray[2].response =
                        "Can you please confirm your name, birthdate, suburb, driver's license for me?";
                    break;
                case (2):
                    responseArray[0].response = "Thanks for your cooperation, I'll go unlock it now.";
                    responseArray[1].response =
                        "This does look right according to our records";
                    responseArray[2].response =
                        "That doesn't look right. Are you sure about the information you provided?";
                    break;
                case (3):
                    responseArray[0].response = "Thank you for letting me check! How can I help?";
                    responseArray[1].response = "Ma'am, I don't think you're accessing your account";
                    responseArray[2].response = "Thank you for your call but I can't authenticate you at this time.";
                    break;
                default:
                    break;
            }

        }
        AnswerRandomised();
    }


    public void AnswerRandomised()
    {
        int i;
        //Debug.Log("Text Answers Randomised");
        for (i = 0; i < 3; i++)
        {
            int a = Random.Range(0, 3);
            int b = Random.Range(0, 3);
            responseClass temp = responseArray[a];
            responseArray[a] = responseArray[b];
            responseArray[b] = temp;
        }
        for (i = 0; i < 3; i++)
        {
            switch (responseArray[i].accuracy)
            {
                case (0):
                    IncorrectAnswer = i;
                    break;
                case (1):
                    SemicorrectAnswer = i;
                    break;
                case (2):
                    CorrectAnswer = i;
                    break;
                default:
                    Debug.Log("I don't feel so good");
                    break;
            }
        }
        Stager();
    }

    void ShowAnswers()
    { 
    Option1.GetComponentInChildren<TextMeshProUGUI>().text =
                responseArray[0].response;
            Option2.GetComponentInChildren<TextMeshProUGUI>().text =
                responseArray[1].response;
            Option3.GetComponentInChildren<TextMeshProUGUI>().text =
                responseArray[2].response;
    }

        public void Stager()
    {
    situation = "staging situation";
        if (Path == 1 || Path == 7 || Path == 4)
        {
            switch (Stage, Story)
            {
                case (0, false):
                    SameTest = true;
                    situation = "(The phone is ringing. What do you pick up and say?)";
                    ToPlay = Clip1;
                    Debug.Log(Clip1.name);
                    break;
                case (1, false):
                    situation = "I'm having trouble with my internet speed";
                    ToPlay = Clip2;
                    break;
                case (1, true):
                    situation =
                        "Oh, thank you! I'm having trouble with my internet";
                    ToPlay = Clip2a;
                    break;
                case (2, false):
                    situation =
                        "Sure, my name and number is Lacie Green, 04 6281 1611";
                    ToPlay = Clip3;
                    break;
                case (2, true):
                    situation =
                        "I appreciate that and of course, my name is Lacie Green, and my number is 04 6281 1611";
                    ToPlay = Clip3a;
                    break;
                case (3, false):
                    situation = "No, Green. Never mind";
                    ToPlay = Clip4;
                    SameTest = false;
                    break;
                case (3, true):
                    situation = "That's right! When can I expect a solution?";
                    ToPlay = Clip4a;
                    SameTest = false;
                    break;
                default:
                    break;
            }
            caller = "Lacie: ";
            
        }
        //Debug.Log("Text Staged, Path is " + Path);
        if (Path == 2 || Path == 5 || Path == 8)
        {
            switch (Stage, Story)
            {
                case (0, false):
                    SameTest = true;
                    situation = "(You've been routed a call with a Lacie Green who is having issues with her internet. What do you do when you pick it up? )";
                    ToPlay = Clip1;
                    Debug.Log(Clip1.name);
                    break;
                case (1, false):
                    situation = "It's my internet, it's running so slow!";
                    ToPlay = Clip5;
                    break;
                case (1, true):
                    situation =
                        "That's me! It's running so slow, is this normal?";
                    ToPlay = Clip5a;
                    break;
                case (2, false):
                    situation =
                        "I'm ready";
                    ToPlay = Clip6;
                    break;
                case (2, true):
                    situation =
                        "Of course, thank you for your help.";
                    ToPlay = Clip6a;
                    break;
                case (3, false):
                    situation = "I did, but I've got funny outlets, I'll try switching them off and on again.";
                    ToPlay = Clip7;
                    SameTest = false;
                    break;
                case (3, true):
                    situation = "Was it really that simple? Thanks anyway!";
                    ToPlay = Clip7a;
                    SameTest = false;
                    break;
                default:
                    break;
            }
            caller = "Lacie: ";
            
        }
        if (Path == 3 || Path == 6 || Path == 9)
        {
            switch (Stage, Story)
            {
                case (0, false):
                    SameTest = true;
                    situation = "(You've been routed a call with a Amy Bell. The only Amy Bell who has an account with us has shared the following information with us: Name: Amy Bell \n Birthdate: 20/3/1992 \n Suburb: Malvern \n Driver's License number: 194048206. What do you do when you pick it up? )";
                    ToPlay = Clip1;
                    Debug.Log(Clip1.name);
                    break;
                case (1, false):
                    situation = "Yeah, what do you need?";
                    ToPlay = Clip8;
                    break;
                case (1, true):
                    situation =
                        "Yes! How can I help?";
                    ToPlay = Clip8a;
                    break;
                case (2, false):
                    situation =
                        "Amy Bell, 20/3/92, Living in Malvern, and my driver's license number is 194047206";
                    ToPlay = Clip9;
                    break;
                case (2, true):
                    situation = "Of course, my name is Amy Bell, My birthday is 20/3/1992, I'm living in Malvern, and my driver's license number is 194047206";
                    ToPlay = Clip9a;
                    break;
                case (3, false):
                    situation = "Oh, um... I must have put in the wrong information the first time. Can you make an exception?";
                    ToPlay = Clip10;
                    SameTest = false;
                    break;
                case (3, true):
                    situation = "This is really bad service!";
                    ToPlay = Clip10a;
                    SameTest = false;
                    break;
                default:
                    break;
            }
            caller = "Amy: ";
            
        }
        Debug.Log("Stage "+Stage+", Story "+Story);
        Situation.text = situation;
        if (Test == 0)
        {
            ShowAnswers();
            AllowAnswer();
            StoryTracker.OutputPrompt(caller,situation);
        }
    }

    public void AllowAnswer()
    {
        Option1.interactable = true;
        Option2.interactable = true;
        Option3.interactable = true;
    }

    public void AnswerOutput(int Answer)
    {
        //Debug.Log("Text Answered");
        Debug.Log(Path);
        Feedback.text = "Option " + (CorrectAnswer + 1) + " was correct";
        timeLeft = 0.0f;
        timerIsRunning = false;
        Advance.interactable = true;
        Option1.interactable = false;
        Option2.interactable = false;
        Option3.interactable = false;
        Debug.Log(Answer+" answered");
        if (Answer == CorrectAnswer)
        {
            if (Path <= 3)
            {
                //Control
                Correct.gameObject.SetActive(true);
            }
            else if (Path >= 4 && Path <= 6)
            {
                // Narrative Changes
                Story = true;
                StoryTracker.OutputAnswer(responseArray[CorrectAnswer].response, 1);
                LogScript
                    .WriteNewLogEntry("Story",
                    "Correct",
                    StoryTracker.score.ToString());
                Debug
                    .Log("Sent accurancy 1, " +
                    responseArray[CorrectAnswer].response);
            }
            else
            {
                //Point based
                Debug.Log("Points Worked");
                UserPoints
                    .GetComponentInChildren<LeaderboardEntry>()
                    .UpdateScore(15);
                PointsFeedback.text = "+15";
                PointsObject.GetComponent<PointsMode>().AIbot();
                PointsFeedback.color = new Color32(132, 155, 134, 255);
                LogScript
                    .WriteNewLogEntry("Points",
                    "Score",
                    UserPoints
                        .GetComponentInChildren<LeaderboardEntry>()
                        .score
                        .ToString());
            }
            LogScript.WriteNewLogEntry("Answer", "Correct", "PlayerFeedback");
        }
        else if (Answer == SemicorrectAnswer)
        {
            if (Path <= 3)
            {
                //Control
                Semicorrect.gameObject.SetActive(true);
            }
            else if (Path >= 4 && Path <= 6)
            {
                Story = false;
                                LogScript
                    .WriteNewLogEntry("Story",
                    "Semicorrect",
                    StoryTracker.score.ToString());
                StoryTracker.OutputAnswer(responseArray[SemicorrectAnswer].response, 0);
                Debug
                    .Log("Sent accurancy 0, " +
                    responseArray[SemicorrectAnswer].response);
            }
            else
            {
                //Point based
                UserPoints
                    .GetComponentInChildren<LeaderboardEntry>()
                    .UpdateScore(5);
                PointsFeedback.text = "+5";
                PointsObject.GetComponent<PointsMode>().AIbot();
                PointsFeedback.color = new Color32(242, 215, 93, 255);
                LogScript
                    .WriteNewLogEntry("Points",
                    "Score",
                    UserPoints
                        .GetComponentInChildren<LeaderboardEntry>()
                        .score
                        .ToString());
            }
            LogScript
                .WriteNewLogEntry("Answer", "Semicorrect", "PlayerFeedback");
        }
        else if (Answer == IncorrectAnswer)
        {
            if (Path <= 3)
            {
                //Control
                Incorrect.gameObject.SetActive(true);
            }
            else if (Path >= 4 && Path <= 6)
            {
                // Narrative Changes
                StoryTracker.OutputAnswer(responseArray[IncorrectAnswer].response, -1);
                Debug.Log("Story failed");
                situation = "retry";
                int rand = Random.Range(0,2);
                switch (rand)
                {
                    case 0:
                        situation = "Luckily, I was a test calller! Let's try that again";
                        ToPlay = ClipFail1;
                    break;
                    case 1:
                        situation = "Sorry, I have bad reception. Can you say that again?";
                        ToPlay = ClipFail2;
                    break;
                    case 2:
                        situation = "Sorry, I didn't hear you. What was that?";
                        ToPlay = ClipFail3;
                    break;
                    default:
                    break;
                }
                
                Situation.text=situation;
                    
                StoryTracker.OutputPrompt(caller, situation);
                SameTest=true;
                Stage--;
                if (Test > 0){PlayAudio();}
                            LogScript
                    .WriteNewLogEntry("Story",
                    "Incorrect",
                    StoryTracker.score.ToString());

            }
            else
            {
                //Point based
                UserPoints
                    .GetComponentInChildren<LeaderboardEntry>()
                    .UpdateScore(-10);
                PointsFeedback.text = "-10";
                PointsObject.GetComponent<PointsMode>().AIbot();
                PointsFeedback.color = new Color32(225, 92, 99, 255);
                LogScript
                    .WriteNewLogEntry("Points",
                    "Score",
                    UserPoints
                        .GetComponentInChildren<LeaderboardEntry>()
                        .score
                        .ToString());
            }
            LogScript.WriteNewLogEntry("Answer", "Incorrect", "PlayerFeedback");
        }
        else if (Answer == 3)
        {
            if (Path <= 3)
            {
                //Control
                timerIsRunning=false;
                Incorrect.gameObject.SetActive(true);
            }
            else if (Path >= 4 && Path <= 6)
            {
                int rand = Random.Range(0,3);
                switch (rand)
                {
                    case 0:
                        situation = "Luckily, I was a test calller! Let's try that again";
                        ToPlay = ClipFail1;
                    break;
                    case 1:
                        situation = "Sorry, I have bad reception. Can you say that again?";
                        ToPlay = ClipFail2;
                    break;
                    case 2:
                        situation = "Sorry, I didn't hear you. What was that?";
                        ToPlay = ClipFail3;
                    break;
                    case 3:
                        situation = "Hello?";
                        ToPlay = ClipTimeOut;
                    break;
                    default:
                    break;
                }
                SameTest = true;
                Stage--;
                timerIsRunning=false;
                StoryTracker.OutputPrompt(caller, situation);
                LogScript
                    .WriteNewLogEntry("Story",
                    "Timeout",
                    StoryTracker.score.ToString());
                
                
            }
            else
            {
                //Point based
                //Achievement response
                UserPoints
                    .GetComponentInChildren<LeaderboardEntry>()
                    .UpdateScore(-10);
                PointsFeedback.text = "-10";
                PointsObject.GetComponent<PointsMode>().AIbot();
                PointsFeedback.color = new Color32(225, 92, 99, 255);
                LogScript
                    .WriteNewLogEntry("Points",
                    "Score",
                    UserPoints
                        .GetComponentInChildren<LeaderboardEntry>()
                        .score
                        .ToString());
            }
            timerIsRunning=false;
            LogScript
                .WriteNewLogEntry("Answer", "TimerRanOut", "PlayerFeedback");
        }
        
    }
}