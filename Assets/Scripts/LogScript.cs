using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
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


    private static string participantId;

    private static TMP_InputField IDInput;

    private static DatabaseReference reference;

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
        reference =
            FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(reference + " found");
        IDInput =
            GameObject.Find("InputField_ID").GetComponent<TMP_InputField>();
        Debug.Log(IDInput + " found");
    }
    }

    void Start()
    {
        initialise();
    }

    public static void WriteNewLogEntry(string gameEvent, string tag1, string tag2) {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        string key = reference.Child("participants").Child(participantId).Push().Key;
        Debug.Log(key);
        LogEntry logEntry = new LogEntry(gameEvent, tag1, tag2);
        DateTime dt = DateTime.Now;
        string logTime = dt.ToString("MM/dd");
        logEntry.time = dt.ToString("HH:mm:ss");
        Dictionary<string, System.Object> entryValues = logEntry.ToDictionary();

        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
        Debug.Log(childUpdates);
        childUpdates["/participants/" + participantId + "/" + logTime + "/" + key] = entryValues;

        reference.UpdateChildrenAsync(childUpdates);
    }
}