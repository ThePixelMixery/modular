using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class SaveToJson : MonoBehaviour
{
    DatabaseReference reference;

    TMP_InputField IDInput;

    log logEntryScript;

    string participantId;

    void Start()
    {
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log (reference);
        IDInput =
            GameObject.Find("InputField_ID").GetComponent<TMP_InputField>();
        Debug.Log (IDInput);

        log = GameObject.Find("LogObject").GetComponent<LogEntry>();
        Debug.Log (log);
    }

    public void Send()
    {
        LogEntry logEntry = new LogEntry();
        string json = JsonUtility.ToJson(logEntry);
        reference
            .Child("participants")
            .Child(participantId)
            .SetRawJsonValueAsync(json);
        Debug.Log("logEntry Sent");
    }

    public void ParticipantID()
    {
        participantId = IDInput.GetComponent<TMP_InputField>().text;

        Debug.Log (participantId);
    }
}
