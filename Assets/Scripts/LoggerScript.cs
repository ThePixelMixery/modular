using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggerScript : MonoBehaviour
{
    public string gameEvent;
    public string tag1;
    public string tag2;

    public void logIt(){
    LogScript.WriteNewLogEntry(gameEvent,tag1,tag2);
    }

    // Update is called once per frame
}
