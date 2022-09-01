using System.Collections;
using System.Collections.Generic;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.Objects;
using TMPro;
using UnityEngine;

public class LoggerObjectScript : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    public void DisplayData(string data)
    {
        outputText.color =
            outputText.color == Color.green ? Color.blue : Color.green;
        outputText.text = data;
        Debug.Log (data);
    }

    public void DisplayInfo(string info)
    {
        outputText.color = Color.white;
        outputText.text = info;
        Debug.Log (info);
    }

    public void DisplayErrorObject(string error)
    {
        var parsedError =
            StringSerializationAPI.Deserialize(typeof (FirebaseError), error) as
            FirebaseError;
        DisplayError(parsedError.message);
    }

    public void DisplayError(string error)
    {
        outputText.color = Color.red;
        outputText.text = error;
        Debug.LogError (error);
    }
}
