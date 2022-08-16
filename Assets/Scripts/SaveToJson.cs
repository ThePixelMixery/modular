using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class SaveToJson : MonoBehaviour
{
    DatabaseReference reference;

    string participantId;

    void Start()
    {
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log (reference);
    }

    public static void logEntry(string id, string tag1, string tag2)
    {

        Debug.Log(id + ", " + tag1 + ", "+ tag2);
    }
    public void writeNewEntry()
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
        InputField IDInput =
            GameObject.Find("InputField_ID").GetComponent<InputField>();
        participantId = IDInput.text;
        Debug.Log (participantId);
    }
    public static void logEntry(
        string newGameEvent,
        string newTag1,
        string newTag2
    )
}
