using System;
using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour
{
    public class LogEntry
    {
        public string gameEvent;

        public string tag1;

        public string tag2;

        public LogEntry(string gameEvent, string tag1, string tag2)
        {
            this.gameEvent = gameEvent;
            this.tag1 = tag1;
            this.tag2 = tag2;
        }
    }

    private string participantId;

    private TMP_InputField IDInput;

    private DatabaseReference reference;

    public void ParticipantID()
    {
        participantId = IDInput.GetComponent<TMP_InputField>().text;

        Debug.Log(participantId + " set");
    }

    void Start()
    {
        // Get the root reference location of the database.
        DatabaseReference reference =
            FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(reference + " found");
        IDInput =
            GameObject.Find("InputField_ID").GetComponent<TMP_InputField>();
        Debug.Log(IDInput + " found");
    }

    public void WriteNewLogEntry(string gameEvent, string tag1, string tag2)
    {
        LogEntry logEntry = new LogEntry(gameEvent, tag1, tag2);
        string json = JsonUtility.ToJson(logEntry);

        reference
            .Child("participants")
            .Child(participantId)
            .SetValueAsync(json);
    }
}
