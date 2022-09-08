using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using TMPro;
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
