using System;
using System.Collections.Generic;

using FirebaseWebGL.Scripts.FirebaseBridge;
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
        public Dictionary<string, System.Object> ToDictionary() {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["time"] = time;
        result["gameEvent"] = gameEvent;
        result["tag1"] = tag1;
        result["tag2"] = tag2;

        return result;
        }
    }

    public static GameObject LoggerObject;

    private static string participantId;

    private static TMP_InputField IDInput;

//    private static DatabaseReference reference;

    private static bool Initialised = false;

    
    public static void ParticipantID()
    {

        participantId = IDInput.GetComponent<TMP_InputField>().text;
        
        Debug.Log(participantId + " set");
    }

    

    public static void initialise(){
        if (Initialised == false){
        Initialised = true;
        // Get the root reference location of the database.
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//        Debug.Log(reference + " found");
        IDInput = GameObject.Find("InputField_ID").GetComponent<TMP_InputField>();
        Debug.Log(IDInput + " found");

        }
    }

    void Start()
    {
        initialise();
    }

    

    public static void WriteNewLogEntry(string gameEvent, string tag1, string tag2) {
        LogEntry logEntry = new LogEntry(gameEvent, tag1, tag2);
        DateTime dt = DateTime.Now;
        string logTime = dt.ToString("MM/dd");
        logEntry.time = dt.ToString("HH:mm:ss.fff");
        LoggerObject = new GameObject();

        LoggerObject.AddComponent<LoggerScript>();
        Debug.Log(LoggerObject + " created");

        LoggerObject.GetComponent<LoggerScript>().gameEvent = gameEvent;
        LoggerObject.GetComponent<LoggerScript>().tag1 = tag1;
        LoggerObject.GetComponent<LoggerScript>().tag2 = tag2;

        Debug.Log(gameEvent +", "+ tag1 +", "+ tag2 +" updated");

        FirebaseDatabase
            .PushJSON("https://gamification-research-default-rtdb.firebaseio.com/participants/" +
            participantId +
            "/", //Database path  
            logTime +"/",
            LoggerObject.name, //JSON string to push to the specified path
            "DisplayInfo",
            "DisplayErrorObject");
        Debug.Log(LoggerObject + " pushed");
        Destroy(LoggerObject);
        Debug.Log(LoggerObject + " destroyed");
    }
}
