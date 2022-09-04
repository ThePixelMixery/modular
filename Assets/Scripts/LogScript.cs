using System;
using System.Collections.Generic;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour
{
    public class LogEntry
    {
        public string time;

        public string gameEvent;

        public string tag1;

        public string tag2;

        public LogEntry(string gameEvent, string tag1, string tag2)
        {
            this.gameEvent = gameEvent;
            this.tag1 = tag1;
            this.tag2 = tag2;
        }

        public Dictionary<string, System.Object> ToDictionary()
        {
            Dictionary<string, System.Object> result =
                new Dictionary<string, System.Object>();
            result["time"] = time;
            result["gameEvent"] = gameEvent;
            result["tag1"] = tag1;
            result["tag2"] = tag2;

            return result;
        }
    }

    public static TextMeshProUGUI outputText;

    public static GameObject LoggerObject;

    private static string participantId;

    private static TMP_InputField IDInput;

    private static bool Initialised = false;

    public static void ParticipantID()
    {
        participantId = IDInput.GetComponent<TMP_InputField>().text;

        Debug.Log(participantId + " set");
    }

    public static void initialise()
    {
        if (Initialised == false)
        {
            Initialised = true;
            IDInput =
                GameObject.Find("InputField_ID").GetComponent<TMP_InputField>();
            Debug.Log(IDInput + " found");
        }
    }

    void Start()
    {
        initialise();
    }

    public static void WriteNewLogEntry(
        string gameEvent,
        string tag1,
        string tag2
    )
    {
        LogEntry logEntry = new LogEntry(gameEvent, tag1, tag2);
        DateTime dt = DateTime.Now;
        string day = dt.Day.ToString("dd");
        string month = dt.Month.ToString("MM");
        string logTime = DateTime.Now.ToString("MM/dd");
        string logTimeJson = "\""+month+"\":{\""+day+"\":{";
        logEntry.time = dt.ToString("HH:mm:ss.fff");
        string json = JsonUtility.ToJson(logEntry);
//        Debug.Log(json);

        Debug.Log(gameEvent + ", " + tag1 + ", " + tag2 + " updated");



        FirebaseDatabase
            .PushJSON("participants/"+participantId+"/"+logTime,
            json,
            "LogObject",
            "DisplayInfo",
            "DisplayErrorObject");


        Destroy (LoggerObject);
        Debug.Log(LoggerObject + " destroyed");


        void DisplayInfo(string info)
        {
            outputText.color = Color.white;
            outputText.text = info;
            Debug.Log(info);
        }

        void DisplayErrorObject(string error)
        {
            var parsedError = StringSerializationAPI.Deserialize(typeof(FirebaseError), error) as FirebaseError;
            DisplayError(parsedError.message);
        }

        void DisplayError(string error)
        {
            outputText.color = Color.red;
            outputText.text = error;
            Debug.LogError(error);
        }
    }
}
